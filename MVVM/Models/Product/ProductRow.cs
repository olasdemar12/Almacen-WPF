using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM.Models.Product
{
    public partial class ProductRow : ObservableObject
    {
        [ObservableProperty]
        private int idProduct;
        [ObservableProperty]
        private string productName;
        [ObservableProperty]
        private string category;
        [ObservableProperty]
        private int idcategory;
        [ObservableProperty]
        private string barCode;
        [ObservableProperty]
        private string saleType;
        [ObservableProperty]
        private decimal salePrice;

        // Constructor vacío
        public ProductRow()
        {
        }

        // Constructor con parámetros
        public ProductRow(int idProd, string nombre, string cat, int idCat, string codBarra, string tipoVenta, decimal precioVenta)
        {
            IdProduct = idProd;
            ProductName = nombre;
            Category = cat;
            Idcategory = idCat;
            BarCode = codBarra;
            SaleType = tipoVenta;
            SalePrice = precioVenta;
        }

        // Constructor que recibe Product
        public ProductRow(Product product)
        {
            IdProduct = product.IdProduct;
            ProductName = product.ProductName;
            Idcategory = product.IdCategory;
            BarCode = product.BarCode;
            SaleType = product.SaleType;
            SalePrice = product.SalePrice;
        }
    }
}
