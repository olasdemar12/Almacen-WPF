using TransactionHistoryModel = Almacen_Sistema.MVVM.Models.Movements.TransactionHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Almacen_Sistema.Services.Data.Movements.TransactionHistory
{
    public interface ITransactionRepository
    {
        public Task<List<TransactionHistoryModel>> SelectAllTransaction();
        public Task<bool> InsertTransactionRepository(TransactionHistoryModel transaction);
        public Task<bool> UpdateTransactionRepository(TransactionHistoryModel transaction);
        public Task<bool> DeleteTransactionRepository(int IdTransaction);

    }
}
