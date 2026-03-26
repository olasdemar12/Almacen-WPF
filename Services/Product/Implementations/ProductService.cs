using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Forms.Category;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Product.Implementations
{
    public class ProductService : IProductService
    {
        public ProductService(IProductRepository productRepository, IProductReadCategoryService productReadCategoryService)
        {
            this.productRepository = productRepository;
            this.readCategorys = productReadCategoryService;
        }
        private readonly IProductRepository productRepository;
        private readonly IProductReadCategoryService readCategorys;


        public async Task<ServiceResult<ProductRepositoryResult>> AddProductAsync(ProductModel product)
        {
            var resulExist = await productRepository.ValidateProductAsync(ActionRegister.AddProduct,product);
            if (resulExist.IsSuccess)
                return new ServiceResult<ProductRepositoryResult>(await productRepository.InsertProductAsync(product),
    "Producto agregado correctamente", resulExist);
            else
                return new ServiceResult<ProductRepositoryResult>(false, "Error o Datos invalidos", resulExist);
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            return await productRepository.GetAllProductsAsync();
        }

        public Task<ServiceResult<ProductRepositoryResult>> EditProductAsync(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveProductAsync(int IdProduc)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryModel>> GetCategoriesRegisterAsync()
        {
            return await readCategorys.GetAllCategorysAsync();
        }
    }
}
