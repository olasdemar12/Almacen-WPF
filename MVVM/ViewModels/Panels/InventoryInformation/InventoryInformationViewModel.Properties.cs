using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.InventoryInformation
{
    public partial class InventoryInformationViewModel
    {
        //Modelo de la visa:
        [ObservableProperty]
        private string _productName;
        [ObservableProperty]
        private decimal _totalStock;
        [ObservableProperty]
        [Range(4.99, double.MaxValue, ErrorMessage = "La cantidad minima del stock es de 5")]
        private decimal _minimumStock;
        [ObservableProperty]
        private List<TransactionInventoryProductRow>? _transactionsProductInformation;

        private InventoryRow _objectInventory;
        private CancellationTokenSource? _updateMinimumStockCancellationTokenSource;
    }
}
