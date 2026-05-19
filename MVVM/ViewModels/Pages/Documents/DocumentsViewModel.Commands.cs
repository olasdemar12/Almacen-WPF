using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Pages.Documents
{
    public partial class DocumentsViewModel
    {
        [RelayCommand]
        private void GenerateDocument()
        {
            _filterEventService.OnGenerateReportInvoke(SelectedTypeReport.GetValueOrDefault());
        }
    }
}
