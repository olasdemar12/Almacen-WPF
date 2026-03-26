using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Forms.Category;
using Microsoft.Data.Sqlite;
using MVVM.Models.Product;
using StockMasterControls;
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
            var resulExist = await productRepository.ValidateProductAsync(ActionRegister.AddProduct, product);
            if (resulExist.IsSuccess)
                return new ServiceResult<ProductRepositoryResult>(await productRepository.InsertProductAsync(product),
    "Producto agregado correctamente", resulExist);
            else
                return new ServiceResult<ProductRepositoryResult>(false, "Error o Datos invalidos", resulExist);
        }

        public  List<ProductModel> GetAllProductsAsync()
        {
            return productRepository.GetAllProductsAsync();
        }

        public async Task<ServiceResult<ProductRepositoryResult>> EditProductAsync(ProductModel product)
        {
            var resultExist = await productRepository.ValidateProductAsync(ActionRegister.UpddateProduct,product);
            if (resultExist.IsSuccess)
                return new ServiceResult<ProductRepositoryResult>(await productRepository.UpdateProductAsync(product),
                    "Producto actulizado correctamente",resultExist);
            else 
                return new ServiceResult<ProductRepositoryResult>(false, "Error o Datos invalidos", resultExist);
        }

        public async Task<ServiceResult<bool>> RemoveProductAsync(int IdProduc)
        {
            
            if(await productRepository.DeleteProductAsync(IdProduc))
            {
                return new ServiceResult<bool>(true,"Producto eliminado correctamente",true);
            }
            else
                return new ServiceResult<bool>(false, "Error al intentar eliminar el Producto. Intente nuevamente", false);
        }

        public async Task<List<CategoryModel>> GetCategoriesRegisterAsync()
        {
            return await readCategorys.GetAllCategory();
        }      
    }
}
