using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.Services.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Pages.Documents
{
    public enum ModuleReport
    {
        Movimientos,
        Inventario,
        Productos
    }
    public partial class DocumentsViewModel : ObservableValidator
    {
        public DocumentsViewModel()
        {
            ModuleReportsList = Enum.GetValues(typeof(ModuleReport)).Cast<ModuleReport>().ToList();
            _documentService = new DocumentService();
        }

        private readonly IDocumentService _documentService;

    }
}
