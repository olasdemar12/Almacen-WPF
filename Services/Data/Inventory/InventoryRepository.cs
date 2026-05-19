using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.Services.Data.Inventory
{
    public class InventoryRepository : IInventoryRepository
    {
        public async Task<List<InventoryRow>?> SelectAllInventoryRows()
        {
            SqliteConnection? connection = null;
            List<InventoryRow>? inventoryRows = new List<InventoryRow>();
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
SELECT * FROM (
SELECT * FROM (
SELECT S.IdStock,S.IdProduct,C.IdCategory,P.ProductName,C.CategoryName,S.TotalAmount,S.MiniumStock, T.RegisterDate
FROM CurrentStocks as S INNER JOIN Products as P 
ON S.IdProduct = P.IdProduct
LEFT JOIN Categorys as C 
ON P.IdCategory = C.IdCategory
INNER JOIN TransactionHistory as T
ON P.IdProduct = T.IdProduct WHERE T.Confirmed = 1 ORDER BY T.RegisterDate DESC
) GROUP BY ProductName
) ORDER BY CategoryName ASC, TotalAmount DESC
";
                using SqliteDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    var Status = GetStateStock(reader.GetDecimal(6), reader.GetDecimal(5));
                    var date = DateTime.ParseExact(
                        reader.GetString(7),
                        "dd-MM-yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture
                        );

                    inventoryRows.Add(new InventoryRow(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        await reader.IsDBNullAsync(2) ? 0 : reader.GetInt32(2),
                        reader.GetString(3),
                        await reader.IsDBNullAsync(4) ? "Sin Asignar" : reader.GetString(4),
                        reader.GetDecimal(5),
                        reader.GetDecimal(6),
                        Status,
                        date
                        ));
                }

                await reader.CloseAsync();
                return inventoryRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un Error al consultar todos los inventarios existentes", MessageBoxButton.OK, MessageBoxImage.Error);
                return inventoryRows;
            }
            finally
            {
                if (connection != null)
                {

                    await connection.CloseAsync();
                }
            }
        }

        public async Task<bool> UpdateStockMinumInventory(int IdProduct, decimal MinumStock)
        {
            SqliteConnection? connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"UPDATE CurrentStocks SET MiniumStock = $MinumStock WHERE IdProduct = $IdProduct";
                command.Parameters.AddWithValue("$MinumStock", MinumStock);
                command.Parameters.AddWithValue("$IdProduct", IdProduct);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Ocurrio un Error al actualizar el stock minimo del producto {IdProduct}", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private StateStockInventory GetStateStock(decimal MiniumStock, decimal TotalStock)
        {
            if (TotalStock == 0)
            {
                return StateStockInventory.Agotado;
            }
            else if(MiniumStock >= TotalStock)
            {
                return StateStockInventory.Bajo;
            }
            return StateStockInventory.Normal;
        }
    }
}
