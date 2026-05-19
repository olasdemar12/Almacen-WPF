using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Pages.Documents
{
    public partial class DocumentsViewModel
    {
        partial void OnSelectedTypeReportChanged(ModuleReport? value)
        {
            IdCategorySelected = null;
            StartDate = null;
            EndDate = null;
        }
        public async Task LoadingInformationModule()
        {
            IsLoading = true;
            var result = await _documentService.GetCategoriesAsync();
            Categories = result;
            IsLoading = false;
        }
    }
}
