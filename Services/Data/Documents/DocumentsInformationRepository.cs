using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.Services.Data.Documents.Movements;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.Services.Data.Documents
{
    public class DocumentsInformationRepository : IReadRowMovementsDocument
    {
        async Task<List<RowMovementsProductsDocument>?> IReadRowMovementsDocument.SelectAllInformationDocument()
        {
            SqliteConnection? connection = null;
            List<RowMovementsProductsDocument>? rows = new List<RowMovementsProductsDocument>();
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
SELECT t.IdProduct,p.IdCategory,t.RegisterDate,t.TypeMovement,p.ProductName,c.CategoryName,t.Quantity
FROM 
TransactionHistory as t INNER JOIN Products as p 
ON p.IdProduct = t.IdProduct
LEFT JOIN Categorys as c
ON p.IdCategory = c.IdCategory 
ORDER BY c.CategoryName ASC,t.TypeMovement ASC, t.Quantity DESC
";
                using SqliteDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var date = DateTime.ParseExact(
                        reader.GetString(2),
                        "dd-MM-yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture
                        );
                    rows.Add(new RowMovementsProductsDocument(
                        reader.GetInt32(0),
                        await reader.IsDBNullAsync(1) ? 0 : reader.GetInt32(1),
                        date,
                        reader.GetString(3) == "Entrada" ? TypeMovementTransaction.Entrada : TypeMovementTransaction.Salida,
                        reader.GetString(4),
                        await reader.IsDBNullAsync(5) ? "Sin Asignar" : reader.GetString(5),
                        reader.GetDecimal(6)
                        ));
                }

                await reader.CloseAsync();
                return rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un Error al consultar los movimientos registrados", MessageBoxButton.OK, MessageBoxImage.Error);
                return rows;
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
