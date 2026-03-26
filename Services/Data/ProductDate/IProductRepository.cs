using MVVM.Models.Category;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Data.ProductDate
{
    public enum ActionRegister
    {
        AddProduct,
        UpddateProduct
    }
    public interface IProductRepository
    {
        List<ProductModel> GetAllProductsAsync();
        Task<bool> InsertProductAsync(ProductModel product);
        Task<bool> UpdateProductAsync(ProductModel product);
        Task<bool> DeleteProductAsync(int IdProduct);
        Task<ProductRepositoryResult> ValidateProductAsync(ActionRegister action, ProductModel product);
    }

    public class ProductRepositoryResult
    {
        public bool NameExists { get; set; }
        public bool BarcodeExists { get; set; }
        public bool IsSuccess => NameExists && BarcodeExists; 
    }
}
