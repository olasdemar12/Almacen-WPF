using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Inventory
{
    public interface IInventoryService
    {
        public Task<List<InventoryRow>?> GetInventoryRows();
        public Task<ServiceResult> UpdateMinumStockProduct(decimal MinumStock);
    }
}
