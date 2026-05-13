using Almacen_Sistema.MVVM.Models.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Dialogs.Movements.EntryMovementDelete
{
    public partial class DeleteMovementDialogViewModel:ObservableObject
    {
       public DeleteMovementDialogViewModel(TransactionHistory transaction)
       {
            TransactionMovements = transaction;
       }

        [RelayCommand]
        private async Task CloseDialog()
        {
            DialogHost.Close("DialogsRoot");
        }

        [RelayCommand]
        private async Task DeleteMovement()
        {
            IsEnable = false;
            IsLoading = true;

            await ReactionDeleteMovement();
        }
    }
}
