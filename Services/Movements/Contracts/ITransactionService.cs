using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Almacen_Sistema.Services.Movements.Contracts
{
    public interface ITransactionService
    {
        Task<List<TransactionHistory>> GetAllTransactionAsync();
        Task<ServiceResult> AddTransactionAsync(int idproduct, string productName, TransactionHistory transaction, decimal totalAmount);
        Task<ServiceResult> EditTransactionAsync(TransactionHistory transaction);
        Task<ServiceResult> DeleteTransactionAsync(int IdTransaction);
        Task ConfirmTransactionAsync(TransactionHistory transaction);
    }
}
