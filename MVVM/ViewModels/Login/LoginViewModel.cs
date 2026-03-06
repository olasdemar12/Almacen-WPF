using Almacen_Sistema.Services.Login.Contracts;
using Almacen_Sistema.Services.Login.Implementations;
using Almacen_Sistema.UI.Dialogs.Login;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Almacen_Sistema.MVVM.ViewModels.Login
{
    public partial class LoginViewModel:ObservableValidator
    {

        //Definicion de Propiedades:
        [ObservableProperty]
        [Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(5,ErrorMessage = "Mínimo 5 caracteres")]
        [MaxLength(40, ErrorMessage = "Maximo 40 caracteres")]
        private string _username;

        [ObservableProperty]
        [Required(ErrorMessage = "Campo obligatario")]
        [MinLength(5, ErrorMessage = "Mínimo 5 caracteres")]
        [MaxLength(40, ErrorMessage = "Maximo 40 caracteres")]
        private string _passworduser;

        public Action CloseLogin { get; set; }

        [ObservableProperty]
        private bool _isBusy;

        private IAuthenticationService _login;


        [RelayCommand]
        private async Task Authentication(object parameter)
        {
            ValidateAllProperties();
            if (HasErrors) return;
            IsBusy = true;
            await Task.Delay(2000);
            var result =  _login.AuthenticateUser(Username, Passworduser);
            if (result != null)
            {
                CloseLogin?.Invoke();
            }
            else
            {
                IsBusy = false;
                var dialogData = new
                {
                    Titulo = "Usuario o contraseña incorrecto",
                    Mensaje = "Compruebe su usuario y contraseña\npara volver a intentarlo"
                };

                var view = new ErrorLoginDialog
                {
                    DataContext = dialogData
                };
                await DialogHost.Show(view, "RootDialog");
            }
                IsBusy = false;
        }

        public LoginViewModel()
        {
            _login = new DatabaseAuthenticationService();
        }

    }
}
