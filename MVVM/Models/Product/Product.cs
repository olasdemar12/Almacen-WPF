using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models.Product
{
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private int idProduct;
        [ObservableProperty]
        private string productName;
        [ObservableProperty]
        private string barCode;
        [ObservableProperty]
        private string saleType;
        [ObservableProperty]
        private decimal purchasePrice;
        [ObservableProperty]
        private decimal salePrice;
        [ObservableProperty]
        private int idCategory;
        [ObservableProperty]
        private string categoryName;

        // Constructor vacío
        public Product()
        {
        }

        //Cuando se Inserta:
        public Product(string nombreProducto, string codigoBarra, string tipoVenta, decimal precioCompra, decimal precioVenta, int idCategoria)
        {
            ProductName = nombreProducto;
            BarCode = codigoBarra;
            SaleType = tipoVenta;
            PurchasePrice = precioCompra;
            SalePrice = precioVenta;
            IdCategory = idCategoria;
            CategoryName = categoryName;
        }    
        
        //Constructor para consultar
        public Product(int idProducto, string nombreProducto, string codigoBarra, string tipoVenta, decimal precioCompra, decimal precioVenta, int idCategoria,string CategoryName)
        {
            IdProduct = idProducto;
            ProductName = nombreProducto;
            BarCode = codigoBarra;
            SaleType = tipoVenta;
            PurchasePrice = precioCompra;
            SalePrice = precioVenta;
            IdCategory = idCategoria;
            this.CategoryName = CategoryName;
        }
    }
}