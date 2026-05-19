using Almacen_Sistema.Services.Data.CategoryDate;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Documents
{
    public class DocumentService : IDocumentService
    {
        public DocumentService()
        {
            categorys = new CategoryRepository();
        }

        private readonly ICategorysDocumentFilter categorys;

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var result = await categorys.GetAllCategory();
            result.Add(new CategoryModel(0, "Sin Asignar", 0));
            return result;
        }
    }
}
