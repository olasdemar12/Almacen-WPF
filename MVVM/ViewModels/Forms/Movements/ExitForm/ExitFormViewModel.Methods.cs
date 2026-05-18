using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using StockMasterControls;
using System;
using System.ComponentModel.DataAnnotations;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements.ExitForm
{
    public partial class ExitFormViewModel
    {
        public static ValidationResult? ValidateAmountExit(decimal amountExit, ValidationContext context)
        {
            var viewModel = (ExitFormViewModel)context.ObjectInstance;

            if (viewModel.TransactionObject is null)
                return new ValidationResult("No hay un producto seleccionado.");

            if (amountExit <= 0)
                return new ValidationResult("La cantidad de salida debe ser mayor a cero.");

            if (amountExit > viewModel.TransactionObject.Quantity)
                return new ValidationResult("La cantidad de salida no puede ser mayor a la cantidad disponible.");

            return ValidationResult.Success;
        }

        partial void OnAmountExitChanged(decimal value)
        {
            TakenAmount = CalculateTakenAmount(value);

            ValidateProperty(value, nameof(AmountExit));
        }

        private decimal CalculateTakenAmount(decimal amountExit)
        {
            if (TransactionObject is null)
                return 0.00m;

            decimal result = TotalAmount - amountExit;

            if (result < 0)
                result = 0.00m;

            return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }


        private async Task AddMovementExit()
        {
            TransactionObject.Notes = Notes;
            var result = await _transactionService.AddTransactionAsync(TransactionObject.IdProduct, TransactionObject.ProductName, TransactionObject, AmountExit);

            var type = result.IsSuccess ? NotificationType.Success : NotificationType.Error;
            var NotificationTask = NotificationServiceControl.Instance.ShowNotification(result.Message, type);
            var CloseTask = CloseForm();
            var EventNotificacion = EventsAplicationStockMaser.Instance.MovementEvents.OnTransactionLogChangesInvoke(TypeActionMovementChanges.Add, TransactionObject);

            await Task.WhenAll(NotificationTask, CloseTask, EventNotificacion);
        }

        private async Task UpdateMovementExit()
        {
            TransactionObject.Notes = Notes;
            TransactionObject.Quantity = AmountExit;
            var result = await _transactionService.EditTransactionAsync(TransactionObject);
            var type = result.IsSuccess ? NotificationType.Success : NotificationType.Error;
            var NotificationTask = NotificationServiceControl.Instance.ShowNotification(result.Message, type);
            var CloseTask = CloseForm();
            var EventNotificacion = EventsAplicationStockMaser.Instance.MovementEvents.OnTransactionLogChangesInvoke(TypeActionMovementChanges.Update, TransactionObject);
            await Task.WhenAll(NotificationTask, CloseTask, EventNotificacion);
        }
    }
}
