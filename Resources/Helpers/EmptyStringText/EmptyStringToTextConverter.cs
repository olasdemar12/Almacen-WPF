using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Almacen_Sistema.Resources.Helpers.EmptyStringText
{
    public class EmptyStringToTextConverter : IValueConverter
    {
        public string DefaultText { get; set; } = "Sin Notas";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fallbackText = parameter as string ?? DefaultText;

            if (value == null)
                return fallbackText;

            string text = value.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(text))
                return fallbackText;

            return text.Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
