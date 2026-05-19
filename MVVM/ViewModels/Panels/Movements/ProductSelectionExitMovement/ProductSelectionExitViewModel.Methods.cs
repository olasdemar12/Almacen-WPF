using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using Almacen_Sistema.UI.Forms.Movements_2;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Movements.ProductSelectionExitMovement
{
    public partial class ProductSelectionExitViewModel
    {
        private bool FilterStocksRows(object obj)
        {
            if (obj is not CurrentStockRowPanel stockRow)
                return false;

            // --- Filtro por texto ---
            bool matchesText = true;

            if (!string.IsNullOrWhiteSpace(SearchTextProduct))
            {
                string text = SearchTextProduct.Trim();

                //TODO: Habilitar busqueda por codigo de barras
                matchesText = stockRow.ProductName?.Contains(text, StringComparison.OrdinalIgnoreCase) == true;
            }
            bool matchesCategory = true;

            if (SelectedCategory != null)
            {
                matchesCategory = stockRow.CategoryName == SelectedCategory;
            }

            return matchesText && matchesCategory;
        }
        partial void OnSelectedStockRowChanging(CurrentStockRowPanel? value)
        {
            if(value != null)
            {
                var transaction = new TransactionHistory(0,value.Value.IdProduct,
                    DateTime.Now, TypeMovementTransaction.Salida,value.Value.ProductName,
                    value.Value.TotalAmount,string.Empty,false);

                DialogHost.Close("DialogsRoot", true);
                DialogHost.Show(new ExitFormControl(TypeActionMovementChanges.Add, transaction,value.Value.TotalAmount), "DialogsRoot");
            }
        }
        partial void OnSearchTextProductChanged(string value)
        {
            _searchCancellationTokenSource?.Cancel();

            var cts = new CancellationTokenSource();
            _searchCancellationTokenSource = cts;

            _ = ApplySearchAsync(cts);
        }
        partial void OnSelectedCategoryChanged(string? value)
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

                RowPanelItemsView.Refresh();
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                if (_searchCancellationTokenSource == cts)
                {
                    IsResultsEmpty = RowPanelItemsView.IsEmpty;
                    IsLoadingResults = false;
                }
            }
        }
        public async Task LoadPanelSelection()
        {
            IsLoadingStock = true;
            var RowsTask = _currentStocksService.GetAllStockExitMovement();
            var CategoryTask = _productService.GetCategoriesRegisterAsync();
            await Task.WhenAll(RowsTask, CategoryTask);

            RowPanelItems = new ObservableCollection<CurrentStockRowPanel?>(RowsTask.Result);
            Categories = CategoryTask.Result;
            Categories.Add(new Category(0,"Sin Asignar",0));
            RowPanelItemsView = CollectionViewSource.GetDefaultView(RowPanelItems);
            RowPanelItemsView.Filter = FilterStocksRows;
            RowPanelItemsView.Refresh();
            IsLoadingStock = false;
        }
    }
}
