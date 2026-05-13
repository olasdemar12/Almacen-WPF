using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Movements.Contracts.Panels
{
    public interface ICurrentStocksService
    {
        public Task<ServiceResult> StockAdjustmentAsync(TransactionHistory transaction);
        public Task<List<CurrentStock?>> GetAllStocksProducts();
        public Task<List<CurrentStockRowPanel?>> GetAllStockExitMovement();
    }
}
