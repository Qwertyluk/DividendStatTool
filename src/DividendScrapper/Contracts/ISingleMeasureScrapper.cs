using DividendScrapper.Data;

namespace DividendScrapper.Contracts
{
    internal interface ISingleMeasureScrapper
    {
        Measurement ScrapMeasure();
    }
}
