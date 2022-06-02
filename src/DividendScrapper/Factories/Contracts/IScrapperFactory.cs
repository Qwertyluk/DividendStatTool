using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DividendScrapper.Factories.Contracts
{
    public interface IScrapperFactory
    {
        Scrapper GetScrapper(string companySymbol);
    }
}
