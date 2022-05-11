namespace DividendScrapper.Data
{
    public class Measurement
    {
        public string Name { get; }
        public double Value { get; }

        public Measurement(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
