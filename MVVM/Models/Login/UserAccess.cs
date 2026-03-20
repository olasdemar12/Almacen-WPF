using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Login
{
        public class UserAccess : ObservableObject
        {

            // 🔹 Campos privados
            private int idUser;
            private string nameUser;
            private string passwordUser;

            // 🔹 Propiedades públicas
            public int IdUser
            {
                get => idUser;
                set => SetProperty(ref idUser, value);
            }

            public string NameUser
            {
                get => nameUser;
                set => SetProperty(ref nameUser, value);
            }

            public string PasswordUser
            {
                get => passwordUser;
                set => SetProperty(ref passwordUser, value);
            }

            // 🔹 Constructor sin parámetros
            public UserAccess()
            {
            }

            // 🔹 Constructor para crear objetos
            public UserAccess(int idUser, string nameUser, string passwordUser)
            {
                IdUser = idUser;
                NameUser = nameUser;
                PasswordUser = passwordUser;
            }

            // 🔹 Constructor para extraer/copiar objetos
            public UserAccess(UserAccess user)
            {
                IdUser = user.IdUser;
                NameUser = user.NameUser;
                PasswordUser = user.PasswordUser;
            }
        }
}
