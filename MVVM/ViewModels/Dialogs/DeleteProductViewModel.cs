using Almacen_Sistema.Composition;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.UI.Forms.Product;
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
    public enum ActionDeleteProduct
    {
        DeleteView,
        DeleteSearch
    }
    public partial class DeleteProductViewModel: ObservableObject
    {
        public DeleteProductViewModel(Product product, IProductService service,ActionDeleteProduct actionDelete)
        {
            this._productService = service;
            ProductObject = product;
            this.action = actionDelete;
        }

        private readonly IProductService _productService;

        [ObservableProperty]
        private Product _productObject;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isEnable = true;

        private ServiceResult<bool> _result;
        private ActionDeleteProduct action;

        [RelayCommand]
        private async Task DeleteProduct()
        {
            IsLoading = true;
            IsEnable = false;
            _result = await _productService.RemoveProductAsync(ProductObject.IdProduct);
            if (_result.IsSuccess)
            {
                if (action == ActionDeleteProduct.DeleteSearch)
                    await StockMasterEvents.OnDeleteProductSearch(ProductObject);
                DialogHost.Close("DialogsRoot", _result);
                await NotificationServiceControl.Instance.ShowNotification("Producto eliminado correctamente", NotificationType.Success);
            }
            else
            {
                DialogHost.Close("DialogsRoot", _result);
                await NotificationServiceControl.Instance.ShowNotification($"Error al intentar eliminar el producto {ProductObject.ProductName}", NotificationType.Error);
            }
        }

        [RelayCommand]        
        private async Task CloseDialog()
        {
            switch(action)
            {
                case ActionDeleteProduct.DeleteView:
                    IsEnable = false;
                    DialogHost.Close("DialogsRoot", _result);
                    break;
                case ActionDeleteProduct.DeleteSearch:
                    DialogHost.Close("DialogsRoot", _result);
                    await DialogHost.Show(new ProductFormView("Producto Encontrado", ProductObject, _productService), "DialogsRoot");
                    break;
            }
        }
    }
}
