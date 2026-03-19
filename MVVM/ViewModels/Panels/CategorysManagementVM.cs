using Almacen_Sistema.UI.Forms.Category;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.MVVM.ViewModels.Panels
{
    public partial class CategorysManagementVM:ObservableObject
    {


        [RelayCommand]
        private async Task AddCategory()
        {
            //Cerrarmos el dialogo actual abierto
            DialogHost.CloseDialogCommand.Execute(null, null);
            //Despues esperamos 200ms para que se cierre el dialogo y no se sobrepongan los dialogos.
            await Task.Delay(200);
            //Creamos un Objeto para pasarlo al formulario.
            await DialogHost.Show(new CategoryFormView("Agregar Nueva Categoría",new CategoryModel()), "DialogsRoot");
        }
    }
}
