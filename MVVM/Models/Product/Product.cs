using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models.Product
{
    public class Product : ObservableObject
    {
        // Atributos
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CodigoBarra { get; set; }
        public string TipoVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdCategoria { get; set; }

        // Constructor vacío
        public Product()
        {
        }

        // Constructor con parámetros
        public Product(int idProducto, string nombreProducto, string codigoBarra,
                       string tipoVenta, decimal precioCompra, decimal precioVenta, int idCategoria)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            CodigoBarra = codigoBarra;
            TipoVenta = tipoVenta;
            PrecioCompra = precioCompra;
            PrecioVenta = precioVenta;
            IdCategoria = idCategoria;
        }

        // Constructor que recibe ProductRow
        public Product(ProductRow fila)
        {
            IdProducto = fila.IdProducto;
            NombreProducto = fila.NombreProducto;
            CodigoBarra = fila.CodigoBarra.ToString();
            TipoVenta = fila.TipoVenta;
            PrecioVenta = fila.PrecioVenta;
            IdCategoria = fila.IdCategoria;
        }
    }
}