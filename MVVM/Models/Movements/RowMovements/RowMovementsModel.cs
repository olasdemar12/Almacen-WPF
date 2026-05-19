using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Sistema.MVVM.Models.Movements.RowMovements
{
    public struct RowMovementsProductsDocument
    {
        private int _idProduct;
        private int _idCategory;
        private DateTime _registerDate;
        private TypeMovementTransaction _movementTransaction;
        private string _productName;
        private string _categoryName;
        private decimal _quantity;

        public int IdProduct { get =>  _idProduct;}
        public int IdCategory { get => _idCategory;}
        public DateTime RegisterDate { get => _registerDate;}
        public TypeMovementTransaction MovementTransaction { get => _movementTransaction;}
        public string ProductName { get => _productName;}
        public string CategoryName { get => _categoryName;}
        public decimal Quantity { get => _quantity; }

        public RowMovementsProductsDocument(int IdProduct, int IdCategory, DateTime RegisterDate, TypeMovementTransaction MovementTransaction, string ProductName, string CategoryName, decimal Quantity)
        {
            _idProduct = IdProduct;
            _idCategory = IdCategory;
            _registerDate = RegisterDate;
            _movementTransaction = MovementTransaction;
            _productName = ProductName;
            _categoryName = CategoryName;
            _quantity = Quantity;
        }

        public RowMovementsProductsDocument()
        {
            _idProduct = 0;
            _idCategory = 0;
            _registerDate = DateTime.Now;
            _movementTransaction = TypeMovementTransaction.Entrada;
            _productName = string.Empty;
            _categoryName = string.Empty;
            _quantity = 0.00m;
        }
    }
}
