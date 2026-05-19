using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Movements
{
    public partial class MovementsReportPreviewViewModel
    {
        //Modelo de la vista
        [ObservableProperty]
        private DateTime? _startDate;
        [ObservableProperty]
        private DateTime? _endDate;
        [ObservableProperty]
        private int? _idCategorySelected;
        [ObservableProperty]
        private List<RowMovementsProductsDocument> rowsMovement;
        [ObservableProperty]
        private ICollectionView rowsMovementView;

        //Propiedades de funcionamiento:
        [ObservableProperty]
        private bool _isLoading;
    }
}
