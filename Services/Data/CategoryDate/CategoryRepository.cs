using Almacen_Sistema.BaseDirectory;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using CategoryModel = MVVM.Models.Category.Category;

namespace Almacen_Sistema.Services.Data.CategoryDate
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository() { }
        public async Task<bool> DeleteCategory(int IdCategory)
        {
            SqliteConnection connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM Categorys WHERE IdCategory = $IdCategory";
                command.Parameters.AddWithValue("$IdCategory", IdCategory);

                int rowsAffects = command.ExecuteNonQuery();
                return rowsAffects > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un Error al Eliminar una Categoría", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }

        // TODO: Agregar el total de productos por categoría
        public async Task<List<CategoryModel>> GetAllCategory()
        {
            SqliteConnection? connection = null;
            List<CategoryModel>? categories = new List<CategoryModel>();
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Categorys";
                using SqliteDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    categories.Add(new CategoryModel(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        0
                        ));
                }

                reader.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurio un Error al consultar las Categoría existentes", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return categories;
            }
            finally
            {
                if (connection != null)
                {

                    connection.Close();
                }
            }
            return categories;
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
                reader.Close();
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
            SqliteConnection connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
UPDATE Categorys
SET CategoryName = $CategoryName
WHERE IdCategory = $IdCategory;

SELECT changes();
";
                command.Parameters.AddWithValue("$CategoryName", Category.NombreCategoria);
                command.Parameters.AddWithValue("$IdCategory", Category.IdCategoria);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurio un Error al Actualizar una Categoría", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if(connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
