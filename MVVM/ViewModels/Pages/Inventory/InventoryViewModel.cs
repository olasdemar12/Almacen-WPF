using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.Services.Data.CategoryDate;
using Almacen_Sistema.Services.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Pages.Inventory
{
    public partial class InventoryViewModel : ObservableObject
    {
        public InventoryViewModel()
        {
            _inventoryService = new InventoryService();
            StateStockOptions = Enum.GetValues(typeof(StateStockInventory)).Cast<StateStockInventory>().ToList();
            _categoryRepository = new CategoryRepository();
        }

        private readonly IInventoryService _inventoryService;
        private readonly ICategoryRepository _categoryRepository;
    }
}
