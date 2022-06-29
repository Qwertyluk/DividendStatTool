using CommonUI.ViewModels.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommonUI.ViewModels
{
    public class DataGridViewModel : IDataGridViewModel
    {
        public DataTable Data { get; set; } = new DataTable();
    }
}
