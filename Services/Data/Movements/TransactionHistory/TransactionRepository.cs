using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.Services.Inventory;
using Microsoft.Data.Sqlite;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TransactionHistoryModel = Almacen_Sistema.MVVM.Models.Movements.TransactionHistory;
namespace Almacen_Sistema.Services.Data.Movements.TransactionHistory
{
    public class TransactionRepository : ITransactionRepository, IProductTransactionInformationService
    {
        public async Task<List<TransactionHistoryModel>> SelectAllTransaction()
        {
            SqliteConnection? connection = null;
            List<TransactionHistoryModel>? transactions = new List<TransactionHistoryModel>();
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
SELECT t.IdMovement,t.IdProduct,t.RegisterDate,t.TypeMovement,p.ProductName,t.Quantity,t.notes,t.Confirmed 
FROM TransactionHistory as t INNER JOIN Products as p ON p.IdProduct = t.IdProduct
";
                using SqliteDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var date = DateTime.ParseExact(
                        reader.GetString(2),
                        "dd-MM-yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture
                    );

                    transactions.Add(new TransactionHistoryModel(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        date,
                        Enum.Parse<TypeMovementTransaction>(reader.GetString(3)),
                        reader.GetString(4),
                        reader.GetDecimal(5),
                        reader.GetString(6),
                        reader.GetBoolean(7)
                    ));
                }

                await reader.CloseAsync();
                return transactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un Error al consultar los registros de Transacciones", MessageBoxButton.OK, MessageBoxImage.Error);
                return transactions;
            }
            finally
            {
                if (connection != null)
                {

                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> InsertTransactionRepository(TransactionHistoryModel transaction)
        {
            SqliteConnection connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
INSERT INTO TransactionHistory (IdProduct,TypeMovement,Quantity,RegisterDate,Notes,Confirmed)
VALUES ($IdProduct,$TypeMovement,$Quantity,$RegisterDate,$Notes,$Confirmed);
SELECT last_insert_rowid();
";
                command.Parameters.AddWithValue("$IdProduct", transaction.IdProduct);
                command.Parameters.AddWithValue("$TypeMovement", transaction.TypeMovement.ToString());
                command.Parameters.AddWithValue("$Quantity", transaction.Quantity);
                command.Parameters.AddWithValue("$RegisterDate", transaction.RegisterDate.ToString("dd-MM-yyyy hh:mm:ss"));
                command.Parameters.AddWithValue("$Notes", transaction.Notes);
                command.Parameters.AddWithValue("$Confirmed", Convert.ToInt32(transaction.Confirmed));

                long? IdTransaction = (long?)await command.ExecuteScalarAsync() ?? 0L;
                transaction.IdMovement = (int)IdTransaction;
                return IdTransaction > 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurrio un Error al Insertar el Movimiento", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }
        public async Task<bool> UpdateTransactionRepository(TransactionHistoryModel transaction)
        {
            SqliteConnection connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
UPDATE TransactionHistory
SET 
Quantity = $Quantity,
Notes = $Notes,
Confirmed = $Confirmed
WHERE IdMovement = $IdMovement
";
                command.Parameters.AddWithValue("$Quantity", transaction.Quantity);
                command.Parameters.AddWithValue("$Notes", transaction.Notes);
                command.Parameters.AddWithValue("$IdMovement", transaction.IdMovement);
                command.Parameters.AddWithValue("$Confirmed", Convert.ToInt32(transaction.Confirmed));

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Ocurio un Error al Actualizar el registro del Producto {transaction.ProductName}", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> DeleteTransactionRepository(int IdTransaction)
        {
            SqliteConnection? connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM TransactionHistory WHERE IdMovement = $IdMovement";
                command.Parameters.AddWithValue("$IdMovement", IdTransaction);

                int rowsAffects = await command.ExecuteNonQueryAsync();
                return rowsAffects > 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un error al intentar eliminar el movimiento", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
