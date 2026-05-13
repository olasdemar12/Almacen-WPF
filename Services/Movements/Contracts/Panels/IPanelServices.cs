using Almacen_Sistema.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Movements.Contracts.Panels
{
    public interface IPanelServices
    {
        // sub servicio para obtener los productos registrados
        public IProductService ProductService { get; }

        //sub servicio para obtener los stocks de cada producto registrado.
        public ICurrentStocksService CurrentStocksService { get; }
    }
}
