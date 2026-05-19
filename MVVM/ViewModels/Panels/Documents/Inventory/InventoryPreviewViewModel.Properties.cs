using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Inventory
{
    public partial class InventoryPreviewViewModel
    {
        //Modelo de la vista
        [ObservableProperty]
        private DateTime? _startDate;
        [ObservableProperty]
        private DateTime? _endDate;
        [ObservableProperty]
        private int? _idCategorySelected;
        [ObservableProperty]
        private List<DocumentInventoryRow> rowsInventory;
        [ObservableProperty]
        private ICollectionView rowsInventoryView;

        //Propiedades de funcionamiento:
        [ObservableProperty]
        private bool _isLoading;
    }
}
