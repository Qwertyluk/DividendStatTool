using DividendScrapper.Data;
using DividendScrapper.Enums;

namespace DividendScrapper.Contracts
{
    internal interface ISingleMeasureScrapper
    {
        Factor Factor { get; }
        Measurement ScrapMeasure();
    }
}
