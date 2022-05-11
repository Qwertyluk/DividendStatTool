namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static double PercentageToDouble(this string str)
        {
            return Math.Round(double.Parse(str.TrimEnd('%')) / 100, 4);
        }

        public static double BillionAnnotationToDouble(this string str)
        {
            return double.Parse(str.TrimEnd('B')) * 1_000_000_000;
        }
    }
}
