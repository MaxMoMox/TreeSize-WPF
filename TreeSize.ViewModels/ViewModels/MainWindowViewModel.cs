using System.Collections.ObjectModel;
using TreeSize.Models.Models;
using TreeSize.Services.Implementations;
using TreeSize.ViewModels.Commands;

namespace TreeSize.ViewModels.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private string _title = "TreeSize(Task 10)";
    private string _status = "Ready to work!";
    private Storage _selectedDrive;
    private ObservableCollection<Storage> _drives;
    private ObservableCollection<BaseFile> _files;
    private BaseFile _selectedFile;

    public GetTreeCommand TreeCommand { get; set; }
    public SelectFileCommand SelectFileCommand { get; set; }

    public MainWindowViewModel()
    {
        var fileService = new FileService(); 
        var allDrivesCommand = new GetDrivesCommand(fileService, this);
        _ = allDrivesCommand.ExecuteAsync();
        TreeCommand = new GetTreeCommand(fileService, this);
        SelectFileCommand = new SelectFileCommand(this);
    }

    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }

    public string Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    public Storage SelectedDrive
    {
        get => _selectedDrive;
        set => Set(ref _selectedDrive, value);
    }

    public ObservableCollection<Storage> Drives
    {
        get => _drives;
        set => Set(ref _drives, value);
    }

    public ObservableCollection<BaseFile> Files
    {
        get => _files;
        set => Set(ref _files, value);
    }

    public BaseFile SelectedFile
    {
        get => _selectedFile;
        set => Set(ref _selectedFile, value);
    }
}