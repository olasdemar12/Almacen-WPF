using Almacen_Sistema.BaseDirectory;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OutputsModel = Almacen_Sistema.MVVM.Models.Movements.Outputs;

namespace Almacen_Sistema.Services.Data.Movements.Outputs
{
    public class OutputsRepository : IOutputsRepository
    {
        public Task<bool> DeleteOutputAsync(int idOutput)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOutputAsync(OutputsModel output)
        {
            SqliteConnection? connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
INSERT INTO Outputs 
(IdProduct, RegisterDate, AmountWithDrawn, Notes)
VALUES ($IdProduct, $RegisterDate, $AmountWithDrawn, $Notes)
";
                command.Parameters.AddWithValue("$IdProduct", output.IdProduct);
                command.Parameters.AddWithValue("$RegisterDate", output.RegisterDate);
                command.Parameters.AddWithValue("$AmountWithDrawn", output.AmountWithDrawn);
                command.Parameters.AddWithValue("$Notes", output.Notes);

                long? IdStock = (long?)await command.ExecuteScalarAsync() ?? 0L;
                output.IdOutput = (int)IdStock;
                return IdStock > 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al intentar registra el movimiento de salida", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }

        public Task<List<OutputsModel>?> SelectAllOutputs()
        {
            throw new NotImplementedException();
        }

        public Task<OutputsModel?> SelectOutputByIdAsync(int idOutput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOutputAsync(OutputsModel output)
        {
            throw new NotImplementedException();
        }
    }
}
