using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Documents.PDFReportService.GetPathService
{
    public interface IPathSaveService
    {
        public string? GetPdfSavePath(string suggestedFileName);
    }
}
