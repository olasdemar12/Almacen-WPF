using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.UI.Panels.Movements;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements.ExitForm
{
    public partial class ExitFormViewModel
    {
        [RelayCommand]
        private async Task CloseForm()
        {
            if (TypeActionForm == TypeActionMovementChanges.Add)
            {
                FormEnabled = false;
                DialogHost.Close("DialogsRoot", true);
                await DialogHost.Show(new ProductSelectionExitControl(), "DialogsRoot");
            }
            else
            {
                FormEnabled = false;
                DialogHost.Close("DialogsRoot", true);
            }
        }

        [RelayCommand]
        private async Task SaveMovementExit()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                SystemSounds.Hand.Play();
                return;
            }

            switch(TypeActionForm)
            {
                case TypeActionMovementChanges.Add:
                    await AddMovementExit();
                    break;
                case TypeActionMovementChanges.Update:
                    await UpdateMovementExit();
                    break;
            }
        }
    }
}
