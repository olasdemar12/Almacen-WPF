using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Forms.Product;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Product;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
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

        //Propiedades visibles para la vista:
        public ObservableCollection<ProductModel> Products { get; set; }

        public ICollectionView ProductsViewItems { get; set; }

        public List<CategoryModel> Categories { get; set; }

        [ObservableProperty]
        private string _searchProduct;

        [ObservableProperty]
        private int _selectedCategory;

        [ObservableProperty]
        private string _selectedTypeSale;

        [ObservableProperty]
        private bool _isBusy;

        [RelayCommand]
        private async Task CategoryManagement()
        {
          await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
        }

        [RelayCommand]
        private async Task ProductAdd()
        {
           var result = await DialogHost.Show(new ProductFormView("Agregar Nuevo Producto",new ProductModel(), _productService),"DialogsRoot");
            if (result is Product product && result != null)
            {
                var CategoryProduct = Categories.FirstOrDefault(c => c.IdCategoria == product.IdCategory) ?? new CategoryModel();
                product.CategoryName = CategoryProduct.NombreCategoria;
                Products.Add(product);
                ProductsViewItems.Refresh();
            }
        }

        [RelayCommand]
        private async Task EditProduct(Product product)
        {
            MessageBox.Show($"Editar Producto: {product.ProductName}");
            //await DialogHost.Show(new ProductFormView("Editar Producto", product, _productService), "DialogsRoot");
        }

        public async Task LoadingProducts()
        {
            IsBusy = true;
            var ProductsTask = _productService.GetAllProductsAsync();
            var CategoryTask = _productService.GetCategoriesRegisterAsync();
            await Task.WhenAll(ProductsTask,CategoryTask);

            Products = new ObservableCollection<ProductModel>(await ProductsTask);
            Categories = await CategoryTask;

            ProductsViewItems = CollectionViewSource.GetDefaultView(Products);
            IsBusy = false;
        }
    }
}
