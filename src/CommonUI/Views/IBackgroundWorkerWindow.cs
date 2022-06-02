using CommonUI.ViewModels;
using CommonUI.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUI.Views
{
    public interface IBackgroundWorkerWindow
    {
        IBackgroundWorkerViewModel ViewModel { get; }
        bool? ShowDialog();
    }
}
