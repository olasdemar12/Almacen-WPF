using Almacen_Sistema.MVVM.Models.Documents;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using MVVM.Models.Product;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Almacen_Sistema.MVVM.ViewModels.Panels.Documents.Products
{
    public partial class ProductsPreviewViewModel
    {
        private bool FilterProductRows(object obj)
        {
            if (obj is not DocumentProductRow rowProduct)
                return false;

            if (IdCategorySelected.HasValue)
            {
                if (rowProduct.IdCategory != IdCategorySelected.Value)
                    return false;
            }

            return true;
        }
        private void SetIdCategoryFilter(int? IdCategorySelected)
        {
            this.IdCategorySelected = IdCategorySelected;
            RowsProductView.Refresh();
        }
        private void GenerateReport(ModuleReport typeReport)
        {
            if (typeReport != ModuleReport.Productos || RowsProductView.IsEmpty)
            {
                MessageBox.Show("No hay registros para generar un reporte", "No se puede generar el reporte", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var information = new InformationReport(typeReport, DateTime.Now, DateTime.Now, string.Empty);
            var ListProduct = RowsProductView.Cast<DocumentProductRow>().ToList();

            if (IdCategorySelected != null && ListProduct[0].IdCategory == IdCategorySelected.Value)
            {
                information.CategoryName = ListProduct[0].CategoryName;
            }
            else
            {
                information.CategoryName = "Sin categoria seleccionada";
            }
            var result = _productDocumentService.ExportMovementsDocument(information, ListProduct);
            var type = result.Result.IsSuccess ? NotificationType.Success : NotificationType.Error;
            NotificationServiceControl.Instance.ShowNotification(result.Result.Message, type);
        }

        public async Task LoadingInformationDocument()
        {
            IsLoading = true;
            RowsProduct = await _productDocumentService.GetDocumentProductRowAsync();
            RowsProductView = CollectionViewSource.GetDefaultView(RowsProduct);
            RowsProductView.Filter = FilterProductRows;
            RowsProductView.Refresh();
            IsLoading = false;
        }
    }
}
