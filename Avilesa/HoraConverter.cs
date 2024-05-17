using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Avilesa
{
    public class HoraConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeOnly hora)
                return hora.ToString("HH:mm");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (TimeOnly.TryParseExact(value.ToString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly result))
                return result;

            return value;
        }
    }
}
