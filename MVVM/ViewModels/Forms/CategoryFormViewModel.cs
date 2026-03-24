using Almacen_Sistema.MVVM.ViewModels.Login;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.MVVM.ViewModels.Forms
{
    public partial class CategoryFormViewModel:ObservableValidator
    {
        public CategoryFormViewModel(string title, Category category, ICategoryService service)
        {
            Title = title;
            this._categoryService = service;
            IsEnable = true;
            //Designamos el contenido del Boton y Activamos el Formulario.
            switch (title)
            {
                case "Agregar Nueva Categoría":
                    ContentedButton = "Agregar Categoría";
                    break;
                case "Editar Categoría":
                    ContentedButton = "Guardar Cambios";
                    CategoryObject = category;
                    Name = CategoryObject.NombreCategoria;
                    break;
            }
        }

        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        private Category categoryObject;

        [ObservableProperty]
        private string _title = "Formulario Categoria";

        [ObservableProperty]
        private string _contentedButton;

        [ObservableProperty]
        [Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(5, ErrorMessage = "Mínimo 5 caracteres")]
        [MaxLength(30, ErrorMessage = "Maximo 30 caracteres")]
        [CustomValidation(typeof(CategoryFormViewModel), nameof(ValidarErroresExternos))]
        private string _name;

        [ObservableProperty]
        private bool _isEnable;

        [ObservableProperty]
        private bool _isBusy;


        #region Commandos para el formulario

        [RelayCommand]
        private async Task CloseForm()
        {
            IsEnable = false;
            DialogHost.Close("DialogsRoot", "GoManagementCategory");
            await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
        }

        [RelayCommand]
        private async Task FormCategoryAction()
        {
            _errorManualUsuario = string.Empty;
            ValidateAllProperties();
            if (HasErrors)
            {
                SystemSounds.Hand.Play();
                return;
            }
            IsEnable = false;
            IsBusy = true;
            switch (Title)
            {
                case "Agregar Nueva Categoría":
                    await SaveCategory();
                    break;
                case "Editar Categoría":
                    await EditCategory();
                    break;
            }

        }

        private async Task SaveCategory()
        {
            var result = await _categoryService.AddCategory(Name);
            if(result.IsSuccess && result.Data != null)
            {
                var notificationTask = NotificationServiceControl.Instance.ShowNotification("Categoría Agregada Correctamente", NotificationType.Success);
                var closeformTask = CloseForm();

                await Task.WhenAll(notificationTask, closeformTask);
            }
            else
            {
                SystemSounds.Hand.Play();
                IsEnable = true;
                IsBusy = false;
                ClearErrors(nameof(Name));
                _errorManualUsuario = "El Nombre de la Categoría ya existe, por favor elija otro.";
                ValidateProperty(Name, nameof(Name));
            }
        }

        private async Task EditCategory()
        {
            CategoryObject.NombreCategoria = Name;
            var result = await _categoryService.EditCategory(CategoryObject);
            if(result.IsSuccess)
            {
                var notificationTask = NotificationServiceControl.Instance.ShowNotification("Categoría Editada Correctamente", NotificationType.Success);
                var closeformTask = CloseForm();
                await Task.WhenAll(notificationTask, closeformTask);
            }
            else
            {
                SystemSounds.Hand.Play();
                IsEnable = true;
                IsBusy = false;
                ClearErrors(nameof(Name));
                _errorManualUsuario = result.Message;
                ValidateProperty(Name, nameof(Name));
            }
        }

        #endregion

        #region validaciones Personalizadas

        private string _errorManualUsuario = string.Empty;

        public static ValidationResult ValidarErroresExternos(string valor, ValidationContext contexto)
        {
            var vm = (CategoryFormViewModel)contexto.ObjectInstance;

            if (!string.IsNullOrEmpty(vm._errorManualUsuario))
            {
                return new ValidationResult(vm._errorManualUsuario);
            }
            return ValidationResult.Success;
        }

        #endregion
    }
}
