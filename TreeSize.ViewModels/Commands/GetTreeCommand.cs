using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TreeSize.Models.Models;
using TreeSize.Services.Implementations;
using TreeSize.ViewModels.Commands.BaseCommands;
using TreeSize.ViewModels.ViewModels;

namespace TreeSize.ViewModels.Commands;

public class GetTreeCommand : AsyncCommand
{
    private readonly FileService _fileService;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public GetTreeCommand(FileService fileService, MainWindowViewModel mainWindowViewModel)
    {
        _fileService = fileService;
        _mainWindowViewModel = mainWindowViewModel;
    }

    public override bool CanExecute()
    {
        return true;
    }

    public override async Task ExecuteAsync()
    {
        try
        {
            _mainWindowViewModel.Status = "Processing...";

            _mainWindowViewModel.Files = new ObservableCollection<BaseFile>();

            BindingOperations.EnableCollectionSynchronization(_mainWindowViewModel.Files, new object());

            await Task.Run(() => _fileService.GetTree(_mainWindowViewModel.SelectedDrive.Name, _mainWindowViewModel.Files));

            _mainWindowViewModel.Files =
                new ObservableCollection<BaseFile>(_mainWindowViewModel.Files.OrderBy(f => f.FileType)
                    .ThenByDescending(f => f.Size));

            _mainWindowViewModel.Status = "Success! Ready to work.";
        }
        catch (Exception e)
        {
            _mainWindowViewModel.Status = "Error!";

            MessageBox.Show("Something went wrong!\n" +
                            "The drive files loading error.\n" +
                            $"{e.Message}");
        }
    }
}