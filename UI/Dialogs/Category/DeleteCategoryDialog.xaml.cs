using Almacen_Sistema.MVVM.ViewModels.Dialogs;
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
namespace Almacen_Sistema.UI.Dialogs.Category
{
    /// <summary>
    /// Lógica de interacción para DeleteCategoryDialog.xaml
    /// </summary>
    public partial class DeleteCategoryDialog : UserControl
    {
        public DeleteCategoryDialog(CategoryModel category, ICategoryService service)
        {
            InitializeComponent();
            var ViewModel = new DeleteDialogViewModel(category, service);
            this.DataContext = ViewModel;
        }
    }
}
