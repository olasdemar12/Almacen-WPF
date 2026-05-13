using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.ViewModels.Forms.Movements;
using Almacen_Sistema.Services.Movements.Contracts;
using MVVM.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProductModel = MVVM.Models.Product.Product;

namespace Almacen_Sistema.UI.Forms.Movements.Entry
{
    /// <summary>
    /// Lógica de interacción para FormEnrtyProducts.xaml
    /// </summary>
    public partial class FormEnrtyProducts : UserControl
    {
        public FormEnrtyProducts(TypeActionMovementChanges action, TransactionHistory transaction)
        {
            InitializeComponent();
            this.DataContext = new EntryFormViewModel(action, transaction);
        }

    }
}
