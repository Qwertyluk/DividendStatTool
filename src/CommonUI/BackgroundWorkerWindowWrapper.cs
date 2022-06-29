using CommonUI.Contracts;
using CommonUI.Views;
using System.ComponentModel;

namespace CommonUI
{
    public class BackgroundWorkerWindowWrapper : IBackgroundWorkerWindowWrapper
    {
        private readonly IBackgroundWorkerWindow window;

        internal BackgroundWorkerWindowWrapper(IBackgroundWorkerWindow window)
        {
            this.window = window;
        }

        public object? GetResult(DoWorkEventHandler job)
        {
            window.ViewModel.BackgroundWorkerJob = job;
            window.ShowDialog();

            return window.ViewModel.BackgroundWorkerResult;
        }
    }
}
