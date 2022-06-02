using CommonUI.Contracts;
using DividendScrapper.Factories.Contracts;
using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.CommandProviders
{
    internal class ProviderCommandRunCalculations : IMainWindowCommandProvider
    {
        private readonly IBackgroundWorkerWindowWrapper bwWindowWrapper;
        private readonly IScrapperFactory scrapperFactory;

        public ProviderCommandRunCalculations(IBackgroundWorkerWindowWrapper bwWindowWrapper, IScrapperFactory scrapperFactory)
        {
            this.bwWindowWrapper = bwWindowWrapper;
            this.scrapperFactory = scrapperFactory;
        }

        public ICommand GetCommand(IMainWindowViewModel viewModel)
        {
            return new CommandRunCalculations(viewModel, bwWindowWrapper, scrapperFactory);
        }
    }
}
