using Almacen_Sistema.MVVM.Models.Documents;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using StockMasterControls;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Movements
{
    public partial class MovementsReportPreviewViewModel
    {
        private bool FilterMovements(object obj)
        {
            if (obj is not RowMovementsProductsDocument rowMovements)
                return false;

            if (StartDate.HasValue)
            {
                DateTime start = StartDate.Value.Date;

                if (rowMovements.RegisterDate < start)
                    return false;
            }

            if (EndDate.HasValue)
            {
                DateTime endExclusive = EndDate.Value.Date.AddDays(1);

                if (rowMovements.RegisterDate >= endExclusive)
                    return false;
            }

            if (IdCategorySelected.HasValue)
            {
                if (rowMovements.IdCategory != IdCategorySelected.Value)
                    return false;
            }

            return true;
        }
        private void SetIdCategoryFilter(int? IdCategorySelected)
        {
            this.IdCategorySelected = IdCategorySelected;
            RowsMovementView.Refresh();
        }
        private void SetStartDateTime(DateTime? dateTime)
        {
            this.StartDate = dateTime;
            RowsMovementView.Refresh();
        }
        private void SetEndDateTime(DateTime? dateTime)
        {
            this.EndDate = dateTime;
            RowsMovementView.Refresh();
        }
        private void GenerateReport(ModuleReport typeReport)
        {
            if (typeReport != ModuleReport.Movimientos || RowsMovementView.IsEmpty)
            {
                MessageBox.Show("No hay registros para generar un reporte", "No se puede generar el reporte", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var information = new InformationReport(typeReport, StartDate, EndDate, string.Empty);
            var ListMovements = RowsMovementView.Cast<RowMovementsProductsDocument>().ToList();

            if (IdCategorySelected != null && ListMovements[0].IdCategory == IdCategorySelected.Value)
            {
                information.CategoryName = ListMovements[0].CategoryName;
            }
            else
            {
                information.CategoryName = "Sin categoria seleccionada";
            }
            var result = _movementsDocumentService.ExportMovementsDocument(information, ListMovements);
            var type = result.Result.IsSuccess ? NotificationType.Success : NotificationType.Error;
            NotificationServiceControl.Instance.ShowNotification(result.Result.Message, type);
        }

        public async Task LoadingTableInformation()
        {
            IsLoading = true;
            var result = await _movementsDocumentService.GetRowMovementsProductsDocumentsAsync();
            if (result != null)
            {
                RowsMovement = result;
                RowsMovementView = CollectionViewSource.GetDefaultView(result);
                RowsMovementView.Filter = FilterMovements;
                RowsMovementView.Refresh();
            }
            IsLoading = false;
        }
    }
}
