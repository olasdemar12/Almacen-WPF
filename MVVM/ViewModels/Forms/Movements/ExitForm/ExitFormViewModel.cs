using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.Services.Movements.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements.ExitForm
{
    public partial class ExitFormViewModel : ObservableValidator
    {
        public ExitFormViewModel(TypeActionMovementChanges ActionForm, TransactionHistory transaction)
        {
            TypeActionForm = ActionForm;
            TransactionObject = transaction;
            TakenAmount = transaction.Quantity;
            Notes = ActionForm == TypeActionMovementChanges.Add ? string.Empty : transaction.Notes;
        }

        private readonly ITransactionService _transactionService;
        private static TypeActionMovementChanges TypeActionForm;

    }
}
