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

namespace StockMasterControls
{
    /// <summary>
    /// Lógica de interacción para LoadingViewControl.xaml
    /// </summary>
    public partial class LoadingViewControl : UserControl
    {
        public LoadingViewControl()
        {
            InitializeComponent();
        }

        //Texto que muestra la pantalla de carga.
        public static readonly DependencyProperty TextLoadingProperty =
            DependencyProperty.Register(
                nameof(TextLoading),
                typeof(string),
                typeof(LoadingViewControl),
                new PropertyMetadata("Cargando..."));

        public string TextLoading
        {
            get { return (string)GetValue(TextLoadingProperty); }
            set { SetValue(TextLoadingProperty, value); }
        }

        //Propiedad encargada para mostrar cuando la pantalla de carga esta activa o no.
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register
            (
                nameof(IsLoading),
                typeof(bool),
                typeof(LoadingViewControl),
                new PropertyMetadata(true));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        //Color del ProgressCircle
        public static readonly DependencyProperty ProgressColorProperty =
            DependencyProperty.Register(
                nameof(ProgressColor),
                typeof(Brush),
                typeof(LoadingViewControl),
                new PropertyMetadata(Brushes.BlueViolet));

        public Brush ProgressColor
        {
            get { return (Brush)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        //Color del Texto de carga
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(
                nameof(TextColor),
                typeof(Brush),
                typeof(LoadingViewControl),
                new PropertyMetadata(Brushes.Black));

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
    }
}
