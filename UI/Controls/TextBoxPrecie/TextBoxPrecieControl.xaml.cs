using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica de interacción para TextBoxPrecieControl.xaml
    /// </summary>
    public partial class TextBoxPrecieControl : UserControl
    {
        private long _cents = 0;
        // Bandera para evitar bucles infinitos entre la UI y el ViewModel
        private bool _isUpdating = false;

        public TextBoxPrecieControl()
        {
            InitializeComponent();
            ActualizarUI();
            DataObject.AddPastingHandler(Txt, PrecioTextBox_OnPaste);
        }

        // 🔹 Propiedad Dependency para el valor real (Decimal)
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(decimal),
                typeof(TextBoxPrecieControl),
                // BindsTwoWayByDefault es CRUCIAL para que retorne el valor al ViewModel
                new FrameworkPropertyMetadata(0m, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public decimal Value
        {
            get => (decimal)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // 🔹 Propiedad Hint
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register(
                nameof(Hint),
                typeof(string),
                typeof(TextBoxPrecieControl),
                new PropertyMetadata("Label Message"));

        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        // Detecta si el ViewModel cambia el valor externamente
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxPrecieControl)d;
            if (!control._isUpdating)
            {
                control._cents = (long)((decimal)e.NewValue * 100);
                control.ActualizarUI();
            }
        }

        // Actualiza el texto visual y la propiedad Value
        private void ActualizarUI()
        {
            _isUpdating = true; // Bloqueamos el evento recursivo

            decimal valor = _cents / 100m;
            Value = valor; // Esto empuja el número puro hacia el ViewModel

            Txt.Text = valor.ToString("C2", CultureInfo.CurrentCulture);
            Txt.CaretIndex = Txt.Text.Length;

            _isUpdating = false;
        }

        private void PrecioTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Txt.CaretIndex = Txt.Text.Length;
            e.Handled = true;
        }

        private void PrecioTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
                return;
            }

            int digito = e.Text[0] - '0';
            long nuevoValor = _cents * 10 + digito;

            // Límite máximo
            long maxCents = 9999999999;
            if (nuevoValor > maxCents) nuevoValor = maxCents;

            _cents = nuevoValor;
            ActualizarUI();
            e.Handled = true;
        }

        private void PrecioTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                _cents /= 10;
                ActualizarUI();
                e.Handled = true;
            }
        }

        private void PrecioTextBox_OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}
