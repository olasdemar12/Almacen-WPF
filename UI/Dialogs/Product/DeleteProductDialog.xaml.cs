using Almacen_Sistema.MVVM.ViewModels.Dialogs;
using Almacen_Sistema.Services.Product.Contracts;
using MVVM.Models.Product;
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
using ProductModel = MVVM.Models.Product.Product;

namespace Almacen_Sistema.UI.Dialogs.Product
{
    /// <summary>
    /// Lógica de interacción para DeleteProductDialog.xaml
    /// </summary>
    public partial class DeleteProductDialog : UserControl
    {
        public DeleteProductDialog(ProductModel product, IProductService service, ActionDeleteProduct actionDelete)
        {
            InitializeComponent();
            var ViewModel = new DeleteProductViewModel(product, service, actionDelete);
            this.DataContext = ViewModel;
        }
    }
}
