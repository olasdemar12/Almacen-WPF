using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Documents.ModuleServices.Invetorys
{
    public interface IInventorysDocumentService
    {
        public Task<List<DocumentInventoryRow>> GetDocumentInventoryRowsAsync();

        public Task<ServiceResult> ExportMovementsDocument();
    }
}
