using DividendScrapper.Contracts;
using DividendScrapper.Data;
using DividendScrapper.Enums;
using DividendScrapper.Factories.Contracts;

namespace DividendScrapper
{
    internal class SingleMeasureScrapper : ISingleMeasureScrapper
    {
        private readonly IMeasureTextScrapper textScrapper;
        private readonly Func<string, double> convert;
        private readonly Func<string, bool> validate;
        private readonly IMeasurementFactory measurementFactory;

        public Factor Factor { get; private set; }

        public SingleMeasureScrapper(
            IMeasureTextScrapper textScrapper,
            Func<string, double> convert,
            Func<string, bool> validate,
            Factor factor,
            IMeasurementFactory measurementFactory)
        {
            this.textScrapper = textScrapper;
            this.convert = convert;
            this.validate = validate;
            Factor = factor;
            this.measurementFactory = measurementFactory;
        }

        public Measurement ScrapMeasure()
        {
            string scrappedText = textScrapper.GetScrappedText();

            if (validate(scrappedText))
            {
                double scrappedDPE = convert(scrappedText);
                return measurementFactory.CreateMeasurement(Factor, scrappedDPE);
            }

            return measurementFactory.CreateMeasurement(Factor, 0);
        }
    }
}
