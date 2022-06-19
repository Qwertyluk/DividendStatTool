using DividendScrapper.Data;
using DividendScrapper.Enums;

namespace DividendScrapper.Contracts
{
    public interface IScrapper
    {
        Measurement[] Scrap();
        Measurement Scrap(Factor factor);
    }
}
