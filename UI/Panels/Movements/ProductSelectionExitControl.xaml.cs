using Almacen_Sistema.MVVM.ViewModels.Panels.Movements.ProductSelectionExitMovement;
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

namespace Almacen_Sistema.UI.Panels.Movements
{
    /// <summary>
    /// Lógica de interacción para ProductSelectionExitControl.xaml
    /// </summary>
    public partial class ProductSelectionExitControl : UserControl
    {
        public ProductSelectionExitControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var ViewModel = new ProductSelectionExitViewModel();
            this.DataContext = ViewModel;
            await ViewModel.LoadPanelSelection();
        }
    }
}
