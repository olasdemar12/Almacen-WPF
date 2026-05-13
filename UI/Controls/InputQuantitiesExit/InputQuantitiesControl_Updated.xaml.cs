using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockMasterControls
{
    /// <summary>
    /// Control reutilizable para capturar cantidades decimales.
    ///
    /// Reglas principales:
    /// - Quantity es la cantidad que el usuario selecciona o escribe.
    /// - Los botones respetan Minimum y Maximum.
    /// - La escritura manual permite valores mayores a Maximum para que el ViewModel
    ///   pueda mostrar un mensaje de validacion personalizado.
    /// - El control no calcula stock disponible; eso debe hacerlo el ViewModel.
    /// </summary>
    public partial class InputQuantitiesExitControl : UserControl
    {
        private static readonly Regex QuantityRegex = new(@"^\d*(\.\d{0,2})?$", RegexOptions.Compiled);

        private bool _isUpdatingText;
        private bool _isInternalQuantityChange;

        public InputQuantitiesExitControl()
        {
            InitializeComponent();

            DataObject.AddPastingHandler(QuantityTextBox, QuantityTextBox_Pasting);
            Validation.AddErrorHandler(this, InputQuantitiesExitControl_ValidationError);

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
                typeof(InputQuantitiesExitControl),
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
                typeof(InputQuantitiesExitControl),
                new PropertyMetadata(1.00m));

        public decimal Minimum
        {
            get => (decimal)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum),
                typeof(decimal),
                typeof(InputQuantitiesExitControl),
                new PropertyMetadata(0.00m));

        public decimal Maximum
        {
            get => (decimal)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum),
                typeof(decimal),
                typeof(InputQuantitiesExitControl),
                new PropertyMetadata(decimal.MaxValue));

        public string ValidationErrorMessage
        {
            get => (string)GetValue(ValidationErrorMessageProperty);
            private set => SetValue(ValidationErrorMessageProperty, value);
        }

        public static readonly DependencyProperty ValidationErrorMessageProperty =
            DependencyProperty.Register(
                nameof(ValidationErrorMessage),
                typeof(string),
                typeof(InputQuantitiesExitControl),
                new PropertyMetadata(string.Empty));

        private static void OnQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (InputQuantitiesExitControl)d;

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
            // Ejemplo: "", ".", "10."
            if (string.IsNullOrWhiteSpace(text) || text == ".")
            {
                SetCurrentQuantity(Minimum, updateText: false);
                return;
            }

            if (decimal.TryParse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
            {
                // Importante:
                // Al escribir manualmente NO limitamos contra Maximum.
                // Esto permite que el ViewModel muestre el mensaje:
                // "La cantidad no puede superar el stock disponible".
                value = NormalizeMinimum(value);
                value = RoundQuantity(value);

                SetCurrentQuantity(value, updateText: false);
            }
        }

        private void QuantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NormalizeCurrentText();
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            decimal currentQuantity = GetCurrentQuantity();
            decimal newQuantity = currentQuantity - GetValidStep();

            // Los botones si respetan el rango permitido.
            SetQuantityFromButton(newQuantity);
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            decimal currentQuantity = GetCurrentQuantity();
            decimal newQuantity = currentQuantity + GetValidStep();

            // Los botones si respetan el rango permitido.
            SetQuantityFromButton(newQuantity);
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

            // En LostFocus solo normalizamos formato y minimo.
            // No limitamos contra Maximum para conservar el error de validacion si el usuario escribio de mas.
            value = NormalizeMinimum(value);
            value = RoundQuantity(value);

            SetCurrentQuantity(value, updateText: true);
        }

        private void SetQuantityFromButton(decimal value)
        {
            value = NormalizeMinimum(value);
            value = NormalizeMaximum(value);
            value = RoundQuantity(value);

            SetCurrentQuantity(value, updateText: true);
        }

        private void SetCurrentQuantity(decimal value, bool updateText)
        {
            _isInternalQuantityChange = true;
            SetCurrentValue(QuantityProperty, value);
            _isInternalQuantityChange = false;

            if (updateText)
                UpdateTextFromQuantity();
        }

        private decimal NormalizeMinimum(decimal value)
        {
            return value < Minimum ? Minimum : value;
        }

        private decimal NormalizeMaximum(decimal value)
        {
            decimal effectiveMaximum = Maximum < Minimum ? Minimum : Maximum;
            return value > effectiveMaximum ? effectiveMaximum : value;
        }

        private decimal GetValidStep()
        {
            return Step <= 0 ? 1.00m : Step;
        }

        private static decimal RoundQuantity(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
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

        private void InputQuantitiesExitControl_ValidationError(object sender, ValidationErrorEventArgs e)
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
