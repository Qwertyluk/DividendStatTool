using DividendScrapper.Contracts;

namespace DividendScrapper.Factories.Contracts
{
    public interface IScrapperFactory
    {
        IScrapper GetScrapper(string companySymbol);
    }
}
