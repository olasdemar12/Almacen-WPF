using Almacen_Sistema.UI.Panels.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.MVVM.ViewModels.Pages
{
    public partial class MovementsViewModel: ObservableObject
    {
        [RelayCommand]
        private async Task EntryMovement()
        {
            await DialogHost.Show(new ProductSelectionControl(), "DialogsRoot");
        }
    }
}
