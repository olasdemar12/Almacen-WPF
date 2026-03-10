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
using System.Windows.Markup;

namespace StockMasterControls
{
    /// <summary>
    /// Lógica de interacción para PanelControl.xaml
    /// </summary>
    [ContentProperty(nameof(PanelContent))]
    public partial class PanelControl : UserControl
    {
        public PanelControl()
        {
            InitializeComponent();
        }

        //Propiedad para el Titulo del Panel
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(PanelControl),
                new PropertyMetadata("Panel Title"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        //Estado para mostrar boton de Regresar
        public static readonly DependencyProperty ShowBackButtonProperty =
            DependencyProperty.Register(
                nameof(ShowBackButton),
                typeof(bool),
                typeof(PanelControl),
                new PropertyMetadata(true));

        public bool ShowBackButton
        {
            get { return (bool)GetValue(ShowBackButtonProperty); }
            set { SetValue(ShowBackButtonProperty, value); }
        }

        //Estado paara mostrar el Boton de Cerrar panel
        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register(
                nameof(ShowCloseButton),
                typeof(bool),
                typeof(PanelControl),
                new PropertyMetadata(true));

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        //Contenido a mostrar en el Panel
        public static readonly DependencyProperty PanelContentProperty =
            DependencyProperty.Register(
                nameof(PanelContent),
                typeof(object),
                typeof(PanelControl),
                new PropertyMetadata(null));

        public object PanelContent
        {
            get { return GetValue(PanelContentProperty); }
            set { SetValue(PanelContentProperty, value); }
        }

    }
}
