using Almacen_Sistema.MVVM.Models.Movements;
using CommunityToolkit.Mvvm.ComponentModel;
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
        }

        [ObservableProperty]
        private Product _productObject;
        [ObservableProperty]
        private Tickets _ticketsObject;

    }
}
