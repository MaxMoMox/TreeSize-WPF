using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TreeSize.Models.Enums;
using TreeSize.Models.Models;

namespace TreeSize.Converters.Converters;

public class FileTypeIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BaseFile file)
        {
            switch (file.FileType)
            {
                case FileType.File:
                    return "/TreeSize.Resources;component/Icons/FileImage.png";
                case FileType.Folder:
                    return "/TreeSize.Resources;component/Icons/FolderImage.png";
                case FileType.Drive:
                    return "/TreeSize.Resources;component/Icons/HardDriveImage.png";
            }
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}