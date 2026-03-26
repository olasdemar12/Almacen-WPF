using Almacen_Sistema.MVVM.ViewModels.Forms;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.UI.Forms.Product
{
    /// <summary>
    /// Lógica de interacción para ProductFormView.xaml
    /// </summary>
    public partial class ProductFormView : UserControl
    {
        public ProductFormView(string title, ProductModel product, IProductService productService)
        {
            InitializeComponent();
            this.DataContext = new ProductFormViewModel(title, product, productService);

            Loaded += ProductFormView_Loaded;
        }

        private async void ProductFormView_Loaded(object sender, RoutedEventArgs e)
        {
            var ViewModelForm = this.DataContext as ProductFormViewModel;
            if(ViewModelForm != null)
            await ViewModelForm.InitializeAsync();
        }
    }
}
