using Almacen_Sistema.Services.Data.CategoryDate;
using Almacen_Sistema.Services.Data.Movements.CurrentStock;
using Almacen_Sistema.Services.Data.ProductDate;
using Almacen_Sistema.Services.Movements.Contracts.Panels;
using Almacen_Sistema.Services.Product.Contracts;
using Almacen_Sistema.Services.Product.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Movements.Implementations
{
    public class PanelService : IPanelServices
    {
        public PanelService() 
        {

            IProductRepository productRepository = new ProductRepository();
            IProductReadCategoryService productReadCategoryService = new CategoryRepository();
            _productService = new ProductService(productRepository, productReadCategoryService);

            _currentStocksService = new CurrentStockService();
        }

        public IProductService ProductService => _productService;

        public ICurrentStocksService CurrentStocksService => _currentStocksService;


        private IProductService _productService;
        private ICurrentStocksService _currentStocksService;
    }
}
