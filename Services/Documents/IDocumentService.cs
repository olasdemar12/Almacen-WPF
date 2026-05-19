using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Documents
{
    public interface ICategorysDocumentFilter
    {
        public Task<List<CategoryModel>> GetAllCategory();
    }
    public interface IDocumentService
    {
        public Task<List<CategoryModel>> GetCategoriesAsync();
    }
}
