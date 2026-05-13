using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.Services.Category.Implementations;
using MVVM.Models.Category;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Composition
{
    public static class StockMasterEvents
    {
        public static event CategoryChanges CategoryChanges;
        public static void OnCategoryChanges(CategoryActionChanges action,Category category)
        {
            CategoryChanges?.Invoke(action,category);
        }

        //Evento de reaccion para cuando se elimina un producto depues de buscarse:
        public delegate void DeleteProductSearch(Product product);
        public static event DeleteProductSearch ProductChanges;
        public static async Task OnDeleteProductSearch(Product product)
        {
            ProductChanges?.Invoke(product);
        }
    }

    public sealed class EventsAplicationStockMaser
    {
        private static readonly Lazy<EventsAplicationStockMaser> _instance =
            new(() => new EventsAplicationStockMaser());
        public static EventsAplicationStockMaser Instance => _instance.Value;

        private readonly IMovementEvents _movementEvents;
        public IMovementEvents MovementEvents => _movementEvents;

        private EventsAplicationStockMaser()
        {
            _movementEvents = new MovementEvents();
        }
    }
}
