using System;
using System.Threading.Tasks;
using System.Windows;
using TreeSize.Models.Enums;
using TreeSize.Models.Models;
using TreeSize.ViewModels.Commands.BaseCommands;
using TreeSize.ViewModels.ViewModels;

namespace TreeSize.ViewModels.Commands;

public class SelectFileCommand : BaseCommand
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    public SelectFileCommand(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
    }

    public override Task ExecuteAsync(object parameter)
    {
        try
        {
            _mainWindowViewModel.Status = "Processing...";

            if (parameter is BaseFile {FileType: FileType.Folder} file)
            {
                _mainWindowViewModel.SelectedFile = file;

                _mainWindowViewModel.Status = "Success! Ready to work.";
            }
            else
            {
                _mainWindowViewModel.Status = "Please, select the folder to continue.";
            }

        }
        catch (Exception e)
        {
            _mainWindowViewModel.Status = "Error!";

            MessageBox.Show("Something went wrong!\n" +
                            "The file selection failed\n" +
                            $"{e.Message}");
        }

        return Task.CompletedTask;
    }
}