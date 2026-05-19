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
        }

        private readonly IMovementsDocumentService _movementsDocumentService;
    }
}
