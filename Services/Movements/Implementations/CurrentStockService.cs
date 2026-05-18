using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using Almacen_Sistema.Services.Data.Movements.CurrentStock;
using Almacen_Sistema.Services.Data.Movements.Outputs;
using Almacen_Sistema.Services.Data.Movements.Tickets;
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
            _ticketsRepository = new TicketsRepository();
            _outputsRepository = new OutputsRepository();
        }

        private readonly ICurrentStocksRepository _currentStocksRepository;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IOutputsRepository _outputsRepository;

        public Task<List<CurrentStock?>> GetAllStocksProducts()
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> StockAdjustmentAsync(TransactionHistory transaction)
        {
            var StockProduct = await _currentStocksRepository.GetByCurrentStockIdProductAsync(transaction.IdProduct);

            if (StockProduct != null)
            {
                switch (transaction.TypeMovement)
                {
                    case TypeMovementTransaction.Entrada:
                        await _ticketsRepository.InsertTicketAsync(
                            new Tickets(transaction.IdProduct, transaction.RegisterDate, transaction.Quantity, transaction.Notes));
                        StockProduct.TotalAmount += transaction.Quantity;
                        break;
                    case TypeMovementTransaction.Salida:
                        await _outputsRepository.InsertOutputAsync(
                            new Outputs(transaction.IdProduct, transaction.RegisterDate, transaction.Quantity, transaction.Notes));
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
                switch (transaction.TypeMovement)
                {
                    case TypeMovementTransaction.Entrada:
                        await _ticketsRepository.InsertTicketAsync(
                            new Tickets(transaction.IdProduct, transaction.RegisterDate, transaction.Quantity, transaction.Notes));
                        break;
                    case TypeMovementTransaction.Salida:
                        await _outputsRepository.InsertOutputAsync(
                           new Outputs(transaction.IdProduct, transaction.RegisterDate, transaction.Quantity, transaction.Notes));
                        break;
                }

                if (NewStock)
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
        public async Task<CurrentStock?> GetStockByIdProduct(int IdProduct)
        {
           return await _currentStocksRepository.GetByCurrentStockIdProductAsync(IdProduct);
        }
    }
}
