using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.CommandProviders.Contracts
{
    internal interface IMainWindowCommandProvider
    {
        ICommand GetCommand(IMainWindowViewModel viewModel);
    }
}
