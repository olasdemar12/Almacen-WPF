using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Movements
{
    public partial class Outputs : ObservableObject
    {
        public Outputs()
        {
            IdOutput = 0;
            IdProduct = 0;
            RegisterDate = DateTime.Now;
            AmountWithDrawn = 0;
            Notes = string.Empty;
        }

        public Outputs(int IdProduct, DateTime RegisterDate, decimal AmountWithDrawn, string Notes)
        {
            IdOutput = 0;
            this.IdProduct = IdProduct;
            this.RegisterDate = RegisterDate;
            this.AmountWithDrawn = AmountWithDrawn;
            this.Notes = Notes;
        }

        public Outputs(int IdOutput,int IdProduct, DateTime RegisterDate, decimal AmountWithDrawn, string Notes)
        {
            this.IdOutput = IdOutput;
            this.IdProduct = IdProduct;
            this.RegisterDate = RegisterDate;
            this.AmountWithDrawn = AmountWithDrawn;
            this.Notes = Notes;
        }

        [ObservableProperty]
        private int _idOutput;
        [ObservableProperty]
        private int _idProduct;
        [ObservableProperty]
        private DateTime _registerDate;
        [ObservableProperty]
        private decimal _amountWithDrawn;
        [ObservableProperty]
        private string _notes;
    }
}
