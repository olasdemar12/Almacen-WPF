using Almacen_Sistema.Composition;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Category.Contracts
{
    public interface ICategoryService
    {
        public Task<List<CategoryModel>> GetAllCategorys();
        public Task<ServiceResult<CategoryModel>> AddCategory(string CategoryName);
        public Task<ServiceResult<object>>EditCategory(CategoryModel Category);
        public Task<ServiceResult<bool>> RemoveCategory(int IdCategory);
    }
}
