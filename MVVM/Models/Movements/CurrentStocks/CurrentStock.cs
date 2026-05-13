using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Movements.CurrentStocks
{
    public partial class CurrentStock : ObservableObject
    {
        public CurrentStock()
        {
            IdStock = 0;
            IdProduct = 0;
            TotalAmount = 0;
            MiniumStock = 0;
        }

        public CurrentStock(int idStock, int idProduct, decimal totalAmount, decimal miniumStock)
        {
            IdStock = idStock;
            IdProduct = idProduct;
            TotalAmount = totalAmount;
            MiniumStock = miniumStock;
        }

        public CurrentStock(int idProduct, decimal totalAmount)
        {
            IdProduct = idProduct;
            TotalAmount = totalAmount;
        }

        [ObservableProperty]
        private int _idStock;
        [ObservableProperty]         
        private int _idProduct;
        [ObservableProperty]
        private decimal _totalAmount;
        [ObservableProperty]
        private decimal _miniumStock;
    }
}
