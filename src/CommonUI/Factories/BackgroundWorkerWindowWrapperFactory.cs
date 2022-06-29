using CommonUI.Contracts;
using CommonUI.Factories.Contracts;
using CommonUI.Views;

namespace CommonUI.Factories
{
    public class BackgroundWorkerWindowWrapperFactory : IBackgroundWorkerWindowWrapperFactory
    {
        public IBackgroundWorkerWindowWrapper GetWindow()
        {
            return new BackgroundWorkerWindowWrapper(new BackgroundWorkerWindow());
        }
    }
}
