using CommonUI.Contracts;

namespace CommonUI.Factories.Contracts
{
    public interface IBackgroundWorkerWindowWrapperFactory
    {
        IBackgroundWorkerWindowWrapper GetWindow();
    }
}
