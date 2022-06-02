namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static double PercentageToDouble(this string str)
        {
            return Math.Round(double.Parse(str.TrimEnd('%')) / 100, 4);
        }

        public static double NumberWithAbbreviationToDouble(this string str)
        {
            var abbreviation = str.Last();
            long multiplier = 0;
            switch (abbreviation)
            {
                case 'K':
                    multiplier = 1_000;
                    break;
                case 'M':
                    multiplier = 1_000_000;
                    break;
                case 'B':
                    multiplier = 1_000_000_000;
                    break;
                case 't':
                    multiplier = 1_000_000_000_000;
                    break;
            }

            return double.Parse(str.RemoveLastCharacter()) * multiplier;
        }

        private static string RemoveLastCharacter(this string str)
        {
            return str.Remove(str.Length - 1);
        }
    }
}
