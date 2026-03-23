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
    /// Lógica de interacción para NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        public NotificationControl()
        {
            InitializeComponent();

        }

        public static readonly DependencyProperty TypeProperty =
             DependencyProperty.Register(
                 nameof(Type),
                 typeof(PackIconKind),
                 typeof(NotificationControl),
                 new PropertyMetadata(PackIconKind.None)
                 );

        public PackIconKind Type
        {
            get { return (PackIconKind)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty ColorIconProperty =
            DependencyProperty.Register(
                nameof(ColorIcon),
                typeof(Brush),
                typeof(NotificationControl),
                new PropertyMetadata(Brushes.Black)
                );

        public Brush ColorIcon
        {
            get { return (Brush)GetValue(ColorIconProperty); }
            set { SetValue(ColorIconProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(string),
                typeof(NotificationControl),
                new PropertyMetadata(string.Empty)
                );

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty ShowNotificationProperty =
            DependencyProperty.Register(
                nameof(ShowNotification),
                typeof(bool),
                typeof(NotificationControl),
                new PropertyMetadata(false)
                );

        public bool ShowNotification
        {
            get { return (bool)GetValue(ShowNotificationProperty); }
            set { SetValue(ShowNotificationProperty, value); }
        }
    }
}
