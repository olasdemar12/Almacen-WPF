using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        partial void OnStartDateChanged(DateTime? value)
        {
            _filterEventService.OnStratDateTimeInvoke(value);
        }
        partial void OnEndDateChanged(DateTime? value)
        {
            _filterEventService?.OnEndDateTimeInvoke(value);
        }
        partial void OnIdCategorySelectedChanged(int? value)
        {
            _filterEventService.OnIdCategorySelectedInvoke(value);
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
