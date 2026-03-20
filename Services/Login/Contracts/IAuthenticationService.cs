using Almacen_Sistema.MVVM.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Login.Contracts
{
    public interface IAuthenticationService
    {
        public Task<UserAccess> CredentialValidation(string username, string password);
    }
}
