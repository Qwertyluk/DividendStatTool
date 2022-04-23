using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace DividendStatToolLibrary.Tests
{
    public class FilterSymbolsFromFileTests
    {
        [Fact]
        public void Filter_ShouldFilterStringCollectionProperly()
        {
            // Arrange
            IEnumerable<string> stringCollection = new string[] { "ABC", string.Empty, "CDE", "ABC" };
            IEnumerable<string> expectedResult = new string[] { "ABC", "CDE" };
            FilterSymbolsFromFile uut = new FilterSymbolsFromFile();

            // Assert
            IEnumerable<string> result = uut.Filter(stringCollection);

            // Act
            result.Should().Equal(expectedResult);
        }
    }
}
