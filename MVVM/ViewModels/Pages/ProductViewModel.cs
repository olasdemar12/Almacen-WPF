using Almacen_Sistema.UI.Panels.Products;
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
    partial class ProductViewModel:ObservableObject
    {


        [RelayCommand]
        private async Task CategoryManagement()
        {
           await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
        }

        [RelayCommand]
        private void ProductAdd()
        {
            MessageBox.Show("Implementacion de comando **Productos** Funcionando");
        }
    }
}
