namespace CommonUI.Contracts
{
    public interface IMessageHandler
    {
        public void HandleError(string caption, string warningMessage);
        public void HandleInfo(string caption, string infoMessage);
    }
}
