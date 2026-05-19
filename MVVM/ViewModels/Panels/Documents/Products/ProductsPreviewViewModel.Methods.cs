using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using MVVM.Models.Product;
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
            if (typeReport != ModuleReport.Productos)
                return;
            MessageBox.Show("Se pidio generar un reporte del modulo: Productos");
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
