using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Almacen_Sistema.Resources.Helpers.DataPicker___Control
{
    public static class DatePickerHelper
    {
        public static readonly DependencyProperty DisableManualInputProperty =
            DependencyProperty.RegisterAttached(
                "DisableManualInput",
                typeof(bool),
                typeof(DatePickerHelper),
                new PropertyMetadata(false, OnDisableManualInputChanged));

        public static bool GetDisableManualInput(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableManualInputProperty);
        }

        public static void SetDisableManualInput(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableManualInputProperty, value);
        }

        private static void OnDisableManualInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DatePicker datePicker)
            {
                if ((bool)e.NewValue)
                {
                    datePicker.Loaded += DatePicker_Loaded;
                }
                else
                {
                    datePicker.Loaded -= DatePicker_Loaded;
                }
            }
        }

        private static void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not DatePicker datePicker)
                return;

            DatePickerTextBox textBox = FindTextBox(datePicker);

            if (textBox == null)
                return;

            textBox.IsReadOnly = true;
            textBox.Focusable = false;

            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.PreviewTextInput += TextBox_PreviewTextInput;

            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;

            DataObject.RemovePastingHandler(textBox, OnPaste);
            DataObject.AddPastingHandler(textBox, OnPaste);
        }

        private static DatePickerTextBox FindTextBox(DatePicker datePicker)
        {
            return datePicker.Template.FindName("PART_TextBox", datePicker) as DatePickerTextBox;
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Tab:
                case Key.Enter:
                case Key.Escape:
                    e.Handled = false;
                    break;

                default:
                    e.Handled = true;
                    break;
            }
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}
