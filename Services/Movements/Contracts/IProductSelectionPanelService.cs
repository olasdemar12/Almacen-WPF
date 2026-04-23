using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Movements.Contracts
{
    public interface IProductSelectionPanelService
    {
        //Para obtener todos los productos registrados
        public List<ProductModel> GetAllProductsAsync();

        //Para obtener las categorias registradas
        public Task<List<CategoryModel>> GetAllCategory();
    }
}
