using CommonUI.ViewModels;
using CommonUI.ViewModels.Contracts;
using System;
using System.Windows.Input;

namespace CommonUI.Commands
{
    internal class CancelCommand : ICommand
    {
        private readonly ICancelViewModel viewModel;

        public CancelCommand(ICancelViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.Cancel();
        }
    }
}
