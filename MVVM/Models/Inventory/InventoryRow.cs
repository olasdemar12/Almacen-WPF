using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Movements.Inventory
{
    public enum StateStockInventory
    {
        Normal,
        Bajo,
        Agotado
    }
    public partial class InventoryRow : ObservableObject
    {
        public InventoryRow()
        {
            IdStock = 0;
            IdProduct = 0;
            IdCategory = 0;
            TotalStock = 0.00m;
            MiniumStock = 0.00m;
            StateStock = StateStockInventory.Agotado;
            LastMovement = DateTime.Now;
        }

        public InventoryRow(int idStock, int idProduct, int idCategory, string productName, string categoryName,decimal totalStock, decimal miniumStock, StateStockInventory stateStock, DateTime lastMovement)
        {
            IdStock = idStock;
            IdProduct = idProduct;
            IdCategory = idCategory;
            ProductName = productName;
            CategoryName = categoryName;
            TotalStock = totalStock;
            MiniumStock = miniumStock;
            StateStock = stateStock;
            LastMovement = lastMovement;
        }

        [ObservableProperty]
        private int _idStock;
        [ObservableProperty]
        private int _idProduct;
        [ObservableProperty]
        private int _idCategory;
        [ObservableProperty]
        private string _productName;
        [ObservableProperty]
        private string _categoryName;
        [ObservableProperty]
        private decimal _totalStock;
        [ObservableProperty]
        private decimal _miniumStock;
        [ObservableProperty]
        private StateStockInventory _stateStock;
        [ObservableProperty]
        private DateTime _lastMovement;

    }

    public struct TransactionInventoryProductRow
    {
        private DateTime _registerDate;
        private TypeMovementTransaction _typeMovement;
        private decimal _amount;

        public DateTime RegisterDate { get => _registerDate; set => _registerDate = value; }
        public TypeMovementTransaction TypeMovement { get => _typeMovement; set => _typeMovement = value; }
        public decimal Amount { get => _amount; set => _amount = value; }

        public TransactionInventoryProductRow(DateTime registerDate, TypeMovementTransaction transactionMovement, decimal amount)
        {
            RegisterDate = registerDate;
            TypeMovement = transactionMovement;
            Amount = amount;
        }
    }
}
