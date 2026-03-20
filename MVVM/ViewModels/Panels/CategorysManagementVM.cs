using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.MVVM.ViewModels.Panels
{
    public partial class CategorysManagementVM:ObservableObject
    {
        public CategorysManagementVM(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        private readonly ICategoryService _categoryService;

        [RelayCommand]
        private async Task FormCategoryAction()
        {
            DialogHost.Close("DialogsRoot", "FormCategoryAction");
            await Task.Delay(200);
            await DialogHost.Show(new CategoryFormView("Agregar Nueva Categoría", new CategoryModel(), _categoryService), "DialogsRoot");
        }
    }
}
