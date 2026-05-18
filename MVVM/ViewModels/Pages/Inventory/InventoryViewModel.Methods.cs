using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Pages.Inventory
{
    public partial class InventoryViewModel
    {
        public async Task LoadInventoryItems()
        {
            var inventoryRows = await _inventoryService.GetInventoryRows();
            InventoryItems = new ObservableCollection<InventoryRow>(inventoryRows);
            InventoryItemsView = CollectionViewSource.GetDefaultView(InventoryItems);
            Categories = await _categoryRepository.GetAllCategory();
            Categories.Add(new Category(0, "Sin Asignar", 0));
            InventoryItemsView.Filter = FilterStocksInventory;
            InventoryItemsView.Refresh();
        }

        private bool FilterStocksInventory(object obj)
        {
            if (obj is not InventoryRow stockInventory)
                return false;

            bool matchesProductName = string.IsNullOrEmpty(SearchTextProduct) || stockInventory.ProductName.Contains(SearchTextProduct, StringComparison.OrdinalIgnoreCase);

            if (StarDate.HasValue)
            {
                DateTime start = StarDate.Value.Date;

                if (stockInventory.LastMovement < start)
                    return false;
            }

            if (EndDate.HasValue)
            {
                DateTime endExclusive = EndDate.Value.Date.AddDays(1);

                if (stockInventory.LastMovement >= endExclusive)
                    return false;
            }

            if (SelectedStateStock.HasValue)
            {
                if (stockInventory.StateStock != SelectedStateStock.Value)
                    return false;
            }

            if(IdCategorySelected.HasValue)
            {
                if(stockInventory.IdCategory != IdCategorySelected.Value)
                    return false;
            }

            return true && matchesProductName;
        }

        partial void OnSearchTextProductChanged(string value)
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
                await Task.Delay(300, cts.Token);
                InventoryItemsView.Refresh();
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                if (_searchCancellationTokenSource == cts)
                {
                    InventoryItemsView.Refresh();
                    _searchCancellationTokenSource = null;
                }
            }
        }

        partial void OnIdCategorySelectedChanged(int? value)
        {
            InventoryItemsView.Refresh();
        }

        partial void OnSelectedStateStockChanged(StateStockInventory? value)
        {
            InventoryItemsView.Refresh();
        }

        partial void OnStarDateChanged(DateTime? value)
        {
            InventoryItemsView.Refresh();
        }

        partial void OnEndDateChanged(DateTime? value)
        {
            InventoryItemsView.Refresh();
        }

        partial void OnSelectedInventoryRowChanged(InventoryRow? value)
        {
            if (value is null)
                return;

            SelectedInventoryRow = null;
            SelectedProductIndex = -1;
            InventoryItemsView?.MoveCurrentToPosition(-1);
        }
    }
}
