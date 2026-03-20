using Almacen_Sistema.BaseDirectory;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Data.CategoryDate
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository() { }
        public async Task<bool> DeleteCategory(int IdCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExists(string CategoryName)
        {
            SqliteConnection connection = null;
            bool ExistCategory = false;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
SELECT * FROM
Categorys
WHERE CategoryName = $CategoryName
LIMIT 1
";

                command.Parameters.AddWithValue("$CategoryName", CategoryName);
                using SqliteDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                    ExistCategory = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurio un Error al Consultar la existencia de una Categoría", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                ExistCategory = false;
            }
            finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
            return ExistCategory;
        }

        public async Task<CategoryModel> InsertCategory(string CategoryName)
        {
            SqliteConnection connection = null;
            CategoryModel CategoryNew;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
INSERT INTO Categorys 
(CategoryName) 
VALUES 
($CategoryName);
SELECT last_insert_rowid();
";
                command.Parameters.AddWithValue("$CategoryName", CategoryName);

                long IdCategory = (long)await command.ExecuteScalarAsync();
                CategoryNew = new CategoryModel
                {
                    IdCategoria = (int)IdCategory,
                    NombreCategoria = CategoryName
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un Error al Insertar una Categoría", MessageBoxButton.OK,MessageBoxImage.Error);
                CategoryNew = null;
                return CategoryNew;

            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }

            return CategoryNew;
        }

        public async Task<bool> UpdateCategory(CategoryModel Category)
        {
            throw new NotImplementedException();
        }
    }
}
