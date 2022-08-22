using System.Collections.ObjectModel;
using TreeSize.Models.Models;

namespace TreeSize.Services.Interfaces;

public interface IFileService
{
    Task<ObservableCollection<Storage>> GetDrivesList();
    Task<BaseFile> GetTree(string path, ObservableCollection<BaseFile> files);
}