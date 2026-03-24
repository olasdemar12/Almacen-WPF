using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.UI.Dialogs.Category;
using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // Servicios
        private readonly ICategoryService _categoryService;

        //Propiedades y campos de clase
        [ObservableProperty]
        ObservableCollection<CategoryModel> _categories;

        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private bool _isEnable = false;


        //Definicion de comandos
        [RelayCommand]
        private async Task FormCategoryAction()
        {
            DialogHost.Close("DialogsRoot", "FormCategoryAction");
            await DialogHost.Show(new CategoryFormView("Agregar Nueva Categoría", new CategoryModel(), _categoryService), "DialogsRoot");
        }

        [RelayCommand]
        private async Task EditCategoryAction(CategoryModel category)
        {
            DialogHost.Close("DialogsRoot", "EditCategoryAction");
            await DialogHost.Show(new CategoryFormView("Editar Categoría", category, _categoryService), "DialogsRoot");
        }

        [RelayCommand]
        private async Task DeleteCategoryAction(CategoryModel category)
        {
            DialogHost.Close("DialogsRoot", "DeleteCategoryAction");
            await DialogHost.Show(new DeleteCategoryDialog(category, _categoryService), "DialogsRoot");
        }

        //Metodos de consulta:
        public async Task LoadingData()
        {
            var categories = await _categoryService.GetAllCategorys();
            Categories = new ObservableCollection<CategoryModel>(categories);
        }
    }
}
