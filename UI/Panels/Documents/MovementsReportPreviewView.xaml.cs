using Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Movements;
using Almacen_Sistema.MVVM.ViewModels.Panels.InventoryInformation;
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

namespace Almacen_Sistema.UI.Panels.Documents
{
    /// <summary>
    /// Lógica de interacción para MovementsReportPreviewView.xaml
    /// </summary>
    public partial class MovementsReportPreviewView : UserControl
    {
        public MovementsReportPreviewView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.Unloaded += OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
                return;

            _isLoaded = true;

            _viewModel = new MovementsReportPreviewViewModel();
            DataContext = _viewModel;

            await _viewModel.LoadingTableInformation();
        }


        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _viewModel?.Dispose();

            DataContext = null;
            _viewModel = null;
            _isLoaded = false;
        }

        private MovementsReportPreviewViewModel? _viewModel;
        private bool _isLoaded;
    }
}
