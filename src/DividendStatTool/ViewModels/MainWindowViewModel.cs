using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.Commands.Factories.Contracts;
using DividendStatTool.ViewModels.Contracts;
using System.ComponentModel;
using System.Windows.Input;

namespace DividendStatTool.ViewModels
{
    internal class MainWindowViewModel : IMainWindowViewModel
    {
        public ICommand ButtonFetchFromFileCommand { get; set; }
        public ICommand ButtonFetchFromXTBCommand { get; set; }

        public BindingList<string> Symbols { get; } = new BindingList<string>();

        public MainWindowViewModel(
            IProviderCommandSymbolsFromFile commandSymbolsFromFileProvider,
            IProviderCommandSymbolsFromXTB commandSymbolsFromXTBProvider)
        {
            ButtonFetchFromFileCommand = commandSymbolsFromFileProvider.GetCommandSymbolsFromFile(this);
            ButtonFetchFromXTBCommand = commandSymbolsFromXTBProvider.GetCommandSymbolsFromXTB(this);
        }
    }
}
