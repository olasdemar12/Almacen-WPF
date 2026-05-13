using Almacen_Sistema.MVVM.ViewModels.Login;
using Almacen_Sistema.MVVM.ViewModels.Pages;
using Almacen_Sistema.MVVM.ViewModels.Panels;
using Almacen_Sistema.Services.Category.Contracts;
using Almacen_Sistema.Services.Category.Implementations;
using Almacen_Sistema.Services.Data.CategoryDate;
using Almacen_Sistema.Services.Data.Login;
using Almacen_Sistema.Services.Data.Movements.TransactionHistory;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Login.Contracts;
using Almacen_Sistema.Services.Login.Implementations;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Movements.General;
using Almacen_Sistema.Services.Movements.Implementations;
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
            //Iniciamos los repositorios
            ICategoryRepository categoryRepository = new CategoryRepository();

            //Creamos un objeto de servicio de categorias y iniciamos con el repositorio
            ICategoryService categoryService = new CategoryService(categoryRepository);
            return new CategorysManagementVM(categoryService);

        }

        public static ProductViewModel CreateProductosViewModel()
        {
            //Iniciamos los repositorios
            IProductRepository productRepository = new ProductRepository();
            IProductReadCategoryService productReadCategoryService = new CategoryRepository();

            //Iniciamos el servicio de Productos para la consulta de estos mismo y las categorias guardadas.
            IProductService productService = new ProductService(productRepository,productReadCategoryService);

            return new ProductViewModel(productService);
        }

        public static MovementsViewModel CreateMovementsViewModel()
        {
            IMovementService movementService = new MovementService();
            return new MovementsViewModel(movementService);
        }
    }
    public class ServiceResult
    {
        public ServiceResult()
        {
            Message = string.Empty;
        }

        public ServiceResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public static ServiceResult Success(string message)
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult Failure(string message)
        {
            return new ServiceResult(false, message);
        }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() : base()
        {
        }

        public ServiceResult(bool isSuccess, string message): base(isSuccess, message)
        {
        }

        public ServiceResult(bool isSuccess, string message, T? data)
            : base(isSuccess, message)
        {
            Data = data;
        }

        public T? Data { get; set; }

        public static ServiceResult<T> Success(string message, T data)
        {
            return new ServiceResult<T>(true, message, data);
        }

        public static ServiceResult<T> Failure(string message)
        {
            return new ServiceResult<T>(false, message, default);
        }
    }
}
