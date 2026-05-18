using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.ViewModels.Pages;
using Almacen_Sistema.Services.Movements.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Media.TextFormatting;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements.ExitForm
{
    public partial class ExitFormViewModel : ObservableValidator
    {
        public ExitFormViewModel(TypeActionMovementChanges ActionForm, TransactionHistory transaction,decimal TotalStock = 0.00m)
        {
            _transactionService = MovementsViewModel.MovementService.TransactionService;
            TypeActionForm = ActionForm;
            TransactionObject = transaction;
            this.TotalAmount =  TotalStock;
            this.TakenAmount = this.TotalAmount;
            SetActionTypeForm();
        }

        private readonly ITransactionService _transactionService;
        private static TypeActionMovementChanges TypeActionForm;

        private void SetActionTypeForm()
        {
            ShowButtonBackActive = TypeActionForm == TypeActionMovementChanges.Add;
            AmountExit = TypeActionForm == TypeActionMovementChanges.Add ? 0.00m : TransactionObject.Quantity;
            Notes = TypeActionForm == TypeActionMovementChanges.Add ? string.Empty : TransactionObject.Notes;
            TitleForm = TypeActionForm == TypeActionMovementChanges.Add ? "Registrar Salida" : "Modificar Movimiento de Salida";
            ContentButton = TypeActionForm == TypeActionMovementChanges.Add ? "Registrar Salida" : "Guardar Cambios";
        }

    }
}
