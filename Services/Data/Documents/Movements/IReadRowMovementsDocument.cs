using Almacen_Sistema.MVVM.Models.Movements.RowMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.Services.Data.Documents.Movements
{
    public interface IReadRowMovementsDocument
    {
        public Task<List<RowMovementsProductsDocument>?> SelectAllInformationDocument();
    }
}
