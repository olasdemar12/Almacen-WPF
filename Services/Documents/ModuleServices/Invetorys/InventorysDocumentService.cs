using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.Services.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Documents.ModuleServices.Invetorys
{
    public class InventorysDocumentService : IInventorysDocumentService
    {
        public InventorysDocumentService() 
        { 
            _inventoryService = new InventoryService();
            rowsList = new List<DocumentInventoryRow>();
        }

        private readonly IInventoryService _inventoryService;
        private List<DocumentInventoryRow> rowsList;

        public async Task<List<DocumentInventoryRow>> GetDocumentInventoryRowsAsync()
        {
            var result = await _inventoryService.GetInventoryRows();
            if(result != null)
                foreach (var row in result)
                {
                    rowsList.Add(new DocumentInventoryRow(row.IdCategory,row.ProductName,row.CategoryName,row.TotalStock,row.LastMovement));
                }
            return rowsList;
        }

        public Task<ServiceResult> ExportMovementsDocument()
        {
            throw new NotImplementedException();
        }
    }
}
