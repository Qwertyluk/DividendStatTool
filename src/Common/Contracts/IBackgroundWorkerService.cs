using System.ComponentModel;

namespace Common.Contracts
{
    public interface IBackgroundWorkerService
    {
        void Cancel();

        void Run(DoWorkEventHandler? job, ProgressChangedEventHandler? jobProgressChanged, RunWorkerCompletedEventHandler? jobCompleted);
    }
}
