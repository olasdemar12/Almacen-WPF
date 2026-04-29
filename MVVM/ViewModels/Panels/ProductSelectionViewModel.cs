using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using Almacen_Sistema.UI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.Models.Product;
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

        #region Propiedades, Campos y Observables
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

        [ObservableProperty]
        private bool isLoadingProducts = false;

        [ObservableProperty]
        private bool isLoadingResults = false;

        [ObservableProperty]
        private bool isResultsEmpty = false;

        private CancellationTokenSource? _searchCancellationTokenSource;

        #endregion


        #region Funciones de Clases

        private bool FilterProducts(object obj)
        {
            if (obj is not ProductModel product)
                return false;

            // --- Filtro por texto ---
            bool matchesText = true;

            if (!string.IsNullOrWhiteSpace(SearchProduct))
            {
                string text = SearchProduct.Trim();

                matchesText =
                    product.ProductName?.Contains(text, StringComparison.OrdinalIgnoreCase) == true ||
                    product.BarCode?.Contains(text, StringComparison.OrdinalIgnoreCase) == true;
            }

            // --- Filtro por categoría ---
            bool matchesCategory = true;

            if (SelectedCategory != null)
            {
                // Ajusta esto según tu modelo (Id o Nombre)
                matchesCategory = product.IdCategory == SelectedCategory.GetValueOrDefault();
            }

            // --- Resultado final ---
            return matchesText && matchesCategory;
        }

        partial void OnSelectedProductChanged(ProductModel value)
        {
            if (value != null)
            {
                MessageBox.Show($"Seleccionaste: {value.ProductName}");
            }
        }

        partial void OnSearchProductChanged(string value)
        {
            _searchCancellationTokenSource?.Cancel();

            var cts = new CancellationTokenSource();
            _searchCancellationTokenSource = cts;

            _ = ApplySearchAsync(cts);
        }

        partial void OnSelectedCategoryChanged(int? value)
        {
            _searchCancellationTokenSource?.Cancel();

            var cts = new CancellationTokenSource();
            _searchCancellationTokenSource = cts;

            _ = ApplySearchAsync(cts);
        }

        private async Task ApplySearchAsync(CancellationTokenSource cts)
        {
            try
            {
                IsLoadingResults = true;

                await Task.Delay(300, cts.Token); // prueba visual

                ProductsViewItems.Refresh();
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                if (_searchCancellationTokenSource == cts)
                {
                    IsResultsEmpty = ProductsViewItems.IsEmpty;
                    IsLoadingResults = false;
                }
            }
        }

        public async Task LoadPanelSelection()
        {
            IsLoadingProducts = true;
            var ProductsTask = Task.Run(() => productService.GetAllProductsAsync());
            var CategoryTask = productService.GetCategoriesRegisterAsync();

            await Task.WhenAll(ProductsTask, CategoryTask);

            Products = new ObservableCollection<ProductModel>(ProductsTask.Result);
            Categories = new ObservableCollection<CategoryModel>(CategoryTask.Result);
            Categories.Add(new CategoryModel(0, "Sin Asignar", 0));

            ProductsViewItems = CollectionViewSource.GetDefaultView(Products);
            ProductsViewItems.Filter = FilterProducts;
            ProductsViewItems.Refresh();
            IsLoadingProducts = false;
        }

        #endregion
    }
}
