using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM.Models.Category
{
    public class Category : ObservableObject
    {
        // Atributos
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int TotalProductos { get; set; }

        // Lista de categorías
        public List<Category> ListaCategorias { get; set; } = new List<Category>();

        // Constructor 
        public Category()
        {
        }

        // Constructor 
        public Category(int id, string nombre, int productos)
        {
            IdCategoria = id;
            NombreCategoria = nombre;
            TotalProductos = productos;
        }
    }
}
