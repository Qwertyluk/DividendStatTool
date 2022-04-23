namespace CommonUI.Contracts
{
    public interface IMessageHandler
    {
        public void HandleError(string warningType, string warningMessage);
    }
}
