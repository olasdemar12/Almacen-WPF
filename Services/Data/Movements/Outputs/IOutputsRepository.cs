using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutputsModel = Almacen_Sistema.MVVM.Models.Movements.Outputs;
namespace Almacen_Sistema.Services.Data.Movements.Outputs
{
    public interface IOutputsRepository
    {
        public Task<List<OutputsModel>?> SelectAllOutputs();
        public Task<OutputsModel?> SelectOutputByIdAsync(int idOutput);
        public Task<bool> InsertOutputAsync(OutputsModel output);
        public Task<bool> UpdateOutputAsync(OutputsModel output);
        public Task<bool> DeleteOutputAsync(int idOutput);
    }
}
