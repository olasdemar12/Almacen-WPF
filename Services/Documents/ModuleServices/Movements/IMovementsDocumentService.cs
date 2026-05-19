using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Documents;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Movements;
using MVVM.Models.Category;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Documents.ModuleServices.Movements
{

    public interface IMovementsDocumentService
    {
        public Task<List<RowMovementsProductsDocument>?> GetRowMovementsProductsDocumentsAsync();

        public Task<ServiceResult> ExportMovementsDocument(InformationReport FileName, List<RowMovementsProductsDocument> viewInformationModule);
    }
}
