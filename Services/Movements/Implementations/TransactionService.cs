using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using Almacen_Sistema.Services.Data.Movements.TransactionHistory;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Movements.Contracts.Panels;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Movements.Implementations
{
    public class TransactionService : ITransactionService
    {
        public TransactionService() 
        { 
            _transactionRepository = new TransactionRepository();
            _currentStocksService = new CurrentStockService();
        }

        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentStocksService _currentStocksService;

        public async Task<List<TransactionHistory>> GetAllTransactionAsync()
        {
            var transactions = await _transactionRepository.SelectAllTransaction();
            if (transactions == null)
            {
                return new List<TransactionHistory>();
            }
            return transactions;
        }

        public async Task<ServiceResult> AddTransactionAsync(int idproduct,string productName, TransactionHistory transaction, decimal totalAmount)
        {
            transaction.IdProduct = idproduct;
            transaction.Quantity = totalAmount;
            transaction.ProductName = productName;

            if (await _transactionRepository.InsertTransactionRepository(transaction))
            {
                return ServiceResult.Success("Movimiento registrado correctamente.");
            }
            else
            {
                return ServiceResult.Failure("No se pudo registrar el movimiento.");
            }
        }

        public async Task<ServiceResult> EditTransactionAsync(TransactionHistory transaction)
        {
            if(await _transactionRepository.UpdateTransactionRepository(transaction))
            {
                return ServiceResult.Success("Movimiento actualizado correctamente.");
            }
            else
            {
                return ServiceResult.Failure("No se pudo actualizar el movimiento.");
            }
        }

        public async Task<ServiceResult> DeleteTransactionAsync(int IdTransaction)
        {
           if(await _transactionRepository.DeleteTransactionRepository(IdTransaction))
           {
               return ServiceResult.Success("Movimiento eliminado correctamente.");
           }
           else
           {
               return ServiceResult.Failure("No se pudo eliminar el movimiento.");
           }
        }

        public async Task ConfirmTransactionAsync(TransactionHistory transaction)
        {
            //Falta actualizar que el movimiento o transaccion se ha confirmado, para que no se pueda volver a confirmar o eliminar
            var resultStock = await _currentStocksService.StockAdjustmentAsync(transaction);
            var type = resultStock.IsSuccess ? NotificationType.Success : NotificationType.Error;

            var NotificationTask = NotificationServiceControl.Instance.ShowNotification(resultStock.Message, type);
            var UpdateTransactionTask = EditTransactionAsync(transaction);

            await Task.WhenAll(NotificationTask, UpdateTransactionTask);
        }

        public async Task<decimal> GetTotalAmountStockByIdProduct(int IdProduct)
        {
            var totalAmount = await _currentStocksService.GetStockByIdProduct(IdProduct);
            if(totalAmount != null)
                return totalAmount.TotalAmount;
            return 0.00m;
        }
    }
}
