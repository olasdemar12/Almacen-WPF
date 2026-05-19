using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using Almacen_Sistema.Services.Documents.ModuleServices.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Almacen_Sistema.Composition.EventsDefinitions.Documents.IDocumentEvents;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Products
{
    public partial class ProductsPreviewViewModel : ObservableValidator
    {
        public ProductsPreviewViewModel()
        {
            _productDocumentService = new ProductDocumentService();
            DocumentsViewModel._filterEventService.OnIdCategorySelected -= SetIdCategoryFilter;
            DocumentsViewModel._filterEventService.OnGenerateReport -= GenerateReport;
            DocumentsViewModel._filterEventService.OnIdCategorySelected += SetIdCategoryFilter;
            DocumentsViewModel._filterEventService.OnGenerateReport += GenerateReport;
        }

        private readonly IProductDocumentService _productDocumentService;

    }
}
