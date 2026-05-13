using Almacen_Sistema.MVVM.Models.Movements.CurrentStocks;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Movements.ProductSelectionExitMovement
{
    public partial class ProductSelectionExitViewModel
    {
        //Propiedades del Modelo de Vista
        [ObservableProperty]
        private string _searchTextProduct;
        [ObservableProperty]
        private string? _selectedCategory;
        [ObservableProperty]
        private List<Category> _categories;
        [ObservableProperty]
        private CurrentStockRowPanel? _selectedStockRow;
        [ObservableProperty]
        private ObservableCollection<CurrentStockRowPanel?> _rowPanelItems;
        [ObservableProperty]
        private ICollectionView _rowPanelItemsView;

        //Propiedades para el funcionamiento de la vista
        [ObservableProperty]
        private bool _isLoadingStock = false;
        [ObservableProperty]
        private bool _isLoadingResults = false;
        [ObservableProperty]
        private bool _isResultsEmpty = false;
        private CancellationTokenSource? _searchCancellationTokenSource;

    }
}
