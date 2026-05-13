using Almacen_Sistema.MVVM.ViewModels.Pages;
using Almacen_Sistema.Services.Movements.Contracts.Panels;
using Almacen_Sistema.Services.Product.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Movements.ProductSelectionExitMovement
{
    public partial class ProductSelectionExitViewModel: ObservableObject
    {
        public ProductSelectionExitViewModel()
        {
            _currentStocksService = MovementsViewModel.MovementService.PanelServices.CurrentStocksService;
            _productService = MovementsViewModel.MovementService.PanelServices.ProductService;
        }

        ICurrentStocksService _currentStocksService;
        IProductService _productService;
    }
}
