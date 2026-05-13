using Almacen_Sistema.MVVM.Models.Movements;
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

            decimal result = TransactionObject.Quantity - amountExit;

            if (result < 0)
                result = 0.00m;

            return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }
    }
}
