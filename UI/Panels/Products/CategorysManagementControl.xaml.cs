using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.ViewModels.Panels;
using Almacen_Sistema.Services.Category.Contracts;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Almacen_Sistema.UI.Panels.Products
{
    /// <summary>
    /// Lógica de interacción para CategorysManagementControl.xaml
    /// </summary>
    public partial class CategorysManagementControl : UserControl
    {
        public CategorysManagementControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var ViewModel = Bootstrapper.CreateCategorysManagementVM();
            this.DataContext = ViewModel;
            ViewModel.IsEnable = false;
            ViewModel.IsBusy = true;
            await ViewModel.LoadingData();
            ViewModel.IsBusy = false;
            ViewModel.IsEnable = true;
        }



    }
}
