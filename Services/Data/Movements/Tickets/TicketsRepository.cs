using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicketsModel = Almacen_Sistema.MVVM.Models.Movements.Tickets;

namespace Almacen_Sistema.Services.Data.Movements.Tickets
{
    public class TicketsRepository : ITicketsRepository
    {
        public Task<bool> DeleteTicketAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertTicketAsync(TicketsModel ticket)
        {
            SqliteConnection? connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
INSERT INTO Tickets (IdProduct,RegisterDate,AmountReceived,Notes)
VALUES ($IdProduct,$RegisterDate,$AmountReceived,$Notes)
";
                command.Parameters.AddWithValue("$IdProduct", ticket.IdProduct);
                command.Parameters.AddWithValue("$RegisterDate", ticket.RegisterDate);
                command.Parameters.AddWithValue("AmountReceived", ticket.AmountRegister);
                command.Parameters.AddWithValue("$Notes", ticket.Notes);

                long? IdStock = (long?)await command.ExecuteScalarAsync() ?? 0L;
                ticket.IdTicket = (int)IdStock;
                return IdStock > 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al intentar registra el movimiento de entrada", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }

        public Task<List<TicketsModel>?> SelectAllTickets()
        {
            throw new NotImplementedException();
        }

        public Task<TicketsModel?> SelectTicketByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTicketAsync(TicketsModel ticket)
        {
            throw new NotImplementedException();
        }
    }
}
