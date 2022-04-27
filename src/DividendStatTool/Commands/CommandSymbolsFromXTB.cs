using CommonUI.Contracts;
using CommonUI.Models;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System.Collections.Generic;
using xAPIServices.Contracts;
using xAPIServices.Enums;
using xAPIServices.Exceptions;

namespace DividendStatTool.Commands
{
    internal class CommandSymbolsFromXTB : CommandSymbols
    {
        private readonly IUserCredentialsProvider userCredentialsProvider;
        private readonly IXTBService xtbService;
        private readonly IFilterStringCollection filter;
        private readonly IMessageHandler messageHandler;

        public CommandSymbolsFromXTB(
            IMainWindowViewModel viewModel,
            IUserCredentialsProvider userCredentialsProvider,
            IXTBService xtbService,
            IFilterStringCollection filter,
            IMessageHandler messageHandler) : base(viewModel)
        {
            this.userCredentialsProvider = userCredentialsProvider;
            this.xtbService = xtbService;
            this.filter = filter;
            this.messageHandler = messageHandler;
        }

        public override void Execute(object? parameter)
        {
            UserCredentials? uc = userCredentialsProvider.GetUserCredentials();
            if (uc != null)
            {
                if (TryLogin(uc))
                {
                    IEnumerable<string> symbolsFromXTB = xtbService.GetSymbols(SymbolsGroupName.US);
                    IEnumerable<string> filteredSymbols = filter.Filter(symbolsFromXTB);
                    SetViewModelSymbols(filteredSymbols);
                }
                else
                {
                    messageHandler.HandleError("Login Failed", "Could not connect to XTB server.");
                }
            }
        }

        private bool TryLogin(UserCredentials uc)
        {
            try
            {
                xtbService.Login(uc.UserName, uc.Password);
                return true;
            }
            catch (XTBLoginException)
            {
                return false;
            }
        }
    }
}
