using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM.Models.Category
{
    public partial class Category : ObservableObject
    {
        [ObservableProperty]
        private int idCategoria;
        [ObservableProperty]
        private string nombreCategoria;
        [ObservableProperty]
        private int totalProductos;


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
