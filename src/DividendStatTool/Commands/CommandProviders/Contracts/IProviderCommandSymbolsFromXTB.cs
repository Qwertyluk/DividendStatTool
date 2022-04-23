using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.CommandProviders.Contracts
{
    internal interface IProviderCommandSymbolsFromXTB
    {
        ICommand GetCommandSymbolsFromXTB(IMainWindowViewModel viewModel);
    }
}
