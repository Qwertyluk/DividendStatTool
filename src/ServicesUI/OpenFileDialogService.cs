using DividendCalculation.Services.Contracts;
using Microsoft.Win32;

namespace DividendCalculation.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        private readonly OpenFileDialog openFileDialog = new OpenFileDialog();

        public string FileName { 
            get => openFileDialog.FileName; 
            set => openFileDialog.FileName = value;
        }

        public bool? ShowDialog()
        {
            return openFileDialog.ShowDialog();
        }
    }
}
