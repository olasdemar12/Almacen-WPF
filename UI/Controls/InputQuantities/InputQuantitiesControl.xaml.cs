using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Linq;

namespace StockMasterControls
{
    /// <summary>
    /// Lógica de interacción para InputQuantitiesControl.xaml
    /// </summary>
    public partial class InputQuantitiesControl : UserControl
    {
        private static readonly Regex QuantityRegex = new(@"^\d*(\.\d{0,2})?$", RegexOptions.Compiled);
        private bool _isUpdatingText;
        private bool _isInternalQuantityChange;
        public InputQuantitiesControl()
        {
            InitializeComponent();

            DataObject.AddPastingHandler(QuantityTextBox, QuantityTextBox_Pasting);

            Validation.AddErrorHandler(this, InputQuantitiesControl_ValidationError);

            UpdateTextFromQuantity();
        }

        public decimal Quantity
        {
            get => (decimal)GetValue(QuantityProperty);
            set => SetValue(QuantityProperty, value);
        }

        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register(
                nameof(Quantity),
                typeof(decimal),
                typeof(InputQuantitiesControl),
                new FrameworkPropertyMetadata(
                    0.00m,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnQuantityChanged));

        public decimal Step
        {
            get => (decimal)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register(
                nameof(Step),
                typeof(decimal),
                typeof(InputQuantitiesControl),
                new PropertyMetadata(0.10m));

        public decimal Minimum
        {
            get => (decimal)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum),
                typeof(decimal),
                typeof(InputQuantitiesControl),
                new PropertyMetadata(0.00m));

        private static void OnQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (InputQuantitiesControl)d;

            if (!control._isInternalQuantityChange)
                control.UpdateTextFromQuantity();
        }

        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string previewText = GetPreviewText(e.Text);
            e.Handled = !IsValidQuantityText(previewText);
        }

        private void QuantityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void QuantityTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(DataFormats.Text))
            {
                e.CancelCommand();
                return;
            }

            string pastedText = e.DataObject.GetData(DataFormats.Text)?.ToString() ?? string.Empty;
            string previewText = GetPreviewText(pastedText);

            if (!IsValidQuantityText(previewText))
                e.CancelCommand();
        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingText)
                return;

            string text = QuantityTextBox.Text;

            // Estados temporales permitidos mientras el usuario escribe.
            if (string.IsNullOrWhiteSpace(text) || text == ".")
                return;

            if (decimal.TryParse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
            {
                _isInternalQuantityChange = true;
                Quantity = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                _isInternalQuantityChange = false;
            }
        }

        private void QuantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NormalizeCurrentText();
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            decimal currentQuantity = GetCurrentQuantity();
            SetQuantity(currentQuantity - Step);
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            decimal currentQuantity = GetCurrentQuantity();
            SetQuantity(currentQuantity + Step);
        }

        private string GetPreviewText(string newText)
        {
            string currentText = QuantityTextBox.Text;
            int selectionStart = QuantityTextBox.SelectionStart;
            int selectionLength = QuantityTextBox.SelectionLength;

            return currentText.Remove(selectionStart, selectionLength)
                              .Insert(selectionStart, newText);
        }

        private static bool IsValidQuantityText(string text)
        {
            return QuantityRegex.IsMatch(text);
        }

        private decimal GetCurrentQuantity()
        {
            if (decimal.TryParse(QuantityTextBox.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
                return value;

            return Quantity;
        }

        private void NormalizeCurrentText()
        {
            if (!decimal.TryParse(QuantityTextBox.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
                value = Minimum;

            SetQuantity(value);
        }

        private void SetQuantity(decimal value)
        {
            if (value < Minimum)
                value = Minimum;

            value = Math.Round(value, 2, MidpointRounding.AwayFromZero);

            _isInternalQuantityChange = true;
            Quantity = value;
            _isInternalQuantityChange = false;

            UpdateTextFromQuantity();
        }

        private void UpdateTextFromQuantity()
        {
            if (QuantityTextBox == null)
                return;

            _isUpdatingText = true;
            QuantityTextBox.Text = Quantity.ToString("0.00", CultureInfo.InvariantCulture);
            QuantityTextBox.CaretIndex = QuantityTextBox.Text.Length;
            _isUpdatingText = false;
        }

        public string ValidationErrorMessage
        {
            get => (string)GetValue(ValidationErrorMessageProperty);
            private set => SetValue(ValidationErrorMessageProperty, value);
        }

        public static readonly DependencyProperty ValidationErrorMessageProperty =
            DependencyProperty.Register(
                nameof(ValidationErrorMessage),
                typeof(string),
                typeof(InputQuantitiesControl),
                new PropertyMetadata(string.Empty));

        private void InputQuantitiesControl_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            UpdateValidationErrorMessage();
        }

        private void UpdateValidationErrorMessage()
        {
            ValidationError validationError = Validation.GetErrors(this).FirstOrDefault();

            ValidationErrorMessage = validationError?.ErrorContent?.ToString() ?? string.Empty;
        }
    }
}

