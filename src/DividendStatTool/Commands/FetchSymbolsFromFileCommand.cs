using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DividendCalculation.Commands
{
    internal class FetchSymbolsFromFileCommand : ICommand
    {
        private readonly IMainWindowViewModel viewModel;
        private readonly ISymbolsProvider symbolProvider;

        public FetchSymbolsFromFileCommand(
            IMainWindowViewModel windowViewModel,
            ISymbolsProvider symbolProvider)
        {
            this.viewModel = windowViewModel;
            this.symbolProvider = symbolProvider;
        }

        #region ICommand

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            symbolProvider.GetSymbols();

            if (symbolProvider.Succeeded)
            {
                SetViewModelSymbols(symbolProvider.Symbols);
            }
        }

        private void SetViewModelSymbols(IEnumerable<string> symbols)
        {
            viewModel.Symbols.Clear();
            foreach (var symbol in symbols)
            {
                viewModel.Symbols.Add(symbol);
            }
        }

        #endregion
    }
}
