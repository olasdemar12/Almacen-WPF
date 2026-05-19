using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.Services.Data.Documents;
using Almacen_Sistema.Services.Data.Documents.Movements;
using Almacen_Sistema.Services.Documents.PDFReportService.GetPathService;
using Almacen_Sistema.Services.Movements.Implementations;
using Almacen_Sistema.Services.Product.Implementations;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
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
            _pathSaveService = new PathArchiveSaveService();
        }

        private readonly IReadRowMovementsDocument _readRowMovements;
        private readonly IPathSaveService _pathSaveService;

        public async Task<List<RowMovementsProductsDocument>?> GetRowMovementsProductsDocumentsAsync()
        {
            return await _readRowMovements.SelectAllInformationDocument();
        }

        public async Task<ServiceResult> ExportMovementsDocument(string FileName)
        {
            var path = _pathSaveService.GetPdfSavePath(FileName);
            if(path != null)
                return ServiceResult.Success(path);
            return ServiceResult.Failure("No se pudo obtener la ruta");
        }

    }
}
