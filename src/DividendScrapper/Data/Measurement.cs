using DividendScrapper.Enums;

namespace DividendScrapper.Data
{
    public class Measurement : IComparable<Measurement>
    {
        private readonly Func<double, bool> validate;
        private readonly Func<Measurement, Measurement?, int> compare;

        public Factor Factor { get; }
        public double Value { get; }
        public bool IsValid => validate(Value);

        internal Measurement(
            Factor factor,
            double value,
            Func<double, bool> validate,
            Func<Measurement, Measurement?, int> compare)
        {
            Factor = factor;
            Value = value;
            this.validate = validate;
            this.compare = compare;
        }

        public int CompareTo(Measurement? other)
        {
            return compare(this, other);
        }

        public override string ToString()
        {
            return $"{Factor} {Value}";
        }
    }
}
