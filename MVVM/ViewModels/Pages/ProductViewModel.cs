using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Forms.Product;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;

namespace Almacen_Sistema.MVVM.ViewModels.Pages
{
    public partial class ProductViewModel:ObservableObject
    {
        public ProductViewModel(IProductService productService)
        {
            this._productService = productService;
        }

        private readonly IProductService _productService;

        [RelayCommand]
        private async Task CategoryManagement()
        {
          await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
        }

        [RelayCommand]
        private async Task ProductAdd()
        {
            await DialogHost.Show(new ProductFormView("Agregar Nuevo Producto",new ProductModel(), _productService), "DialogsRoot");
        }
    }
}
