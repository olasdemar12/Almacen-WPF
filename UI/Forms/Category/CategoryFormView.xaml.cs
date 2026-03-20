using Almacen_Sistema.MVVM.ViewModels.Forms;
using Almacen_Sistema.Services.Category.Contracts;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.UI.Forms.Category
{
    /// <summary>
    /// Lógica de interacción para CategoryFormView.xaml
    /// </summary>
    public partial class CategoryFormView : UserControl
    {
        public CategoryFormView(string title, CategoryModel category,ICategoryService categoryService)
        {
            InitializeComponent();
            var ViewModel = new CategoryFormViewModel(title,category,categoryService);
            this.DataContext = ViewModel;
        }
    }
}
