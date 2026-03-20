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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockMasterControls
{
    /// <summary>
    /// Lógica de interacción para LoadingButtonControl.xaml
    /// </summary>
    [ContentProperty(nameof(ButtonContent))]
    public partial class LoadingButtonControl : UserControl
    {
        public LoadingButtonControl()
        {
            InitializeComponent();
        }

        //Color por defecto para todo el boton:
        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register(
                nameof(ButtonColor),
                typeof(Brush),
                typeof(LoadingButtonControl),
                new PropertyMetadata(Brushes.LightGray));

        public Brush ButtonColor
        {
            get { return (Brush)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        //Contenido del boton:
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register(
                nameof(ButtonContent),
                typeof(object),
                typeof(LoadingButtonControl),
                new PropertyMetadata(null));

        public object ButtonContent
        {
            get { return GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }


        //Comando a ejecutar al hacer click en el boton:
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(LoadingButtonControl),
                new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = 
            DependencyProperty.Register(
                nameof(CommandParameter), 
                typeof(object), 
                typeof(LoadingButtonControl), 
                new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }


        //Estado de carga del boton:
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(
                nameof(IsLoading),
                typeof(bool),
                typeof(LoadingButtonControl),
                new PropertyMetadata(false));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            //Mantenemos las dimensiones del boton para que no se redimensione al cambiar el contenido por el spinner:
            var actualwid = button.ActualWidth;
            var actualhei = button.ActualHeight;
            btn.Width = actualwid;
            btn.Height = actualhei;
        }
    }
}
