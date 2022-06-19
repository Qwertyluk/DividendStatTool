using DividendScrapper.Data;
using DividendScrapper.Enums;
using DividendScrapper.Factories.Contracts;

namespace DividendScrapper.Factories
{
    internal class MeasurementFactory : IMeasurementFactory
    {
        private readonly Func<double, bool> validate;
        private readonly Func<Measurement, Measurement?, int> compare;

        public MeasurementFactory(Func<double, bool> validate, Func<Measurement, Measurement?, int> compare)
        {
            this.validate = validate;
            this.compare = compare;
        }

        public Measurement CreateMeasurement(Factor factor, double value)
        {
            return new Measurement(factor, value, validate, compare);
        }
    }
}
