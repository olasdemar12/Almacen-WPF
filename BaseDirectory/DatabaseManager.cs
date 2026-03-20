using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Almacen_Sistema.BaseDirectory
{
    public class DatabaseManager
    {
        private static readonly Lazy<DatabaseManager> _instance =
            new Lazy<DatabaseManager>(() => new DatabaseManager());

        public static DatabaseManager Instance => _instance.Value;

        private readonly string _connectionString;

        private DatabaseManager()
        {
            string projectPath = GetProjectPath();
            string databasePath = Path.Combine(projectPath, "StockMaster.db");

            _connectionString = $"Data Source={databasePath};";
        }

        public SqliteConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        private static string GetProjectPath()
        {
            DirectoryInfo? currentDirectory =
                new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (currentDirectory != null)
            {
                bool hasCsproj = currentDirectory.GetFiles("*.csproj").Any();

                if (hasCsproj)
                {
                    return currentDirectory.FullName;
                }

                currentDirectory = currentDirectory.Parent;
            }

            throw new DirectoryNotFoundException(
                "No se pudo encontrar la carpeta del proyecto.");
        }
    }
}

