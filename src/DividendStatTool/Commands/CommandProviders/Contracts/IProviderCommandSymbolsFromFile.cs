using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.Factories.Contracts
{
    internal interface IProviderCommandSymbolsFromFile
    {
        ICommand GetCommandSymbolsFromFile(IMainWindowViewModel viewModel);
    }
}
