using Almacen_Sistema.Composition.EventsDefinitions.Movements;
using Almacen_Sistema.MVVM.Models.Movements;
using Almacen_Sistema.MVVM.ViewModels.Forms.Movements.ExitForm;
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

namespace Almacen_Sistema.UI.Forms.Movements_2
{
    /// <summary>
    /// Lógica de interacción para ExitFormControl.xaml
    /// </summary>
    public partial class ExitFormControl : UserControl
    {
        public ExitFormControl(TypeActionMovementChanges actionform, TransactionHistory transaction,decimal TotalStock = 0.00m)
        {
            InitializeComponent();
            this.DataContext = new ExitFormViewModel(actionform, transaction, TotalStock);
        }
    }
}
