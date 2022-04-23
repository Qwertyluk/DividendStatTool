using CommonUI.Models;

namespace DividendStatToolLibrary.Contracts
{
    public interface IUserCredentialsProvider
    {
        UserCredentials? GetUserCredentials();
    }
}
