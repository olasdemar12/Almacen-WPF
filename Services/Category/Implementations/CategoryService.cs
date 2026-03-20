using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.Services.Data.CategoryDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Category.Implementations
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            this._categoryRepository = categoryRepository;
        }
        private readonly ICategoryRepository _categoryRepository;


        public async Task<List<CategoryModel>> GetAllCategorys()
        {
            return await _categoryRepository.GetAllCategory();
        }

        public async Task<ServiceResult<CategoryModel>> AddCategory(string CategoryName)
        {
            if (await _categoryRepository.CategoryExists(CategoryName))
            {
                return new ServiceResult<CategoryModel>(false, "La Categoría ya existe. Intente con otro Nombre");
            }

            CategoryModel category = await _categoryRepository.InsertCategory(CategoryName);

            return new ServiceResult<CategoryModel>(true, "Categoría agregada exitosamente", category);
        }

        public async Task<ServiceResult<object>> EditCategory(CategoryModel Category)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> RemoveCategory(int IdCategory)
        {
            throw new NotImplementedException();
        }
    }
}
