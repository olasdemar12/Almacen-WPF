using Almacen_Sistema.Composition;
using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.ViewModels.Pages;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.UI.Panels.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Product;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Media;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements
{
    public partial class EntryFormViewModel: ObservableValidator
    {
        public EntryFormViewModel(TypeActionMovementChanges action, TransactionHistory transaction)
        {
            _transactionService = MovementsViewModel.MovementService.TransactionService;
            MovementObject = transaction;
            IsEnable = true;
            this.ActionForm = action;
            TitleForm = action == TypeActionMovementChanges.Add ? "Registrar Entrada" : "Modificar Movimiento de Entrada";
            TotalAmount = action == TypeActionMovementChanges.Add ? 0.00m : transaction.Quantity;
            ContentButton = action == TypeActionMovementChanges.Add ? "Registrar Entrada" : "Guardar Cambios";
            Notes = action == TypeActionMovementChanges.Add ? string.Empty : transaction.Notes;
        }
        private ITransactionService _transactionService;
        [ObservableProperty]
        private TypeActionMovementChanges _actionForm;
        [ObservableProperty]
        private string _titleForm;
        [ObservableProperty]
        private TransactionHistory _movementObject;
        [ObservableProperty]
        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
        private decimal _totalAmount;
        [ObservableProperty]
        private bool _isEnable;
        [ObservableProperty]
        private string _contentButton;
        [ObservableProperty]
        [MaxLength(500, ErrorMessage = "Las notas no pueden exceder los 500 caracteres")]
        private string _notes;


        [RelayCommand]
        private async Task CloseForm()
        {
            if(ActionForm == TypeActionMovementChanges.Add)
            {
                IsEnable = false;
                DialogHost.Close("DialogsRoot", true);
                await DialogHost.Show(new ProductSelectionControl(), "DialogsRoot");
            }
            else
            {
                IsEnable = false;
                DialogHost.Close("DialogsRoot", true);
            }
        }

        [RelayCommand]
        private async Task SaveEntry()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                SystemSounds.Hand.Play();
                return;
            }
            await ExecuteActionFormTransaction();
            return;
        }

        private async Task ExecuteActionFormTransaction()
        {
            switch(ActionForm)
            {
                case TypeActionMovementChanges.Add:
                    MovementObject.Notes = Notes;
                    var result = await _transactionService.AddTransactionAsync(MovementObject.IdProduct, MovementObject.ProductName, MovementObject, TotalAmount);

                    var type = result.IsSuccess ? NotificationType.Success : NotificationType.Error;
                    var NotificationTask = NotificationServiceControl.Instance.ShowNotification(result.Message, type);
                    var CloseTask = CloseForm();
                    var EventNotificacion = EventsAplicationStockMaser.Instance.MovementEvents.OnTransactionLogChangesInvoke(TypeActionMovementChanges.Add, MovementObject);

                    await Task.WhenAll(NotificationTask, CloseTask, EventNotificacion);
                    break;
                case TypeActionMovementChanges.Update:
                    MovementObject.Notes = Notes;
                    MovementObject.Quantity = TotalAmount;
                    var resultUpdate = await _transactionService.EditTransactionAsync(MovementObject);

                    var typeUpdate = resultUpdate.IsSuccess ? NotificationType.Success : NotificationType.Error;
                    var NotificationTaskUpdate = NotificationServiceControl.Instance.ShowNotification(resultUpdate.Message, typeUpdate);
                    var CloseTaskUpdate = CloseForm();
                    var EventNotificacionUpdate = EventsAplicationStockMaser.Instance.MovementEvents.OnTransactionLogChangesInvoke(TypeActionMovementChanges.Update, MovementObject);

                    await Task.WhenAll(NotificationTaskUpdate, CloseTaskUpdate, EventNotificacionUpdate);
                    break;
            }
        }
    }
}
