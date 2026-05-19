using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Inventory
{
    public partial class InventoryPreviewViewModel
    {
        private bool FilterInventoryRows(object obj)
        {
            if (obj is not DocumentInventoryRow rowInventory)
                return false;

            if (StartDate.HasValue)
            {
                DateTime start = StartDate.Value.Date;

                if (rowInventory.LastMovement < start)
                    return false;
            }

            if (EndDate.HasValue)
            {
                DateTime endExclusive = EndDate.Value.Date.AddDays(1);

                if (rowInventory.LastMovement >= endExclusive)
                    return false;
            }

            if (IdCategorySelected.HasValue)
            {
                if (rowInventory.IdCategory != IdCategorySelected.Value)
                    return false;
            }

            return true;
        }
        private void SetStartDateTime(DateTime? dateTime)
        {
            this.StartDate = dateTime;
            RowsInventoryView.Refresh();
        }
        private void SetEndDateTime(DateTime? dateTime)
        {
            this.EndDate = dateTime;
            RowsInventoryView.Refresh();
        }
        private void SetIdCategoryFilter(int? IdCategorySelected)
        {
            this.IdCategorySelected = IdCategorySelected;
            RowsInventoryView.Refresh();
        }
        private void GenerateReport(ModuleReport typeReport)
        {
            if (typeReport != ModuleReport.Inventario)
                return;
            MessageBox.Show("Se pidio generar un reporte del modulo: Inventario");
        }
        public async Task LoadingInformationDocument()
        {
            IsLoading = true;
            var result = await _invetoryDocumentService.GetDocumentInventoryRowsAsync();
            RowsInventory = result;
            RowsInventoryView = CollectionViewSource.GetDefaultView(result);
            RowsInventoryView.Filter = FilterInventoryRows;
            RowsInventoryView.Refresh();
            IsLoading = false;
        }
    }
}
