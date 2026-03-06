using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para IconTextButtonControl.xaml
    /// </summary>
    public partial class IconTextButtonControl : UserControl
    {
        public IconTextButtonControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty =
                DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(IconTextButtonControl),
                new PropertyMetadata(PackIconKind.Plus));

        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(IconTextButtonControl),
                new PropertyMetadata("TextButton"));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
               DependencyProperty.Register(
               nameof(Command),
               typeof(ICommand),
               typeof(IconTextButtonControl),
               new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty ColorButtonProperty =
        DependencyProperty.Register(
        nameof(ColorButton),
        typeof(Brush),
        typeof(IconTextButtonControl),
        new PropertyMetadata(Brushes.Blue));

        public Brush ColorButton
        {
            get => (Brush)GetValue(ColorButtonProperty);
            set => SetValue(ColorButtonProperty, value);
        }

        public static readonly DependencyProperty ContentColorButtonProperty =
        DependencyProperty.Register(
        nameof(ContentColorButton),
        typeof(Brush),
        typeof(IconTextButtonControl),
        new PropertyMetadata(Brushes.White));

        public Brush ContentColorButton
        {
            get => (Brush)GetValue(ContentColorButtonProperty);
            set => SetValue(ContentColorButtonProperty, value);
        }
    }
}
