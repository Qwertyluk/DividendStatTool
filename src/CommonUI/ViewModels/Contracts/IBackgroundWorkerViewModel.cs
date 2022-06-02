using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUI.ViewModels.Contracts
{
    public interface IBackgroundWorkerViewModel
    {
        object? BackgroundWorkerResult { get; }

        DoWorkEventHandler? BackgroundWorkerJob { get; set; }

        void StartWork();
    }
}
