using Almacen_Sistema.Composition;
using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.ViewModels.Pages;
using MaterialDesignThemes.Wpf;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Dialogs.Movements.EntryMovementDelete
{
    public partial class DeleteMovementDialogViewModel
    {
        
        public async Task ReactionDeleteMovement()
        {
            var result = await MovementsViewModel.MovementService.TransactionService.DeleteTransactionAsync(TransactionMovements.IdMovement);
            var type = result.IsSuccess ? NotificationType.Success : NotificationType.Error;

            //Tareas en paralelo para mostrar la notificación, cerrar el dialogo y actualizar la vista de movimientos
            var NotificationTask = NotificationServiceControl.Instance.ShowNotification(result.Message, type);
            var CloseTask = CloseDialog();
            var EventNotificacion = EventsAplicationStockMaser.Instance.MovementEvents.OnTransactionLogChangesInvoke(TypeActionMovementChanges.Delete, TransactionMovements);

            await Task.WhenAll(NotificationTask, CloseTask, EventNotificacion);
            IsEnable = true;
            IsLoading = false;
        }
    }
}
