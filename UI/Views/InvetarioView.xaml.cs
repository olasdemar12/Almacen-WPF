using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.ViewModels.Pages.Inventory;
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

namespace Almacen_Sistema.UI.Views
{
    /// <summary>
    /// Lógica de interacción para InvetarioView.xaml
    /// </summary>
    public partial class InvetarioView : Page
    {
        public InvetarioView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = new InventoryViewModel();
            this.DataContext = viewModel;
            await viewModel.LoadInventoryItems();
        }
    }
}
