using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Movements
{
    public partial class MovementsReportPreviewViewModel
    {
        public async Task LoadingTableInformation()
        {
            IsLoading = true;
            var result = await _movementsDocumentService.GetRowMovementsProductsDocumentsAsync();
            if (result != null)
            {
                RowsMovement = result;
                RowsMovementView = CollectionViewSource.GetDefaultView(result);
            }
            IsLoading = false;
        }
    }
}
