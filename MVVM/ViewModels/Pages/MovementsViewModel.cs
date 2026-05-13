using Almacen_Sistema.Composition;
using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Movements.General;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Dialogs.Movement;
using Almacen_Sistema.UI.Forms.Movements.Entry;
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
    public partial class MovementsViewModel: ObservableObject
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
            if (transaction == null)
                return;

            if (transaction.Confirmed)
                return;

            transaction.Confirmed = true;
            await MovementService.TransactionService.ConfirmTransactionAsync(transaction);

            TransactionHistoryViewItems.Refresh();
        }
        [RelayCommand]
        private async Task ModificationEntryMovement(TransactionHistory transaction)
        {
            await DialogHost.Show(new FormEnrtyProducts(TypeActionMovementChanges.Update, transaction), "DialogsRoot");
        }
        [RelayCommand]
        private async Task RemoveMovementRegisted(TransactionHistory transaction)
        {
            await DialogHost.Show(new DeleteMovementDialog(transaction), "DialogsRoot");
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

            return true;
        }
        private void ConfigureView()
        {
            TransactionHistoryViewItems.SortDescriptions.Clear();


            TransactionHistoryViewItems.SortDescriptions.Add(
                new SortDescription(nameof(TransactionHistory.Confirmed), ListSortDirection.Ascending)
            );
            TransactionHistoryViewItems.Filter = FilterMovements;
        }
        public async Task LoadMovementDates()
        {
            TransactionHistoryItems = new ObservableCollection<TransactionHistory>(await _movementService.TransactionService.GetAllTransactionAsync());
            TransactionHistoryViewItems = CollectionViewSource.GetDefaultView(TransactionHistoryItems);
            ConfigureView();
            TransactionHistoryViewItems.Refresh();
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
