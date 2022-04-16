using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendCalculation.Commands.Factories.Contracts
{
    internal interface IFetchSymbolsFromFileCommandFactory
    {
        ICommand GetFetchFromFileCommand(IMainWindowViewModel mainWindowViewModel);
    }
}
