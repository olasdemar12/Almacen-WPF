using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Data.CategoryDate
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryModel>> GetAllCategory();
        public Task<CategoryModel> InsertCategory(string CategoryName);
        public Task<bool> UpdateCategory(CategoryModel Category);
        public Task<bool> DeleteCategory(int IdCategory);
        public Task<bool> CategoryExists(string CategoryName);
    }
}
