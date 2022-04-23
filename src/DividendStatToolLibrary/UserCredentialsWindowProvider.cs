using Common.ViewModels;
using Common.Views;
using CommonUI.Models;
using DividendStatToolLibrary.Contracts;

namespace DividendStatToolLibrary
{
    public class UserCredentialsWindowProvider : IUserCredentialsProvider
    {
        public UserCredentials? GetUserCredentials()
        {
            SignInViewModel viewModel = new SignInViewModel();
            SignInWindow window = new SignInWindow()
            {
                DataContext = viewModel
            };

            if (window.ShowDialog() == true)
            {
                return viewModel.UserCredentials;
            };

            return null;
        }
    }
}
