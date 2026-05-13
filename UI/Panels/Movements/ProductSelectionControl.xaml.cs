using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.ViewModels.Forms;
using Almacen_Sistema.MVVM.ViewModels.Panels;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Almacen_Sistema.UI.Panels.Movements
{
    /// <summary>
    /// Lógica de interacción para ProductSelectionControl.xaml
    /// </summary>
    public partial class ProductSelectionControl : UserControl
    {
        public ProductSelectionControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var ViewModel = new ProductSelectionViewModel();
            this.DataContext = ViewModel;
            await ViewModel.LoadPanelSelection();
        }
    }
}
