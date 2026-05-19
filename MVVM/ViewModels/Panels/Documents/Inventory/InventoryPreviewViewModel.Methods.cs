using Almacen_Sistema.MVVM.Models.Documents;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using StockMasterControls;
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
            if (typeReport != ModuleReport.Inventario || RowsInventoryView.IsEmpty)
            {
                MessageBox.Show("No hay registros para generar un reporte", "No se puede generar el reporte", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var information = new InformationReport(typeReport, StartDate, EndDate, string.Empty);
            var ListInventory = RowsInventoryView.Cast<DocumentInventoryRow>().ToList();

            if (IdCategorySelected != null && ListInventory[0].IdCategory == IdCategorySelected.Value)
            {
                information.CategoryName = ListInventory[0].CategoryName;
            }
            else
            {
                information.CategoryName = "Sin categoria seleccionada";
            }
            var result = _invetoryDocumentService.ExportMovementsDocument(information, ListInventory);
            var type = result.Result.IsSuccess ? NotificationType.Success : NotificationType.Error;
            NotificationServiceControl.Instance.ShowNotification(result.Result.Message, type);
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
