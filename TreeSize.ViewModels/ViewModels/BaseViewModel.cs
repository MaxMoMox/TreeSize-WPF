using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TreeSize.ViewModels.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnyPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(field, value))
        {
            return false;
        }

        field = value;
        OnyPropertyChanged(propertyName);

        return true;
    }
}