using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;

namespace Almacen_Sistema.MVVM.ViewModels.Panels
{
    public partial class ProductSelectionViewModel:ObservableObject
    {
        public ProductSelectionViewModel(IProductService service)
        {
            this.productService = service;
        }

        //Servicio principal
        private IProductService productService;

        //Observables y colecciones para la vista del Panel
        [ObservableProperty]
        private ObservableCollection<ProductModel> products;

        [ObservableProperty]
        private ICollectionView productsViewItems;

        [ObservableProperty]
        private ObservableCollection<CategoryModel> categories;

        [ObservableProperty]
        private string searchProduct;

        [ObservableProperty]
        private int? selectedCategory = null;

        [ObservableProperty]
        private ProductModel selectedProduct;

        partial void OnSelectedProductChanged(ProductModel value)
        {
            if (value != null)
            {
                MessageBox.Show($"Seleccionaste: {value.ProductName}");
            }
        }

        public async Task LoadPanelSelection()
        {
            var ProductsTask = Task.Run(() => productService.GetAllProductsAsync());
            var CategoryTask = productService.GetCategoriesRegisterAsync();

            await Task.WhenAll(ProductsTask, CategoryTask);

            Products = new ObservableCollection<ProductModel>(ProductsTask.Result);
            Categories = new ObservableCollection<CategoryModel>(CategoryTask.Result);
            Categories.Add(new CategoryModel(0, "Sin Asignar", 0));

            ProductsViewItems = CollectionViewSource.GetDefaultView(Products);
            ProductsViewItems.Refresh();
        }
    }
}
