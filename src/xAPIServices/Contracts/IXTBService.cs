namespace xAPIServices.Contracts
{
    public interface IXTBService
    {
        void Login(string userID, string password);
        IEnumerable<string> GetSymbols(string groupName);
    }
}
