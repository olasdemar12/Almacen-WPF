using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Data.Login
{
    public interface IUserAccessRepository
    {
        //CredentialValidation
        public Task<bool> AuthenticateUser(string username, string password);

        //Propiedad para almacenar el Id del usuario autenticado

        public int IdUserAuthenticated { get; }
    }
}
