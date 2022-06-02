using Common.Contracts;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Common
{
    public class BackgroundWorkerService : IBackgroundWorkerService
    {
        private BackgroundWorker? backgroundWorker;

        public void Run(DoWorkEventHandler? job, ProgressChangedEventHandler? jobProgressChanged, RunWorkerCompletedEventHandler? jobCompleted)
        {
            InitBackgroundWorker(job, jobProgressChanged, jobCompleted);
            backgroundWorker.RunWorkerAsync();
        }

        public void Cancel()
        {
            backgroundWorker?.CancelAsync();
        }

        [MemberNotNull(nameof(backgroundWorker))]
        private void InitBackgroundWorker(DoWorkEventHandler? job, ProgressChangedEventHandler? jobProgressChanged, RunWorkerCompletedEventHandler? jobCompleted)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += job;
            backgroundWorker.ProgressChanged += jobProgressChanged;
            backgroundWorker.RunWorkerCompleted += jobCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }
    }
}
