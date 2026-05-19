using Almacen_Sistema.Composition;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Documents.ModuleServices.Products
{
    public interface IProductDocumentService
    {
        public Task<List<DocumentProductRow>> GetDocumentProductRowAsync();

        public Task<ServiceResult> ExportMovementsDocument();
    }

    public interface IProductRowsServiceList
    {
        List<ProductModel> GetAllProductsAsync();
    }
}
