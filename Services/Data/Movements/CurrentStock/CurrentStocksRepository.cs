using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using CurrentStockModel = Almacen_Sistema.MVVM.Models.Movements.CurrentStocks.CurrentStock;
namespace Almacen_Sistema.Services.Data.Movements.CurrentStock
{
    public class CurrentStocksRepository : ICurrentStocksRepository
    {
        public async Task<CurrentStockModel?> GetByCurrentStockIdProductAsync(int IdProduct)
        {
            SqliteConnection? connection = null;
            CurrentStockModel? currentStock = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM CurrentStocks WHERE IdProduct = $IdProduct";
                command.Parameters.AddWithValue("$IdProduct", IdProduct);
                using SqliteDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    currentStock = new CurrentStockModel(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetDecimal(2),
                        reader.GetDecimal(3)
                    );
                }

                await reader.CloseAsync();
                return currentStock;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al intentar obtener informacion del stock consultado", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> UpdateStockMovementAsync(CurrentStockModel stockModel)
        {
            SqliteConnection? connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"UPDATE CurrentStocks SET TotalAmount = $TotalAmount WHERE IdStock = $IdStock";
                command.Parameters.AddWithValue("$TotalAmount", stockModel.TotalAmount);
                command.Parameters.AddWithValue("$IdStock", stockModel.IdStock);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Ocurrio un Error al actualizar el stock del producto {stockModel.IdProduct}", MessageBoxButton.OK, MessageBoxImage.Error);
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
        public async Task<bool> InsertCurrentStockAsync(CurrentStockModel currentStock)
        {
            SqliteConnection? connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
INSERT INTO CurrentStocks (IdProduct,TotalAmount)
VALUES ($IdProduct,$TotalAmount);
SELECT last_insert_rowid();";
                command.Parameters.AddWithValue("$IdProduct", currentStock.IdProduct);
                command.Parameters.AddWithValue("$TotalAmount", currentStock.TotalAmount);

                long? IdStock = (long?)await command.ExecuteScalarAsync() ?? 0L;
                currentStock.IdStock = (int)IdStock;
                return IdStock > 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al intentar insertar el stock", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }
        public async Task<List<CurrentStockRowPanel?>> SelectAllRowStocks()
        {
            SqliteConnection? connection = null;
            List<CurrentStockRowPanel?> currentStocks = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
SELECT S.IdStock,S.IdProduct,P.ProductName,C.CategoryName,S.TotalAmount FROM CurrentStocks as S 
INNER JOIN Products as P ON S.IdProduct = P.IdProduct INNER JOIN Categorys as C ON P.IdCategory = C.IdCategory
WHERE S.TotalAmount > 0 ORDER BY C.CategoryName ASC,S.TotalAmount DESC
";
                using SqliteDataReader reader = await command.ExecuteReaderAsync();
                currentStocks = new List<CurrentStockRowPanel?>();
                while (reader.Read())
                {
                    currentStocks.Add(new CurrentStockRowPanel(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDecimal(4)
                        ));
                }

                await reader.CloseAsync();
                return currentStocks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurrio un Error al intentar obtener los stokcs disponibles", MessageBoxButton.OK, MessageBoxImage.Error);
                return currentStocks;
            }
            finally
            {
                if (connection != null)
                {

                    await connection.CloseAsync();
                }
            }
        }
        public Task<List<CurrentStockModel?>> SelectAllCurrentStocksAsync()
        {
            MessageBox.Show("No se ha implementado la funcionalidad de obtener todos los stocks actuales", "Funcionalidad no implementada", MessageBoxButton.OK, MessageBoxImage.Information);
            return null;
        }
    }
}
