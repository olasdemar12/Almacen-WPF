using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;
namespace Almacen_Sistema.MVVM.ViewModels.Pages.Inventory
{
    public partial class InventoryViewModel
    {
        //Modelo de la vista:
        [ObservableProperty]
        private string _searchTextProduct;
        [ObservableProperty]
        private int? _idCategorySelected = null;
        [ObservableProperty]
        private StateStockInventory? _selectedStateStock;
        [ObservableProperty]
        private DateTime? _starDate;
        [ObservableProperty]
        private DateTime? _endDate;
        [ObservableProperty]
        private ObservableCollection<InventoryRow> _inventoryItems;
        [ObservableProperty]
        private ICollectionView _inventoryItemsView;
        [ObservableProperty]
        private InventoryRow? _selectedInventoryRow;
        [ObservableProperty]
        private int _selectedProductIndex = -1;

        private CancellationTokenSource? _searchCancellationTokenSource;
        public IReadOnlyList<StateStockInventory> StateStockOptions { get; } 
        public List<CategoryModel> Categories { get; set; }
    }
}
