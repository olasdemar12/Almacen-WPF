using Almacen_Sistema.MVVM.ViewModels.Login;
using Almacen_Sistema.UI.Window;
using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Almacen_Sistema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var loginViewModel = new LoginViewModel();
            if (loginViewModel.CloseLogin == null)
            {
                loginViewModel.CloseLogin = new Action(() =>
                {
                    var stockMaster = new StockMasterWindow();
                    stockMaster.Show();
                    this.Close();
                });
            }
            this.DataContext = loginViewModel;
        }
    }
}