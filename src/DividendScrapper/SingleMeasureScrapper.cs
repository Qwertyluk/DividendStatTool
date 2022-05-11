using DividendScrapper.Contracts;
using DividendScrapper.Data;

namespace DividendScrapper
{
    internal class SingleMeasureScrapper : ISingleMeasureScrapper
    {
        private readonly MeasureTextScrapper textScrapper;
        private readonly Func<string, double> convert;
        private readonly Func<string, bool> validate;
        private readonly string friendlyName;

        public SingleMeasureScrapper(
            MeasureTextScrapper textScrapper,
            Func<string, double> convert,
            Func<string, bool> validate,
            string friendlyName)
        {
            this.textScrapper = textScrapper;
            this.convert = convert;
            this.validate = validate;
            this.friendlyName = friendlyName;
        }

        public Measurement ScrapMeasure()
        {
            string scrappedText = textScrapper.GetScrappedText();

            if (validate(scrappedText))
            {
                double scrappedDPE = convert(scrappedText);
                return new Measurement(friendlyName, scrappedDPE);
            }

            return new Measurement(friendlyName, 0);
        }
    }
}
