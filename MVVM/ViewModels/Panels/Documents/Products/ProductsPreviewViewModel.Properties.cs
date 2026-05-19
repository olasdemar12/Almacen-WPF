using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Products
{
    public partial class ProductsPreviewViewModel
    {
        [ObservableProperty]
        private int? _idCategorySelected;
        [ObservableProperty]
        private List<DocumentProductRow> rowsProduct;
        [ObservableProperty]
        private ICollectionView rowsProductView;

        //Propiedades de funcionamiento:
        [ObservableProperty]
        private bool _isLoading;
    }
}
