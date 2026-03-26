using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Data.ProductDate;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Product.Contracts
{
    public interface IProductService
    {
        public Task<ServiceResult<ProductRepositoryResult>> AddProductAsync(ProductModel product);
        public Task<List<ProductModel>> GetAllProductsAsync();
        public Task<ServiceResult<ProductRepositoryResult>> EditProductAsync(ProductModel product);
        public Task<bool> RemoveProductAsync(int IdProduc);
        public Task<List<CategoryModel>> GetCategoriesRegisterAsync();
    }
}
