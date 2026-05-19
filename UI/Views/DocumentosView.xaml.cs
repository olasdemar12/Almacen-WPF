using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
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
    /// Lógica de interacción para DocumentosView.xaml
    /// </summary>
    public partial class DocumentosView : Page
    {
        public DocumentosView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = new DocumentsViewModel();
            this.DataContext = vm;
            await vm.LoadingInformationModule();
        }
    }
}
