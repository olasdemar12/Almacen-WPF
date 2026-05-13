using Almacen_Sistema.MVVM.Models.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Dialogs.Movements.EntryMovementDelete
{
    public partial class DeleteMovementDialogViewModel
    {
        [ObservableProperty]
        private TransactionHistory _transactionMovements;
        [ObservableProperty]
        private bool _isLoading = false;
        [ObservableProperty]
        private bool _isEnable = true;
    }
}
