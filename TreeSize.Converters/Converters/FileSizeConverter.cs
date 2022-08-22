using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TreeSize.Converters.Converters;

public class FileSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        const long gbSize = 1073741824;
        const int mbSize = 1048576;
        const int kbSize = 1024;
        const int numbersAfterComma = 2;

        var size = (long)value;

        return size switch
        {
            >= gbSize => $"{Math.Round((double)size / gbSize, numbersAfterComma)} GB",
            >= mbSize => $"{Math.Round((double)size / mbSize, numbersAfterComma)} MB",
            >= kbSize => $"{Math.Round((double)size / kbSize, numbersAfterComma)} KB",
            _ => $"{value} Byte",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}