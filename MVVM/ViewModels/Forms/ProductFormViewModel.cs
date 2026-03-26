using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Panels.Products;
using Almacen_Sistema.UI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
using MVVM.Models.Product;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Almacen_Sistema.MVVM.ViewModels.Forms
{
    public partial class ProductFormViewModel: ObservableValidator
    {
        public ProductFormViewModel(string title, Product product, IProductService productService)
        {
            this._productService = productService;
            Title = title;
            IsEnable = true;
            switch(title)
            {
                case "Agregar Nuevo Producto":
                    ButtonContent = "Agregar Producto";
                    break;
                case "Editar Producto":
                    ButtonContent = "Guardar Cambios";
                    ProductObject = product;
                    EditingProduct();
                    break;
            }
        }

        private readonly IProductService _productService;

        //Metodo inicializador para actualizar el Producto
        private void EditingProduct()
        {
            ProductName = ProductObject.ProductName;
            BarCode = ProductObject.BarCode;
            IdCategory = ProductObject.IdCategory;
            SaleType = ProductObject.SaleType;
            PurchasePrice = ProductObject.PurchasePrice;
            SalePrice = ProductObject.SalePrice;
        }

        #region Propiedades del Formulario para su estructura y funcionamiento:
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private Product _productObject;

        [ObservableProperty]
        private string _buttonContent;

        [ObservableProperty]
        private bool _isEnable;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<Category> _categories;
        #endregion

        #region Propiedades de datos del Formulario:
        [ObservableProperty]
        [Required(ErrorMessage = "El Nombre del Producto es obligatorio")]
        [MinLength(10, ErrorMessage = "Mínimo 10 caracteres")]
        [MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [CustomValidation(typeof(ProductFormViewModel), nameof(ValidarErroresExternos))]
        private string _productName;

        [ObservableProperty]
        [Required(ErrorMessage = "El Codigo de Barras del Producto es obligatorio")]
        [MinLength(10, ErrorMessage = "Mínimo 10 caracteres")]
        [MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [CustomValidation(typeof(ProductFormViewModel), nameof(ValidarErroresExternos))]
        private string _barCode;

        [ObservableProperty]
        [Required(ErrorMessage = "Campo obligatorio")]
        private int? _idCategory;

        [ObservableProperty]
        [Required(ErrorMessage = "Campo obligatorio")]
        private string _saleType;

        [ObservableProperty]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor que 0")]
        private decimal _purchasePrice = 0.00m;

        [ObservableProperty]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor que 0")]
        private decimal _salePrice = 0.00m;
        #endregion

        [RelayCommand]
        private Task CloseForm()
        {
            IsEnable = false;
            DialogHost.Close("DialogsRoot", ProductObject);
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task ActionForm()
        {
            _errorManualUsuario = string.Empty;
            ValidateAllProperties();
            if(HasErrors)
            {
                SystemSounds.Hand.Play();
                return;
            }
            IsEnable = false;
            IsLoading = true;
            switch(Title)
            {
                case "Agregar Nuevo Producto":
                    ProductObject = new Product(ProductName,BarCode,SaleType,PurchasePrice,SalePrice,IdCategory.GetValueOrDefault());
                    await SaveProduct(ProductObject);
                    break;
                case "Editar Producto":
                    await EditProduct();
                    break;
            }
            IsEnable = true;
            IsLoading = false;
        }

        private async Task SaveProduct(Product product)
        {
            var result = await _productService.AddProductAsync(product);
            if(result.IsSuccess)
            {
                var notificationTask = NotificationServiceControl.Instance.ShowNotification(
                    result.Message, NotificationType.Success);
                var closeFormTask = CloseForm();

                await Task.WhenAll(notificationTask, closeFormTask);
            }

            if(!result.Data.NameExists)
            {
                SystemSounds.Hand.Play();
                ClearErrors(nameof(ProductName));
                _errorManualUsuario = "Este Nombre de producto ya existe. Intente con otro";
                ValidateProperty(ProductName, nameof(ProductName));
            }

            if(!result.Data.BarcodeExists)
            {
                SystemSounds.Hand.Play();
                ClearErrors(nameof(BarCode));
                _errorManualUsuario = "Este Codigo de Barras ya existe. Intente con otro";
                ValidateProperty(BarCode, nameof(BarCode));
            }
        }

        private async Task EditProduct()
        {

        }

        public async Task InitializeAsync()
        {
          Categories = await _productService.GetCategoriesRegisterAsync();
        }


        private string _errorManualUsuario = string.Empty;

        public static ValidationResult ValidarErroresExternos(string valor, ValidationContext contexto)
        {
            var vm = (ProductFormViewModel)contexto.ObjectInstance;

            if (!string.IsNullOrEmpty(vm._errorManualUsuario))
            {
                return new ValidationResult(vm._errorManualUsuario);
            }
            return ValidationResult.Success;
        }
    }
}
