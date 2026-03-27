using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Implementations;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using Almacen_Sistema.UI.Dialogs.Category;
using Almacen_Sistema.UI.Dialogs.Login;
using Almacen_Sistema.UI.Dialogs.Product;
using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Forms.Product;
using Almacen_Sistema.UI.Panels.Products;
using Almacen_Sistema.UI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
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
            StockMasterEvents.CategoryChanges += EventReactionCategory;
            StockMasterEvents.ProductChanges += EventReactionProductRemove;
        }

        #region Propiedades para el funcionamiento del View  y el ViewModel
        private readonly IProductService _productService;

        //Propiedades visibles para la vista:
        [ObservableProperty]
        private ObservableCollection<ProductModel> products;

        [ObservableProperty]
        private ICollectionView productsViewItems;

        [ObservableProperty]
        private ObservableCollection<CategoryModel> _categories;

        [ObservableProperty]
        private string _searchProduct;

        [ObservableProperty]
        private int? _selectedCategory = null;

        [ObservableProperty]
        private string _selectedTypeSale;

        [ObservableProperty]
        private bool _isBusy;

        public bool IsFilterActive => (SelectedCategory.HasValue || !string.IsNullOrWhiteSpace(SelectedTypeSale)) && !ProductsViewItems.Cast<Product>().Any();


        #endregion

        #region Comandos y Metodos para el Funcionamiento del ViewModel

        [RelayCommand]
        private async Task CategoryManagement()
        {
            var result = await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
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
            }
        }

        [RelayCommand]
        private async Task EditProduct(ProductModel product)
        {
            var result = await DialogHost.Show(new ProductFormView("Editar Producto", product, _productService), "DialogsRoot");
            if (result is Product ProductReturn && result != null)
            {
                var CategoryProduct = Categories.FirstOrDefault(c => c.IdCategoria == ProductReturn.IdCategory) ?? new CategoryModel();
                ProductReturn.CategoryName = CategoryProduct.NombreCategoria;
            }
        }

        [RelayCommand]
        private async Task RemoveProduct(ProductModel product)
        {
            var result = await DialogHost.Show(new DeleteProductDialog(product, _productService,Dialogs.ActionDeleteProduct.DeleteView), "DialogsRoot");
            if ((result is ServiceResult<bool> ServiceResult && result != null) && ServiceResult.IsSuccess)
            {
                Products.Remove(product);
            }
        }

        [RelayCommand]
        private async Task SearchProducts()
        {
            var producto = Products.FirstOrDefault(p => p.ProductName == SearchProduct || p.BarCode == SearchProduct);
            if(producto != null)
            {
                await DialogHost.Show(new ProductFormView("Producto Encontrado", producto, _productService), "DialogsRoot");
            }
            else if(producto == null && !string.IsNullOrEmpty(SearchProduct))
            {
                var dialogData = new
                {
                    Titulo = "Error en la Búsqueda",
                    Mensaje = "No se encontraron resultados que coincidan con la Búsqueda."
                };

                var view = new ErrorLoginDialog
                {
                    DataContext = dialogData
                };
                await DialogHost.Show(view, "DialogsRoot");
            }
        }


        //Metodo para cargar datos al modulo
        public async Task LoadingProducts()
        {
            var ProductsTask =  Task.Run(() => _productService.GetAllProductsAsync());
            var CategoryTask =  _productService.GetCategoriesRegisterAsync();

            await Task.WhenAll(ProductsTask, CategoryTask);

            Products = new ObservableCollection<ProductModel>(await ProductsTask);
            Categories = new ObservableCollection<CategoryModel>(await CategoryTask);
            Categories.Add(new CategoryModel(0, "Sin Asignar", 0));

            ProductsViewItems = CollectionViewSource.GetDefaultView(Products);
            ProductsViewItems.Filter = FilterProducts;
            ProductsViewItems.Refresh();
        }
    
        //Metodo para notificar de cambios que ocurrieron en categorias
        private void EventReactionCategory(CategoryActionChanges action, CategoryModel category)
        {
            
            switch(action)
            {
                case CategoryActionChanges.AddCategory:
                    Categories.Add(category);
                    break;
                case CategoryActionChanges.UpdateCategory:
                    if(category != null)
                    {
                        Categories.FirstOrDefault(c => c.IdCategoria == category.IdCategoria).NombreCategoria = category.NombreCategoria;
                        foreach (var item in Products.Where(p => p.IdCategory == category.IdCategoria))
                        {
                            item.CategoryName = category.NombreCategoria;
                        }
                        ProductsViewItems.Refresh();
                    }
                    break;
                case CategoryActionChanges.DeleteCategory:
                    var result = Categories.FirstOrDefault(c => c.IdCategoria == category.IdCategoria);
                    if(result != null)
                    {
                        Categories.Remove(result);
                        foreach (var item in Products.Where(p => p.IdCategory == category.IdCategoria))
                        {
                            item.CategoryName = "Sin Asignar";
                            item.IdCategory = 0;
                        }
                    }
                    break;
            }
        }

        //Metodo para notificar al remover un producto en el formulario de Busqueda:
        private async void EventReactionProductRemove(Product product)
        {
            Products.Remove(product);
        }

        //Metodo para filtrar datos:
        private bool FilterProducts(object obj)
        {
            if (obj is not Product product)
                return false;

            bool matchesCategory = !SelectedCategory.HasValue
                || product.IdCategory == SelectedCategory.Value;

            bool matchesTypeSale = string.IsNullOrWhiteSpace(SelectedTypeSale)
                || product.SaleType == SelectedTypeSale;
            return matchesCategory && matchesTypeSale;
        }

        partial void OnSelectedCategoryChanged(int? value)
        {
            ProductsViewItems.Refresh();
            OnPropertyChanged(nameof(IsFilterActive));
        }

        partial void OnSelectedTypeSaleChanged(string value)
        {
            ProductsViewItems.Refresh();
            OnPropertyChanged(nameof(IsFilterActive));
        }
        #endregion
    }
}
