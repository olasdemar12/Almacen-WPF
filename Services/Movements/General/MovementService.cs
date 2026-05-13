using Almacen_Sistema.Services.Movements.Contracts;
using Almacen_Sistema.Services.Movements.Contracts.Panels;
using Almacen_Sistema.Services.Movements.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Movements.General
{
    public class MovementService : IMovementService
    {
        public MovementService()
        {
            _transactionService = new TransactionService();
            _panelServices = new PanelService();
        }
        public ITransactionService TransactionService => _transactionService;

        public IPanelServices PanelServices => _panelServices;

        private ITransactionService _transactionService;
        private IPanelServices _panelServices;
    }
}
