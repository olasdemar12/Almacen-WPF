using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Inventory
{
    public interface IProductInformationService
    {
        public Task<List<TransactionInventoryProductRow>?> GetAllProductTransaction(int IdProduct);

        public Task<ServiceResult> UpdateMinumStockProduct(int IdProduct, decimal MinumStock);
    }

    public interface IProductTransactionInformationService
    {
        public Task<List<TransactionHistory>> SelectAllTransaction();
    }
}
