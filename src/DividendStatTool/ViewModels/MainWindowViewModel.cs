using DividendCalculation.Commands.Factories.Contracts;
using DividendStatTool.ViewModels.Contracts;
using System.ComponentModel;
using System.Windows.Input;

namespace DividendCalculation.ViewModels
{
    internal class MainWindowViewModel : IMainWindowViewModel
    {
        public ICommand ButtonFetchFromFileCommand { get; }

        public BindingList<string> Symbols { get; } = new BindingList<string>();

        public MainWindowViewModel(IFetchSymbolsFromFileCommandFactory populateSymbolsCommandFactory)
        {
            ButtonFetchFromFileCommand = populateSymbolsCommandFactory.GetFetchFromFileCommand(this);
        }
    }
}
