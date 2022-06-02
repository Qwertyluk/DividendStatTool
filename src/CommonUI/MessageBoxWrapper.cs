using CommonUI.Contracts;
using System.Windows;

namespace CommonUI
{
    public class MessageBoxWrapper : IMessageHandler
    {
        public void HandleError(string caption, string warningMessage)
        {
            MessageBox.Show(warningMessage, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void HandleInfo(string caption, string infoMessage)
        {
            MessageBox.Show(infoMessage, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
