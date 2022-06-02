using DividendStatTool.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DividendStatTool.Commands
{
    internal abstract class CommandPopulateSymbols : ICommand
    {
        private readonly IMainWindowViewModel viewModel;

        public CommandPopulateSymbols(IMainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #region ICommand

        public virtual event EventHandler? CanExecuteChanged { add { } remove { } }

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public virtual void Execute(object? parameter)
        {
            SetViewModelSymbols(Enumerable.Empty<string>());
        }

        protected void SetViewModelSymbols(IEnumerable<string> symbols)
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
