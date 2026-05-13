using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using Almacen_Sistema.Services.Data.Movements.CurrentStock;
using Almacen_Sistema.Services.Movements.Contracts.Panels;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.Services.Movements.Implementations
{
    public class CurrentStockService : ICurrentStocksService
    {
        public CurrentStockService() 
        {
            _currentStocksRepository = new CurrentStocksRepository();
        }

        private readonly ICurrentStocksRepository _currentStocksRepository;
        public Task<List<CurrentStock?>> GetAllStocksProducts()
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> StockAdjustmentAsync(TransactionHistory transaction)
        {
            var StockProduct = await _currentStocksRepository.GetByCurrentStockIdProductAsync(transaction.IdProduct);

            if (StockProduct != null)
            {
                switch(transaction.TypeMovement)
                {
                    case TypeMovementTransaction.Entrada:
                        StockProduct.TotalAmount += transaction.Quantity;
                        break;
                    case TypeMovementTransaction.Salida:
                        StockProduct.TotalAmount -= transaction.Quantity;
                        break;
                }
                var result = await _currentStocksRepository.UpdateStockMovementAsync(StockProduct);

                if (result)
                {
                    return ServiceResult.Success("Movimiento confirmado y ajuste de stock con éxito.");
                }
                return ServiceResult.Failure("Error al ajustar el stock.");
            }
            else
            {
                var NewStock = await _currentStocksRepository.InsertCurrentStockAsync(
                    new CurrentStock(transaction.IdProduct, transaction.Quantity));
                if(NewStock)
                {
                    return ServiceResult.Success("Movimiento confirmado y ajuste de stock con éxito.");
                }
                return ServiceResult.Failure("Error al ajustar el stock.");
            }
        }
        public async Task<List<CurrentStockRowPanel?>> GetAllStockExitMovement()
        {
            return await _currentStocksRepository.SelectAllRowStocks();
        }
    }
}
