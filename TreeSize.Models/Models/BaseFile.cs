using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TreeSize.Models.Enums;

namespace TreeSize.Models.Models;

public class BaseFile : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private long _size;
    private ObservableCollection<BaseFile> _children = new();
    private string _path = string.Empty;
    private FileType _fileType;

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name)
            {
                return;
            }

            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public long Size
    {
        get => _size;
        set
        {
            if (value == _size)
            {
                return;
            }

            _size = value;
            OnPropertyChanged(nameof(Size));
        }
    }

    public ObservableCollection<BaseFile> Children
    {
        get => _children;
        set
        {
            if (value == _children)
            {
                return;
            }

            _children = value;
            OnPropertyChanged(nameof(Children));
        }
    }

    public string Path
    {
        get => _path;
        set
        {
            if (value == _path)
            {
                return;
            }

            _path = value;
            OnPropertyChanged(nameof(Path));
        }
    }

    public FileType FileType
    {
        get => _fileType;
        set
        {
            if (value == _fileType)
            {
                return;
            }

            _fileType = value;
            OnPropertyChanged(nameof(FileType));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}