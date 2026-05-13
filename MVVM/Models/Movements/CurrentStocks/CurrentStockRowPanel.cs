using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Almacen_Sistema.MVVM.Models.Movements.CurrentStocks
{
    public struct CurrentStockRowPanel
    {
        private int _idStock;
        private int _idProduct;
        private string _productName;
        private string _categoryName;
        private decimal _totalAmount;

        public int IdStock { get => _idStock; set => _idStock = value; }
        public int IdProduct { get => _idProduct; set => _idProduct = value; }
        public string ProductName { get => _productName; set => _productName = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }
        public decimal TotalAmount { get => _totalAmount; set => _totalAmount = value; }
        public CurrentStockRowPanel()
        {
            IdStock = 0;
            IdProduct = 0;
            ProductName = string.Empty;
            CategoryName = string.Empty;
            TotalAmount = 0;
        }
        public CurrentStockRowPanel(int idStock, int idProduct, string productName, string categoryName, decimal totalAmount)
        {
            IdStock = idStock;
            IdProduct = idProduct;
            ProductName = productName;
            CategoryName = categoryName;
            TotalAmount = totalAmount;
        }
    }
}
