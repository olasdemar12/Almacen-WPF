using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Documents.ModuleServices.Products
{
    public class ProductDocumentService : IProductDocumentService
    {
        public ProductDocumentService()
        {
            productService = new ProductRepository();
            productRows = new List<DocumentProductRow>();
        }
        private readonly IProductRowsServiceList productService;
        private List<DocumentProductRow> productRows;
        public async Task<List<DocumentProductRow>> GetDocumentProductRowAsync()
        {
            var result = productService.GetAllProductsAsync();
            foreach (var product in result)
            {
                productRows.Add(new DocumentProductRow(
                    product.IdCategory.GetValueOrDefault(),
                    product.ProductName,
                    product.CategoryName,
                    product.Active == 1 ? StateProduct.Activo : StateProduct.Inactivo,
                    product.SalePrice,
                    product.SaleType
                    ));
            }
            return productRows;
        }
        public Task<ServiceResult> ExportMovementsDocument()
        {
            throw new NotImplementedException();
        }
    }
}
