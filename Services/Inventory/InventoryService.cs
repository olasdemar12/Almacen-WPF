using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.Services.Data.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Almacen_Sistema.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public InventoryService()
        {
           _inventoryRepository = new InventoryRepository(); 
        }

        private readonly IInventoryRepository _inventoryRepository;

        public async Task<List<InventoryRow>?> GetInventoryRows()
        {
            var result = await _inventoryRepository.SelectAllInventoryRows();
            if(result == null)
            {
                return new List<InventoryRow>();
            }
            return result;
        }

        public Task<ServiceResult> UpdateMinumStockProduct(decimal MinumStock)
        {
            throw new NotImplementedException();
        }
    }
}
