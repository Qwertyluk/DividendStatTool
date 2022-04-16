using DividendCalculation.Commands.Factories.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System.Windows.Input;

namespace DividendCalculation.Commands.Factories
{
    internal class FetchSymbolsFromFileCommandFactory : IFetchSymbolsFromFileCommandFactory
    {
        private readonly ISymbolsProvider symbolsProvider;

        public FetchSymbolsFromFileCommandFactory(ISymbolsProvider symbolsProvider)
        {
            this.symbolsProvider = symbolsProvider;
        }

        public ICommand GetFetchFromFileCommand(IMainWindowViewModel mainWindowViewModel)
        {
            return new FetchSymbolsFromFileCommand(mainWindowViewModel, symbolsProvider);
        }
    }
}
