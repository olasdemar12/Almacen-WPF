using Almacen_Sistema.Services.Category.Implementations;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Composition
{
    public static class StockMasterEvents
    {
        public static event CategoryChanges CategoryChanges;
        public static void OnCategoryChanges(CategoryActionChanges action,Category category)
        {
            CategoryChanges?.Invoke(action,category);
        }
    }
}
