using CommonUI.Contracts;
using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System.Windows.Input;
using xAPIServices.Contracts;

namespace DividendStatTool.Commands.CommandProviders
{
    internal class ProviderCommandSymbolsFromXTB : IProviderCommandSymbolsFromXTB
    {
        private readonly IUserCredentialsProvider userCredentialsProvider;
        private readonly IXTBService xtbService;
        private readonly IFilterStringCollection filter;
        private readonly IMessageHandler messageHandler;

        public ProviderCommandSymbolsFromXTB(
            IUserCredentialsProvider userCredentialsProvider,
            IXTBService xtbService,
            IFilterStringCollection filter,
            IMessageHandler messageHandler)
        {
            this.userCredentialsProvider = userCredentialsProvider;
            this.xtbService = xtbService;
            this.filter = filter;
            this.messageHandler = messageHandler;
        }

        public ICommand GetCommandSymbolsFromXTB(IMainWindowViewModel viewModel)
        {
            return new CommandSymbolsFromXTB(viewModel, userCredentialsProvider, xtbService, filter, messageHandler);
        }
    }
}
