using Common.Contracts;
using CommonUI.Commands;
using CommonUI.Contracts;
using CommonUI.ViewModels.Contracts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CommonUI.ViewModels
{
    public class BackgroundWorkerViewModel : IBackgroundWorkerViewModel, ICancelViewModel, INotifyPropertyChanged
    {
        public ICommand CancelCommand { get; }
        public DoWorkEventHandler? BackgroundWorkerJob { get; set; }
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }
        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                dialogResult = value;
                OnPropertyChanged();
            }
        }
        public object? BackgroundWorkerResult { get; private set; }

        private int progress;
        private bool? dialogResult;
        private readonly IBackgroundWorkerService worker;
        private readonly IMessageHandler messageHandler;

        internal BackgroundWorkerViewModel(IBackgroundWorkerService worker, IMessageHandler messageHandler)
        {
            CancelCommand = new CancelCommand(this);
            this.worker = worker;
            this.messageHandler = messageHandler;
        }

        public void StartWork()
        {
            BackgroundWorkerResult = null;
            worker.Run(BackgroundWorkerJob, BackgroundWorker_ProgressChanged, BackgroundWorker_RunWorkerCompleted);
        }

        public void Cancel()
        {
            worker.Cancel();
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Progress = 0;
            messageHandler.HandleInfo("Information.", e.Cancelled ? "Process canceled." : "Process completed successfully.");

            if (e.Error == null && !e.Cancelled)
            {
                BackgroundWorkerResult = e.Result;
            }

            DialogResult = !e.Cancelled;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
