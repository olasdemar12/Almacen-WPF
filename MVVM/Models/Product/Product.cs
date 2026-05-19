using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models.Product
{
    public enum StateProduct
    {
        Inactivo,
        Activo
    }


    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private int? idProduct;
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
        private int? idCategory;
        [ObservableProperty]
        private int active;
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

    public struct DocumentProductRow
    {
        private int _idCategory;
        private string _productName;
        private string _categoryName;
        private StateProduct _stateProduct;
        private decimal _priceSale;
        private string _typeSale;

        public int IdCategory { get => _idCategory; set => _idCategory = value; }
        public string ProductName { get => _productName; set => _productName = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }
        public StateProduct StateProduct { get => _stateProduct; set => _stateProduct = value; }
        public decimal PriceSale { get => _priceSale; set => _priceSale = value; }
        public string TypeSale { get => _typeSale; set => _typeSale = value; }

        public DocumentProductRow()
        {
            _idCategory = 0;
            _productName = string.Empty;
            _stateProduct = StateProduct.Inactivo;
            _priceSale = 0.00m;
            _typeSale = "Unidad";
        }

        public DocumentProductRow(int IdCategory, string ProductName, string CategoryName, StateProduct StateProduct, decimal PriceSale, string TypeSale)
        {
            this.IdCategory = IdCategory;
            this.ProductName = ProductName;
            this.CategoryName = CategoryName;
            this.StateProduct = StateProduct;
            this.PriceSale = PriceSale;
            this.TypeSale = TypeSale;
        }
    }
}