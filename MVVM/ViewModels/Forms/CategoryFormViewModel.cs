using Almacen_Sistema.UI.Forms.Category;
using Almacen_Sistema.UI.Panels.Products;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MVVM.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.ViewModels.Forms
{
    public partial class CategoryFormViewModel:ObservableValidator
    {
        public CategoryFormViewModel(string title, Category category)
        {
            Title = title;
            //Designamos el contenido del Boton y Activamos el Formulario.
            switch(title)
            {
                case "Agregar Nueva Categoría":
                    ContentedButton = "Agregar Categoría";
                    IsEnable = true;
                    break;
                case "Editar Categoría":
                    ContentedButton = "Guardar Cambio";
                    IsEnable = true;
                    break;
            }
        }

        [ObservableProperty]
        private Category categoryObject;

        [ObservableProperty]
        private string _title = "Formulario Categoria";

        [ObservableProperty]
        private string _contentedButton;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool _isEnable;

        [ObservableProperty]
        private bool _isBusy;

        [RelayCommand]
        private async Task CloseForm()
        {
            IsEnable = false;
            //Cerrarmos el dialogo actual abierto
            DialogHost.CloseDialogCommand.Execute(null, null);
            //Despues esperamos 200ms para que se cierre el dialogo y no se sobrepongan los dialogos.
            await Task.Delay(200);
            //Creamos un Objeto para pasarlo al formulario.
            await DialogHost.Show(new CategorysManagementControl(), "DialogsRoot");
        }



    }
}
