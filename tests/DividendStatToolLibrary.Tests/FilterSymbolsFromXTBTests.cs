using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace DividendStatToolLibrary.Tests
{
    public class FilterSymbolsFromXTBTests
    {
        [Fact]
        public void Filter_ShouldFilterStringCollectionProperly()
        {
            // Arrange
            IEnumerable<string> stringCollection = new string[] { "TGNA.US_9", "SIRI.US_9", "BMRN.US_9" };
            IEnumerable<string> expectedResult = new string[] { "TGNA", "SIRI", "BMRN" };
            FilterSymbolsFromXTB uut = new FilterSymbolsFromXTB();

            // Act
            IEnumerable<string> result = uut.Filter(stringCollection);

            // Assert
            result.Should().Equal(expectedResult);
        }
    }
}
