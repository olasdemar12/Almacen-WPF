using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Movements
{
    public enum TypeMovementTransaction
    {
        Entrada,
        Salida
    }
    public partial class TransactionHistory:ObservableObject
    {
        //Constructor sin Parametros:
        public TransactionHistory()
        {
            IdMovement = 0;
            IdProduct = 0;
            TypeMovement = TypeMovementTransaction.Entrada;
            Quantity = 0;
            RegisterDate = DateTime.Now;
            Notes = string.Empty;
            Confirmed = false;
        }

        //Constructor Para Insert:
        public TransactionHistory(int idProduct, TypeMovementTransaction typeMovement, decimal quantity, DateTime register, string notes, bool confirmed)
        {
            IdProduct = idProduct;
            TypeMovement = typeMovement;
            Quantity = quantity;
            RegisterDate = register;
            Notes = notes;
            Confirmed = confirmed;
        }

        public TransactionHistory(int idMovement, int idProduct, DateTime register, TypeMovementTransaction typeMovement, string productName, decimal quantity, string notes, bool confirmed)
        {
            IdMovement = idMovement;
            IdProduct = idProduct;
            RegisterDate = register;
            TypeMovement = typeMovement;
            ProductName = productName;
            Quantity = quantity;
            Notes = notes;
            Confirmed = confirmed;
        }

        [ObservableProperty]
        private int _idMovement;
        [ObservableProperty]
        private int _idProduct;
        [ObservableProperty]
        private string _productName;
        [ObservableProperty]
        private TypeMovementTransaction _typeMovement;
        [ObservableProperty]
        private decimal _quantity;
        [ObservableProperty]
        private DateTime _registerDate;
        [ObservableProperty]
        private string _notes;
        [ObservableProperty]
        private bool _confirmed;
    }
}
