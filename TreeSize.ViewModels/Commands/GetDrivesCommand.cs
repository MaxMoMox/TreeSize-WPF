using System;
using System.Threading.Tasks;
using System.Windows;
using TreeSize.Services.Implementations;
using TreeSize.ViewModels.Commands.BaseCommands;
using TreeSize.ViewModels.ViewModels;

namespace TreeSize.ViewModels.Commands;

public class GetDrivesCommand : AsyncCommand
{
    private readonly FileService _fileService;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public GetDrivesCommand(FileService fileService, MainWindowViewModel mainWindowViewModel)
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

            _mainWindowViewModel.Drives = await _fileService.GetDrivesList();

            _mainWindowViewModel.Status = "Double click on the drive.";
        }
        catch (Exception e)
        {
            MessageBox.Show("Something went wrong!\n" +
                            "The Drive list loading error\n" +
                            $"{e.Message}");

            Application.Current.Shutdown();
        }
    }
}