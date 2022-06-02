using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUI.Contracts
{
    public interface IBackgroundWorkerWindowWrapper
    {
        object? GetResult(DoWorkEventHandler job);
    }
}
