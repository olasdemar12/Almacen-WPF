using Almacen_Sistema.MVVM.ViewModels.Pages.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Composition.EventsDefinitions.Documents
{
    public interface IDocumentEvents
    {
        public delegate void SetDateTime(DateTime? DateTime);
        public event SetDateTime OnStartDateTime;
        public event SetDateTime OnEndDateTime;
        public void OnStratDateTimeInvoke(DateTime? dateTime);
        public void OnEndDateTimeInvoke(DateTime? dateTime);


        public delegate void SetIdCategorySelected(int? IdCategorySelected);
        public event SetIdCategorySelected? OnIdCategorySelected;
        public void OnIdCategorySelectedInvoke(int? IdCategorySelected);

        public delegate void GenerateReport(ModuleReport typeReport);
        public event GenerateReport OnGenerateReport;
        public void OnGenerateReportInvoke(ModuleReport typeReport);
    }

    public class DocumentEvents : IDocumentEvents
    {
        public event IDocumentEvents.SetIdCategorySelected? OnIdCategorySelected;
        public event IDocumentEvents.SetDateTime? OnStartDateTime;
        public event IDocumentEvents.SetDateTime? OnEndDateTime;
        public event IDocumentEvents.GenerateReport? OnGenerateReport;

        public void OnStratDateTimeInvoke(DateTime? dateTime)
        {
            OnStartDateTime?.Invoke(dateTime);
        }
        public void OnEndDateTimeInvoke(DateTime? dateTime)
        {
            OnEndDateTime?.Invoke(dateTime);
        }
        public void OnIdCategorySelectedInvoke(int? IdCategorySelected)
        {
            OnIdCategorySelected?.Invoke(IdCategorySelected);
        }

        public void OnGenerateReportInvoke(ModuleReport typeReport)
        {
            OnGenerateReport?.Invoke(typeReport);
        }
    }
}
