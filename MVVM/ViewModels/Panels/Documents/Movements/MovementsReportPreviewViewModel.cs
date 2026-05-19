using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using Almacen_Sistema.Services.Documents.ModuleServices.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Movements
{
    public partial class MovementsReportPreviewViewModel : ObservableObject 
    {
        public MovementsReportPreviewViewModel()
        {
            _movementsDocumentService = new MovementsDocumentService();
            DocumentsViewModel._filterEventService.OnIdCategorySelected += SetIdCategoryFilter;
            DocumentsViewModel._filterEventService.OnStartDateTime += SetStartDateTime; 
            DocumentsViewModel._filterEventService.OnEndDateTime += SetEndDateTime;

            DocumentsViewModel._filterEventService.OnGenerateReport += GenerateReport;
            DocumentsViewModel._filterEventService.OnGenerateReport -= GenerateReport;
        }

        private readonly IMovementsDocumentService _movementsDocumentService;
    }
}
