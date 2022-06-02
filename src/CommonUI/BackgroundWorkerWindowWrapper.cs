using CommonUI.Contracts;
using CommonUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUI
{
    public class BackgroundWorkerWindowWrapper : IBackgroundWorkerWindowWrapper
    {
        private readonly IBackgroundWorkerWindow window;

        public BackgroundWorkerWindowWrapper() : this(new BackgroundWorkerWindow()) { }

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
