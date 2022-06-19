using DividendScrapper.Data;
using DividendScrapper.Enums;

namespace DividendScrapper.Factories.Contracts
{
    internal interface IMeasurementFactory
    {
        Measurement CreateMeasurement(Factor factor, double value);
    }
}
