using DividendStatTool.ViewModels.Contracts;
using System;
using System.Linq;
using System.Windows.Input;

namespace DividendStatTool.Commands
{
    internal abstract class CommandExecutableWhenSymbolsExist : ICommand
    {
        protected readonly IMainWindowViewModel viewModel;

        public CommandExecutableWhenSymbolsExist(IMainWindowViewModel viewModel)
        {
            viewModel.Symbols.ListChanged += SymbolsListChanged;
            this.viewModel = viewModel;
        }

        #region ICommand

        public virtual event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return viewModel.Symbols.Any();
        }

        public abstract void Execute(object? parameter);

        #endregion

        private void SymbolsListChanged(object? sender, System.ComponentModel.ListChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
