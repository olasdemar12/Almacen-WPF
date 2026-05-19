using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Documents.PDFReportService.GetPathService
{
    public class PathArchiveSaveService : IPathSaveService
    {
        public string? GetPdfSavePath(string suggestedFileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Guardar reporte PDF";
            dialog.FileName = suggestedFileName;
            dialog.DefaultExt = ".pdf";
            dialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            dialog.AddExtension = true;
            dialog.OverwritePrompt = true;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
