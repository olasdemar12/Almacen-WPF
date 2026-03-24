using Almacen_Sistema.BaseDirectory;
using Microsoft.Data.Sqlite;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CategoryModel = MVVM.Models.Category.Category;
using ProductModel = MVVM.Models.Product.Product;
namespace Almacen_Sistema.Services.Data.ProductDate
{
    public class ProductRepository : IProductRepository
    {
        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertProductAsync(ProductModel product)
        {
            SqliteConnection connection = null;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"
INSERT INTO Products (ProductName,BarCode,TypeSale,PurchasePrice,SalePrice,IdCategory)
VALUES ($ProductName,$BarCode,$TypeSale,$PurchasePrice,$SalePrice,$IdCategory);
SELECT last_insert_rowid();
";
                command.Parameters.AddWithValue("$ProductName", product.ProductName);
                command.Parameters.AddWithValue("$BarCode", product.BarCode);
                command.Parameters.AddWithValue("$TypeSale", product.SaleType);
                command.Parameters.AddWithValue("$PurchasePrice", product.PurchasePrice);
                command.Parameters.AddWithValue("$SalePrice", product.SalePrice);
                command.Parameters.AddWithValue("$IdCategory", product.IdCategory);

                long? IdProduct = (long?)await command.ExecuteScalarAsync() ?? 0L;
                product.IdProduct = (int)IdProduct;
                return IdProduct > 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurrio un Error al Insertar Producto", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }
        }

        public async Task<bool> UpdateProductAsync(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProductAsync(int IdProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }

        //Validar nombre y codigo de barras
        public async Task<ProductRepositoryResult> ValidateProductAsync(ActionRegister action, ProductModel product)
        {
            ProductRepositoryResult result = new ProductRepositoryResult();
            var NameValidation = ValidateNameProductAsync(action, product);
            var CodeBarValidation = ValidateBarCodeProductAsync(action, product);
            await Task.WhenAll(NameValidation, CodeBarValidation);
            result.NameExists = await NameValidation;
            result.BarcodeExists = await CodeBarValidation;
            return result;
        }

        private async Task<bool> ValidateNameProductAsync(ActionRegister action, ProductModel product)
        {
            SqliteConnection connection = null;
            int IdProduct = 0;
            bool result = false;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT IdProduct FROM Products WHERE ProductName = $ProductName";
                command.Parameters.AddWithValue("$ProductName", product.ProductName);
                using SqliteDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    IdProduct = reader.IsDBNullAsync(0).Result ? 0 : reader.GetInt32(0) ;
                }

                switch(action)
                {
                    case ActionRegister.AddProduct:
                        result = IdProduct == 0;
                        break;
                    case ActionRegister.UpddateProduct:
                        if(IdProduct == product.IdProduct)
                        {
                            result = true;
                        }
                        else if(IdProduct != 0)
                        {
                            result = false;
                        }
                        else if(IdProduct == 0)
                        {
                            result = true;
                        }
                        break;
                }
                return result;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurio un Error al intentar validar el Nombre de un Producto.", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return result;
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }

        private async Task<bool> ValidateBarCodeProductAsync(ActionRegister action, ProductModel product)
        {
            SqliteConnection connection = null;
            int IdProduct = 0;
            bool result = false;
            try
            {
                connection = DatabaseManager.Instance.CreateConnection();
                await connection.OpenAsync();

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT IdProduct FROM Products WHERE BarCode = $BarCode";
                command.Parameters.AddWithValue("$BarCode", product.BarCode);
                using SqliteDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    IdProduct = reader.IsDBNullAsync(0).Result ? 0 : reader.GetInt32(0);
                }

                switch (action)
                {
                    case ActionRegister.AddProduct:
                        result = IdProduct == 0;
                        break;
                    case ActionRegister.UpddateProduct:
                        if (IdProduct == product.IdProduct)
                        {
                            result = true;
                        }
                        else if (IdProduct != 0)
                        {
                            result = false;
                        }
                        else if (IdProduct == 0)
                        {
                            result = true;
                        }
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurio un Error al intentar validar el Codigo de Barra de un Producto.", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return result;
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                }
            }
        }

    }
}
