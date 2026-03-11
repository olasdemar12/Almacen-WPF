using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM.Models.Product
{
    public class ProductRow : ObservableObject
    {
        // Atributos
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        public int IdCategoria { get; set; }
        public int CodigoBarra { get; set; }
        public string TipoVenta { get; set; }
        public decimal PrecioVenta { get; set; }

        // Constructor vacío
        public ProductRow()
        {
        }

        // Constructor con parámetros
        public ProductRow(int idProd, string nombre, string cat, int idCat, int codBarra, string tipoVenta, decimal precioVenta)
        {
            IdProducto = idProd;
            NombreProducto = nombre;
            Categoria = cat;
            IdCategoria = idCat;
            CodigoBarra = codBarra;
            TipoVenta = tipoVenta;
            PrecioVenta = precioVenta;
        }

        // Constructor que recibe Product
        public ProductRow(Product product)
        {
            IdProducto = product.IdProducto;
            NombreProducto = product.NombreProducto;
            IdCategoria = product.IdCategoria;
            CodigoBarra = int.Parse(product.CodigoBarra);
            TipoVenta = product.TipoVenta;
            PrecioVenta = product.PrecioVenta;
        }
    }
}
