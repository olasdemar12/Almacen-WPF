using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentStockModel = Almacen_Sistema.MVVM.Models.Movements.CurrentStocks.CurrentStock;
namespace Almacen_Sistema.Services.Data.Movements.CurrentStock
{
    public interface ICurrentStocksRepository
    {
        public Task<List<CurrentStockModel?>> SelectAllCurrentStocksAsync();
        public Task<bool> InsertCurrentStockAsync(CurrentStockModel currentStock);
        public Task<CurrentStockModel?> GetByCurrentStockIdProductAsync(int IdProduct);
        public Task<bool> UpdateStockMovementAsync(CurrentStockModel stockModel);
        public Task<List<CurrentStockRowPanel?>> SelectAllRowStocks();
    }
}
