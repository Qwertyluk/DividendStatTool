using CommonUI.Contracts;
using DividendScrapper.Contracts;
using DividendScrapper.Factories.Contracts;
using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.CommandProviders
{
    internal class ProviderCommandRunCalculations : IMainWindowCommandProvider
    {
        private readonly IBackgroundWorkerWindowWrapper bwWindowWrapper;
        private readonly ISymbolsScrapper symbolsScrapper;
        private readonly ISymbolMeasurementsFilter symbolsFilter;
        private readonly ISymbolsRanking symbolsRanking;

        public ProviderCommandRunCalculations(
            IBackgroundWorkerWindowWrapper bwWindowWrapper,
            ISymbolsScrapper symbolsScrapper,
            ISymbolMeasurementsFilter symbolsFilter,
            ISymbolsRanking symbolsRanking)
        {
            this.bwWindowWrapper = bwWindowWrapper;
            this.symbolsScrapper = symbolsScrapper;
            this.symbolsFilter = symbolsFilter;
            this.symbolsRanking = symbolsRanking;
        }

        public ICommand GetCommand(IMainWindowViewModel viewModel)
        {
            return new CommandRunCalculations(viewModel, bwWindowWrapper, symbolsScrapper, symbolsFilter, symbolsRanking);
        }
    }
}
