using Almacen_Sistema.MVVM.Models.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Composition.EventsDefinitions.Movements
{
    public enum TypeActionMovementChanges
    {
        Add,
        Update,
        Delete
    }
    public interface IMovementEvents
    {
        public delegate Task TransactionLogRefresher(TypeActionMovementChanges typeAction, TransactionHistory transaction);
        public event TransactionLogRefresher? OnTransactionLogChanges;
        public Task OnTransactionLogChangesInvoke(TypeActionMovementChanges typeAction, TransactionHistory transaction);
    }
    public class MovementEvents : IMovementEvents
    {
        public event IMovementEvents.TransactionLogRefresher? OnTransactionLogChanges;

        public async Task OnTransactionLogChangesInvoke(TypeActionMovementChanges typeAction, TransactionHistory transaction)
        {
            if (OnTransactionLogChanges != null)
            {
                await OnTransactionLogChanges.Invoke(typeAction, transaction);
            }
        }
    }
}
