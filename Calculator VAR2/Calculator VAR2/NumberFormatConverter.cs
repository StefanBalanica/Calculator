using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Calculator_VAR2
{
    public class NumberFormatConverter : IValueConverter
    {
        public int CountNonZeroDecimals(double value)
        {
            string strValue = value.ToString("0.####################", CultureInfo.InvariantCulture);

            int decimalPointIndex = strValue.IndexOf('.');

            if (decimalPointIndex == -1)
            {
                return 0;
            }

            string fractionalPart = strValue.Substring(decimalPointIndex + 1);
            int nonZeroCount = fractionalPart.Count(c => c != '0');
            if (nonZeroCount>5)
            {
                nonZeroCount=5;
            }
            return nonZeroCount;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ( value is double  d)
            {

                if (d % 1 != 0)
                {
                    switch (CountNonZeroDecimals(d))
                    {
                        case 1:
                            return string.Format(CultureInfo.CurrentCulture, "{0:N1}", d);
                        case 2:
                            return string.Format(CultureInfo.CurrentCulture, "{0:N2}", d);
                        case 3:
                            return string.Format(CultureInfo.CurrentCulture, "{0:N3}", d);
                        case 4:
                            return string.Format(CultureInfo.CurrentCulture, "{0:N4}", d);
                        case 5:
                            return string.Format(CultureInfo.CurrentCulture, "{0:N5}", d);

                    }
                }
                return string.Format(CultureInfo.CurrentCulture, "{0:N0}", d);

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
