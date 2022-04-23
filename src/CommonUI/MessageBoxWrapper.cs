using CommonUI.Contracts;
using System.Windows;

namespace CommonUI
{
    public class MessageBoxWrapper : IMessageHandler
    {
        public void HandleError(string warningType, string warningMessage)
        {
            MessageBox.Show(warningMessage, warningType, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
