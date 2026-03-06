using Almacen_Sistema.MVVM.Models.Login;
using Almacen_Sistema.Services.Login.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Login.Implementations
{
    public class DatabaseAuthenticationService : IAuthenticationService
    {
        public UserAccess AuthenticateUser(string username, string password)
        {
            var user = UserAccess.Users
                        .FirstOrDefault(u =>
                            u.NameUser == username &&
                            u.PasswordUser == password);

            return user; // Retorna objeto si existe, null si no
        }
    }
}
