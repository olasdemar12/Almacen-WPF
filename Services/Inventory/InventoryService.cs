using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.Services.Data.Inventory;
using Almacen_Sistema.Services.Data.Movements.TransactionHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Almacen_Sistema.Services.Inventory
{
    public class InventoryService : IInventoryService, IProductInformationService
    {
        public InventoryService()
        {
           _inventoryRepository = new InventoryRepository();
            _productTransactionService = new TransactionRepository();
        }

        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductTransactionInformationService _productTransactionService;

        public async Task<List<InventoryRow>?> GetInventoryRows()
        {
            var result = await _inventoryRepository.SelectAllInventoryRows();
            if(result == null)
            {
                return new List<InventoryRow>();
            }
            return result;
        }
        public async Task<List<TransactionInventoryProductRow>?> GetAllProductTransaction(int IdProduct)
        {
            var result = await _productTransactionService.SelectAllTransaction();
            if (result != null)
            {
                List<TransactionInventoryProductRow> ProductTransactions = new List<TransactionInventoryProductRow>();
                foreach (var row in result)
                {
                    if (row.IdProduct == IdProduct && row.Confirmed)
                    {
                       ProductTransactions.Add(new TransactionInventoryProductRow
                        {
                            Amount = row.Quantity,
                            RegisterDate = row.RegisterDate,
                            TypeMovement = row.TypeMovement
                        });
                    }
                }
                return ProductTransactions;
            }
            return null;
        }

        public async Task<ServiceResult> UpdateMinumStockProduct(int IdProduct, decimal MinumStock)
        {
            var result = await _inventoryRepository.UpdateStockMinumInventory(IdProduct, MinumStock);
            if (result)
                return ServiceResult.Success("Stock minimo actualizado");
            else
                return ServiceResult.Failure("No se pudo actualizar el stock minimo del producto seleccionado");
        }
    }
}
