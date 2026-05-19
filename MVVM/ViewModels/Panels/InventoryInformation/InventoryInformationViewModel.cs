using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.Services.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.InventoryInformation
{
    public partial class InventoryInformationViewModel : ObservableValidator
    {
        public InventoryInformationViewModel(InventoryRow? rowSelection)
        {
            _productInformationService = new InventoryService();
            if (rowSelection != null)
                _objectInventory = rowSelection;
        }

        private readonly IProductInformationService _productInformationService;
    }
}
