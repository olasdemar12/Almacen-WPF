using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Product.Contracts
{
    public interface IProductReadCategoryService
    {
        public Task<List<CategoryModel>> GetAllCategorysAsync();
    }
}
