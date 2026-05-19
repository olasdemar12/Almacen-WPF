using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Pages.Documents
{
    public partial class DocumentsViewModel
    {
        [ObservableProperty]
        private ModuleReport? _selectedTypeReport;
        [ObservableProperty]
        private DateTime? _startDate;
        [ObservableProperty]
        private DateTime? _endDate;
        [ObservableProperty]
        private int? _idCategorySelected;
        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<Category> _categories;
        public IReadOnlyList<ModuleReport> ModuleReportsList { get; }
    }
}
