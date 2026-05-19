using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.InventoryInformation
{
    public partial class InventoryInformationViewModel
    {
        partial void OnMinimumStockChanged(decimal value)
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            _updateMinimumStockCancellationTokenSource?.Cancel();
            var cts = new CancellationTokenSource();
            _updateMinimumStockCancellationTokenSource = cts;

            _ = ApplyUpdateMinimumStockAsync(cts);
        }

        private async Task ApplyUpdateMinimumStockAsync(CancellationTokenSource cts)
        {
            try
            {
                await Task.Delay(300, cts.Token);
                await _productInformationService.UpdateMinumStockProduct(_objectInventory.IdProduct,this.MinimumStock);
                if (_objectInventory.TotalStock == 0)
                {
                    _objectInventory.StateStock = StateStockInventory.Agotado;
                }
                else if (MinimumStock >= _objectInventory.TotalStock)
                {
                    _objectInventory.StateStock = StateStockInventory.Bajo;
                }
                else
                {
                    _objectInventory.StateStock = StateStockInventory.Normal;
                }
                _objectInventory.MiniumStock = this.MinimumStock;
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                if (_updateMinimumStockCancellationTokenSource == cts)
                {
                    _updateMinimumStockCancellationTokenSource = null;
                }
            }
        }
        public async Task SetInformationProduct()
        {
            ProductName = _objectInventory.ProductName;
            TotalStock = _objectInventory.TotalStock;
            MinimumStock = _objectInventory.MiniumStock;
            TransactionsProductInformation = await _productInformationService.GetAllProductTransaction(_objectInventory.IdProduct);
        }
    }
}
