namespace TreeSize.Models.Models;

public class Storage : BaseFile
{
    private long _freeSpace;
    private long _totalCapacity;

    public long FreeSpace
    {
        get => _freeSpace;
        set
        {
            if (value == _freeSpace)
            {
                return;
            }

            _freeSpace = value;
            OnPropertyChanged(nameof(FreeSpace));
        }
    }

    public long TotalCapacity
    {
        get => _totalCapacity;
        set
        {
            if (value == _totalCapacity)
            {
                return;
            }

            _totalCapacity = value;
            OnPropertyChanged(nameof(TotalCapacity));
        }
    }
}
