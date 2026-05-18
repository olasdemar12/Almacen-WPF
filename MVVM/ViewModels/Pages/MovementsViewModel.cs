using Almacen_Sistema.Composition;
using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Movements.General;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Dialogs.Movement;
using Almacen_Sistema.UI.Forms.Movements.Entry;
using Almacen_Sistema.UI.Forms.Movements_2;
using Almacen_Sistema.UI.Panels.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Pages
{
    public partial class MovementsViewModel : ObservableObject
    {

        public MovementsViewModel(IMovementService movementService)
        {
            _movementService = movementService;
            MovementTypes = Enum.GetValues(typeof(TypeMovementTransaction)).Cast<TypeMovementTransaction>().ToList();
            EventsAplicationStockMaser.Instance.MovementEvents.OnTransactionLogChanges += ReactionPageEvent;
        }

        public static IMovementService MovementService { get => _movementService; }
        private static IMovementService _movementService;

        public IReadOnlyList<TypeMovementTransaction> MovementTypes { get; }
        [ObservableProperty]
        private ObservableCollection<TransactionHistory> _transactionHistoryItems;
        [ObservableProperty]
        private ICollectionView _transactionHistoryViewItems;
        [ObservableProperty]
        private TypeMovementTransaction? selectedMovementType;
        [ObservableProperty]
        private DateTime? startDate;
        [ObservableProperty]
        private DateTime? endDate;
        [ObservableProperty]
        private string searchTextProductMovements;
        [ObservableProperty]
        private bool _isBusy;
        public bool IsFilterActive => TransactionHistoryViewItems.IsEmpty;

        private CancellationTokenSource? _searchCancellationTokenSource;

        [RelayCommand]
        private async Task EntryMovement()
        {
             await DialogHost.Show(new ProductSelectionControl(), "DialogsRoot");
        }

        [RelayCommand]
        private async Task ExitMovement()
        {
            await DialogHost.Show(new ProductSelectionExitControl(), "DialogsRoot");
        }
        [RelayCommand]
        private async Task ConfirmMovementAsync(TransactionHistory transaction)
        {
            var TotalStockActive = await _movementService.TransactionService.GetTotalAmountStockByIdProduct(transaction.IdProduct);
            if (transaction == null)
                return;

            if (transaction.TypeMovement == TypeMovementTransaction.Salida && TotalStockActive <= 0)
            {
                TransactionHistoryViewItems.Refresh();
                MessageBox.Show("No se puede confirmar la transacción. Verifique que no esté confirmada previamente y que haya stock disponible.", "Error al Confirmar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            transaction.Confirmed = true;
            await MovementService.TransactionService.ConfirmTransactionAsync(transaction);

            TransactionHistoryViewItems.Refresh();
        }
        [RelayCommand]
        private async Task ModificationMovement(TransactionHistory transaction)
        {
            switch (transaction.TypeMovement)
            {
                case TypeMovementTransaction.Entrada:
                    await DialogHost.Show(new FormEnrtyProducts(TypeActionMovementChanges.Update, transaction), "DialogsRoot");
                    break;

                case TypeMovementTransaction.Salida:
                    var TotalStock = await _movementService.TransactionService.GetTotalAmountStockByIdProduct(transaction.IdProduct);
                    await DialogHost.Show(new ExitFormControl(TypeActionMovementChanges.Update, transaction, TotalStock), "DialogsRoot");
                    break;
            }
        }
        [RelayCommand]
        private async Task RemoveMovementRegisted(TransactionHistory transaction)
        {
            await DialogHost.Show(new DeleteMovementDialog(transaction), "DialogsRoot");
        }

        partial void OnSearchTextProductMovementsChanged(string value)
        {
            _searchCancellationTokenSource?.Cancel();

            var cts = new CancellationTokenSource();
            _searchCancellationTokenSource = cts;

            _ = ApplySearchAsync(cts);
        }
        partial void OnSelectedMovementTypeChanged(TypeMovementTransaction? value)
        {
            TransactionHistoryViewItems.Refresh();
        }
        partial void OnStartDateChanged(DateTime? value)
        {
            if (value.HasValue && EndDate.HasValue)
            {
                if (value.Value > EndDate.Value)
                {
                    EndDate = value;
                }
            }

            TransactionHistoryViewItems.Refresh();
        }
        partial void OnEndDateChanged(DateTime? value)
        {
            if (value.HasValue && StartDate.HasValue)
            {
                if (value.Value < StartDate.Value)
                {
                    StartDate = value;
                }
            }

            TransactionHistoryViewItems.Refresh();
        }

        private bool FilterMovements(object obj)
        {
            if (obj is not TransactionHistory movement)
                return false;

            bool matchesProductName = string.IsNullOrEmpty(SearchTextProductMovements) || movement.ProductName.Contains(SearchTextProductMovements, StringComparison.OrdinalIgnoreCase);

            if (StartDate.HasValue)
            {
                DateTime start = StartDate.Value.Date;

                if (movement.RegisterDate < start)
                    return false;
            }

            if (EndDate.HasValue)
            {
                DateTime endExclusive = EndDate.Value.Date.AddDays(1);

                if (movement.RegisterDate >= endExclusive)
                    return false;
            }

            if (SelectedMovementType.HasValue)
            {
                if (movement.TypeMovement != SelectedMovementType.Value)
                    return false;
            }

            return true && matchesProductName;
        }
        private void ConfigureView()
        {
            TransactionHistoryViewItems.SortDescriptions.Clear();


            TransactionHistoryViewItems.SortDescriptions.Add(
                new SortDescription(nameof(TransactionHistory.Confirmed), ListSortDirection.Ascending)
            );
            TransactionHistoryViewItems.Filter = FilterMovements;
        }
        private async Task ApplySearchAsync(CancellationTokenSource cts)
        {
            try
            {
                await Task.Delay(300, cts.Token);
                TransactionHistoryViewItems.Refresh();
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                if (_searchCancellationTokenSource == cts)
                {
                    TransactionHistoryViewItems.Refresh();
                    _searchCancellationTokenSource = null;
                }
            }
        }

        public async Task LoadMovementDates()
        {
            IsBusy = true;
            TransactionHistoryItems = new ObservableCollection<TransactionHistory>(await _movementService.TransactionService.GetAllTransactionAsync());
            TransactionHistoryViewItems = CollectionViewSource.GetDefaultView(TransactionHistoryItems);
            ConfigureView();
            TransactionHistoryViewItems.Refresh();
            IsBusy = false;
        }

        public async Task ReactionPageEvent(TypeActionMovementChanges typeAction, TransactionHistory transaction)
        {
            switch(typeAction)
            {
                case TypeActionMovementChanges.Add:
                    TransactionHistoryItems.Add(transaction);
                    break;
                case TypeActionMovementChanges.Update:
                    TransactionHistory item = TransactionHistoryItems.FirstOrDefault(x => x.IdMovement == transaction.IdMovement) ?? new TransactionHistory();
                    if (item != null)
                    {
                        int index = TransactionHistoryItems.IndexOf(item);
                        TransactionHistoryItems[index] = transaction;
                    }
                    break;
                case TypeActionMovementChanges.Delete:
                    TransactionHistoryItems.Remove(transaction);
                    break;
            }
        }
    }
}
