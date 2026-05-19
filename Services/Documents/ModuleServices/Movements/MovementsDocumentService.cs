using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.Services.Data.Documents;
using Almacen_Sistema.Services.Data.Documents.Movements;
using Almacen_Sistema.Services.Movements.Implementations;
using Almacen_Sistema.Services.Product.Implementations;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductModel = MVVM.Models.Product.Product;

namespace Almacen_Sistema.Services.Documents.ModuleServices.Movements
{
    public class MovementsDocumentService : IMovementsDocumentService
    {
        public MovementsDocumentService()
        {
            _readRowMovements = new DocumentsInformationRepository();
        }

        private readonly IReadRowMovementsDocument _readRowMovements;

        public async Task<List<RowMovementsProductsDocument>?> GetRowMovementsProductsDocumentsAsync()
        {
            return await _readRowMovements.SelectAllInformationDocument();
        }

        public Task<ServiceResult> ExportMovementsDocument()
        {
            throw new NotImplementedException();
        }

    }
}
