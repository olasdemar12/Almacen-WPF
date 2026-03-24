using Almacen_Sistema.MVVM.ViewModels.Login;
using Almacen_Sistema.MVVM.ViewModels.Pages;
using Almacen_Sistema.MVVM.ViewModels.Panels;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.Services.Category.Implementations;
using Almacen_Sistema.Services.Data.CategoryDate;
using Almacen_Sistema.Services.Data.Login;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Login.Contracts;
using Almacen_Sistema.Services.Login.Implementations;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Composition
{
    public static class Bootstrapper
    {
        public static LoginViewModel CreateLoginViewModel()
        {
            //Iniciamos el repositorio de acceso a datos
            IUserAccessRepository userAccessRepository = new UserAccessRepository();

            //Iniciamos el servicio encargado de validar las credenciales del usuario
            IAuthenticationService authenticationService = new DatabaseAuthenticationService(userAccessRepository);

            //Finalmente, creamos la instancia del ViewModel de Login, inyectando el servicio de autenticación
            return new LoginViewModel(authenticationService);
        }

        public static CategorysManagementVM CreateCategorysManagementVM()
        {
            ICategoryRepository categoryRepository = new CategoryRepository();
            ICategoryService categoryService = new CategoryService(categoryRepository);
            return new CategorysManagementVM(categoryService);

        }

        public static ProductViewModel CreateProductosViewModel()
        {
            IProductRepository productRepository = new ProductRepository();
            IProductReadCategoryService productReadCategoryService = new CategoryRepository();

            IProductService productService = new ProductService(productRepository,productReadCategoryService);

            return new ProductViewModel(productService);
        }
    }

    public class ServiceResult<T>
    {
        public ServiceResult(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        public ServiceResult()
        {
        }

        public ServiceResult(bool isSuccess, string message, T? data)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
