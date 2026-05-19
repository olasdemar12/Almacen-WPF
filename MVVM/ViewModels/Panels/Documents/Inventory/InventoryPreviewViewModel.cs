using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using Almacen_Sistema.Services.Documents.ModuleServices.Invetorys;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Inventory
{
    public partial class InventoryPreviewViewModel : ObservableValidator
    {
        public InventoryPreviewViewModel()
        {
            _invetoryDocumentService = new InventorysDocumentService();
            DocumentsViewModel._filterEventService.OnIdCategorySelected -= SetIdCategoryFilter;
            DocumentsViewModel._filterEventService.OnStartDateTime -= SetStartDateTime;
            DocumentsViewModel._filterEventService.OnEndDateTime -= SetEndDateTime;
            DocumentsViewModel._filterEventService.OnGenerateReport -= GenerateReport;
            DocumentsViewModel._filterEventService.OnIdCategorySelected += SetIdCategoryFilter;
            DocumentsViewModel._filterEventService.OnStartDateTime += SetStartDateTime;
            DocumentsViewModel._filterEventService.OnEndDateTime += SetEndDateTime;
            DocumentsViewModel._filterEventService.OnGenerateReport += GenerateReport;
        }

        private readonly IInventorysDocumentService _invetoryDocumentService;
    }
}
