using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Movements
{
    public partial class Tickets : ObservableValidator
    {
        public Tickets() 
        {
            IdTickets = 0;
            IdProduct = 0;
            RegisterDate = DateTime.Now;
            AmountRegister = 0;
            Notes = string.Empty;
        }
        [ObservableProperty]
        private int _idTickets;
        [ObservableProperty]
        private int _idProduct;
        [ObservableProperty]
        private DateTime _registerDate;
        [ObservableProperty]
        private decimal _amountRegister;
        [ObservableProperty]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        private string _notes;
    }
}
