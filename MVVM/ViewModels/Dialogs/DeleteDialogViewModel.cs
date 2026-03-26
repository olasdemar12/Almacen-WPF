using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.Services.Category.Implementations;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Dialogs
{
    public partial class DeleteDialogViewModel:ObservableObject
    {
        public DeleteDialogViewModel(Category category, ICategoryService service)
        {
            this._categoryService = service;
            CategoryObject = category;
            CategoryObject.NombreCategoria = $"{category.NombreCategoria}.";
        }

        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        private Category _categoryObject;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isEnable = true;

        [RelayCommand]
        private async Task DeleteCategory()
        {
            IsLoading = true;
            IsEnable = false;
            var result = _categoryService.RemoveCategory(CategoryObject.IdCategoria).Result;
            if(result.IsSuccess)
            {
                var NotificationTask = NotificationServiceControl.Instance.ShowNotification(result.Message, NotificationType.Success);
                var CloseDialogTask = CloseDialog();
                await Task.WhenAll(NotificationTask, CloseDialogTask);
                StockMasterEvents.OnCategoryChanges(CategoryActionChanges.DeleteCategory, CategoryObject);
            }
            else
            {
                var NotificationTask = NotificationServiceControl.Instance.ShowNotification(result.Message, NotificationType.Error);
                var CloseDialogTask = CloseDialog();
                await Task.WhenAll(NotificationTask, CloseDialogTask);
            }
        }

        [RelayCommand]
        private async Task CloseDialog()
        {
            IsEnable = false;
            DialogHost.Close("DialogsRoot", "GoManagementCategory");
            await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
        }

    }
}
