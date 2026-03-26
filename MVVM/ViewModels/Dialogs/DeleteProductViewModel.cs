using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
using MVVM.Models.Product;
using StockMasterControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Dialogs
{
    public partial class DeleteProductViewModel: ObservableObject
    {
        public DeleteProductViewModel(Product product, IProductService service)
        {
            this._productService = service;
            ProductObject = product;
            ProductObject.ProductName = $"{ProductObject.ProductName}.";
        }

        private readonly IProductService _productService;

        [ObservableProperty]
        private Product _productObject;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isEnable = true;

        private ServiceResult<bool> _result;

        [RelayCommand]
        private async Task DeleteProduct()
        {
            IsLoading = true;
            IsEnable = false;
            _result = await _productService.RemoveProductAsync(ProductObject.IdProduct);
            if (_result.IsSuccess)
            {
                var NotificationTask = NotificationServiceControl.Instance.ShowNotification("Producto eliminado correctamente", NotificationType.Success);
                var CloseDialogTask = CloseDialog();
                await Task.WhenAll(NotificationTask, CloseDialogTask);
            }
            else
            {
                var NotificationTask = NotificationServiceControl.Instance.ShowNotification($"Error al intentar eliminar el producto {ProductObject.ProductName}", NotificationType.Error);
                var CloseDialogTask = CloseDialog();
                await Task.WhenAll(NotificationTask, CloseDialogTask);
            }
        }

        [RelayCommand]        
        private Task CloseDialog()
        {
            IsEnable = false;
            DialogHost.Close("DialogsRoot", _result);
            return Task.CompletedTask;
        }
    }
}
