using Almacen_Sistema.Composition;
using Almacen_Sistema.MVVM.Models.Documents;
using Almacen_Sistema.MVVM.Models.Movements.Inventory;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Documents.PDFReportService.GetPathService;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using MVVM.Models.Product;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.Services.Documents.ModuleServices.Products
{
    public class ProductDocumentService : IProductDocumentService
    {
        public ProductDocumentService()
        {
            productService = new ProductRepository();
            productRows = new List<DocumentProductRow>();
            _pathSaveService = new PathArchiveSaveService();
        }
        private readonly IProductRowsServiceList productService;
        private readonly IPathSaveService _pathSaveService;
        private List<DocumentProductRow> productRows;
        public async Task<List<DocumentProductRow>> GetDocumentProductRowAsync()
        {
            var result = productService.GetAllProductsAsync();
            foreach (var product in result)
            {
                productRows.Add(new DocumentProductRow(
                    product.IdCategory.GetValueOrDefault(),
                    product.ProductName,
                    product.CategoryName,
                    product.Active == 1 ? StateProduct.Activo : StateProduct.Inactivo,
                    product.SalePrice,
                    product.SaleType
                    ));
            }
            return productRows;
        }

        public async Task<ServiceResult> ExportMovementsDocument(InformationReport informationReport, List<DocumentProductRow> viewInformationModule)
        {
            var path = _pathSaveService.GetPdfSavePath(informationReport.FileName);
            if (path != null)
            {
                GenerateMovementReport(path, viewInformationModule, informationReport);
                return ServiceResult.Success($"Reporte del Modulo {informationReport.TypeReport} Generado correctamente");
            }
            return ServiceResult.Failure("No se pudo obtener la ruta");
        }

        public void GenerateMovementReport(string filePath, List<DocumentProductRow> viewInformationModule, InformationReport informationReport)
        {
            if (!filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                filePath += ".pdf";

            try
            {
                using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(35);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(text => text.FontSize(9));

                        page.Header()
                            .Element(container => ComposeHeader(container, informationReport));

                        page.Content()
                            .PaddingVertical(10)
                            .Element(container => ComposeTable(container, viewInformationModule));

                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Página ");
                                text.CurrentPageNumber();
                                text.Span(" de ");
                                text.TotalPages();
                            });
                    });
                }).GeneratePdf(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar PDF: {ex.Message}");
            }
        }

        private void ComposeHeader(QuestPDF.Infrastructure.IContainer container, InformationReport informationReport)
        {
            container.Column(column =>
            {
                column.Item()
                    .Text("Stock Master")
                    .FontSize(18)
                    .Bold();

                column.Item()
                    .Text("Reporte de movimientos")
                    .FontSize(14)
                    .SemiBold();

                column.Item()
                    .PaddingTop(8)
                    .Text($"Fecha de generación: {informationReport.ArchiveGenerateDate.ToString("dd/MM/yyyy")}");

                column.Item()
                    .PaddingTop(6)
                    .Text("Filtros aplicados:")
                    .SemiBold();

                column.Item()
                    .Text($"Tipo: {informationReport.TypeReport}");

                column.Item()
                    .Text($"Categoria: {informationReport.CategoryName}");

                column.Item()
                    .PaddingTop(10)
                    .LineHorizontal(1)
                    .LineColor(Colors.Grey.Lighten1);
            });
        }

        private void ComposeTable(QuestPDF.Infrastructure.IContainer container, List<DocumentProductRow> movements)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);       // Producto
                    columns.RelativeColumn(1.5f);    // Categoría
                    columns.RelativeColumn(1.5f);    // Estado
                    columns.ConstantColumn(75);      // Tipo de venta
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCellStyle).Text("Producto");
                    header.Cell().Element(HeaderCellStyle).Text("Categoría");
                    header.Cell().Element(HeaderCellStyle).Text("Estado");
                    header.Cell().Element(HeaderCellStyle).Text("Tipo de venta");
                });

                foreach (var movement in movements)
                {
                    table.Cell().Element(BodyCellStyle).Text(movement.ProductName);
                    table.Cell().Element(BodyCellStyle).Text(movement.CategoryName);
                    table.Cell().Element(BodyCellStyle).AlignCenter().Text(movement.StateProduct.ToString());
                    table.Cell().Element(BodyCellStyle).Text(movement.TypeSale);
                }
            });
        }

        private static QuestPDF.Infrastructure.IContainer HeaderCellStyle(QuestPDF.Infrastructure.IContainer container)
        {
            return container
                .Background("#4488C5")
                .PaddingVertical(6)
                .PaddingHorizontal(4)
                .DefaultTextStyle(text => text.FontColor(Colors.White).SemiBold())
                .AlignCenter();
        }

        private static QuestPDF.Infrastructure.IContainer BodyCellStyle(QuestPDF.Infrastructure.IContainer container)
        {
            return container
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(6)
                .PaddingHorizontal(4).AlignCenter();
        }
    }
}
