using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TreeSize.ViewModels.Commands.BaseCommands;

public interface IAsyncCommand : ICommand
{
    IEnumerable<Task> RunningTasks { get; }
    bool CanExecute();
    Task ExecuteAsync();
}