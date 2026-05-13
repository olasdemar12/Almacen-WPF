using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements.ExitForm
{
    public partial class ExitFormViewModel
    {
        //Modelo de la vista
        [ObservableProperty]
        private TransactionHistory _transactionObject;
        [ObservableProperty]
        private decimal _takenAmount;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(ExitFormViewModel), nameof(ValidateAmountExit))]
        private decimal _amountExit;
        [ObservableProperty]
        private string _notes = string.Empty;

        //Propiedades de funcionamiento
        [ObservableProperty]
        private bool _showButtonBackActive = TypeActionForm == TypeActionMovementChanges.Add;
        [ObservableProperty]
        private string _titleForm = TypeActionForm == TypeActionMovementChanges.Add ? "Registrar Salida" : "Modificar Movimiento de Salida";
        [ObservableProperty]
        private string _contentButton = TypeActionForm == TypeActionMovementChanges.Add ? "Registrar Salida" : "Guardar Cambios";
        [ObservableProperty]
        private bool _formEnabled = true;
        [ObservableProperty]
        private bool _loadingAction = false;
    }
}
