using Almacen_Sistema.MVVM.Models.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsModel = Almacen_Sistema.MVVM.Models.Movements.Tickets;

namespace Almacen_Sistema.Services.Data.Movements.Tickets
{
    public interface ITicketsRepository
    {
        public Task<List<TicketsModel>?> SelectAllTickets();
        public Task<TicketsModel?> SelectTicketByIdAsync(int id);
        public Task<bool> InsertTicketAsync(TicketsModel ticket);
        public Task<bool> UpdateTicketAsync(TicketsModel ticket);
        public Task<bool> DeleteTicketAsync(int id);
    }
}
