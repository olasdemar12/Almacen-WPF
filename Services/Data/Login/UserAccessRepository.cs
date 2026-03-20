using Almacen_Sistema.BaseDirectory;
using Almacen_Sistema.MVVM.Models.Login;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.Services.Data.Login
{
    public class UserAccessRepository:IUserAccessRepository
    {
        public UserAccessRepository() { }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            SqliteConnection connection = null;
            UserAccess userAccess = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
SELECT * FROM UserAccess 
WHERE UserCredencial = $user 
AND PasswordUser = $password;
";
                command.Parameters.AddWithValue("$user", username.Trim());
                command.Parameters.AddWithValue("$password", password.Trim());

                using SqliteDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    userAccess = new UserAccess
                    {
                        IdUser = reader.GetInt32(0),
                        NameUser = reader.GetString(1),
                        PasswordUser = reader.GetString(2)
                    };
                    _IdUserAuthenticated = userAccess.IdUser;
                }
                await reader.CloseAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurrio un error al consultar las credenciales", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
            return userAccess != null;
        }

        private int _IdUserAuthenticated;
        public int IdUserAuthenticated { get => _IdUserAuthenticated;}
    }
}
