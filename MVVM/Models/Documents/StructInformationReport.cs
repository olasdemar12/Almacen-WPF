using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Documents
{
    public struct InformationReport
    {
        public ModuleReport TypeReport;
        public string FileName;
        public DateTime ArchiveGenerateDate = DateTime.Now;
        public DateTime? StartDate;
        public DateTime? EndDate;
        public string CategoryName;

        public InformationReport(ModuleReport TypeReport, DateTime? StartDate, DateTime? EndDate, string CategoryName)
        {
            this.TypeReport = TypeReport;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.CategoryName = CategoryName;
            FileName = GetNameFile();
        }

        public string GetNameFile()
        {
            return $"{TypeReport.ToString()} - {ArchiveGenerateDate.ToString("dd-MM-yyyy HH-mm-ss")}";
        }
    }
}
