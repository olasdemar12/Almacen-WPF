using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Login;
using Almacen_Sistema.Services.Data.Login;
using Almacen_Sistema.Services.Login.Contracts;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Almacen_Sistema.Services.Login.Implementations
{
    public class DatabaseAuthenticationService : IAuthenticationService
    {
        private readonly IUserAccessRepository userAccessRepository;

        //Constructor que recibe el repositorio de acceso a datos a través de inyección de dependencias
        public DatabaseAuthenticationService(IUserAccessRepository userAccessRepository)
        {
            this.userAccessRepository = userAccessRepository;
        }

        public async Task<UserAccess> CredentialValidation(string username, string password)
        {
            UserAccess userAccess;
            bool isValid = await userAccessRepository.AuthenticateUser(username, password);
            if (isValid)
            {
                userAccess = new UserAccess(userAccessRepository.IdUserAuthenticated,username,password);
            }
            else
            {
                userAccess = null;
            }

            return userAccess;
        }
    }
}
