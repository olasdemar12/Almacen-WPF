using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Data.Inventory
{
    public interface IInventoryRepository
    {
        public Task<List<InventoryRow>?> SelectAllInventoryRows();
        public Task<bool> UpdateStockMinumInventory(decimal MinumStock);
        //TODO: Implementacion de metodo para obtener todas las transacciones de un producto en especifico.
    }
}
