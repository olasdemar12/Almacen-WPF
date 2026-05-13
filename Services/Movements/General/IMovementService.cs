using Almacen_Sistema.Services.Data.Movements.TransactionHistory;
using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Movements.Contracts.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Movements.General
{
    public interface IMovementService
    {
        //Servicio del modulo movimientos
        ITransactionService TransactionService { get; }
        //Servicio de Paneles de Movimientos
        IPanelServices PanelServices { get; }

    }
}
