using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.UI.Panels.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Forms.Movements
{
    public partial class EntryFormViewModel: ObservableValidator
    {
        public EntryFormViewModel(Product product)
        {
            ProductObject = product;
            TicketsObject = new Tickets();
            IsEnable = true;
        }

        [ObservableProperty]
        private Product _productObject;
        [ObservableProperty]
        private Tickets _ticketsObject;
        [ObservableProperty]
        private bool _isEnable;

        [RelayCommand]
        private void CloseForm()
        {
            IsEnable = false;
            DialogHost.Close("DialogsRoot", true);
            DialogHost.Show(new ProductSelectionControl(), "DialogsRoot");
        }
    }
}
