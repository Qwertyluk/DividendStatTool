using System.Collections.Generic;
using System.Data;

namespace CommonUI.ViewModels.Contracts
{
    public interface IDataGridViewModel
    {
        DataTable Data { get; set; }
    }
}
