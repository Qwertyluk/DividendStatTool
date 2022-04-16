using DividendCalculation.Services.Contracts;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using TestsCommon;
using Xunit;

namespace DividendStatToolLibrary.Tests
{
    public class SymbolsProviderFromFileTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void GetSymbols_ShouldReadSymbolsFromFile()
        {
            // Arrange
            string filePath = TestData.CombineWithResourceDirectory("symbols.txt");
            Mock<IOpenFileDialogService> mockOpenFileDialog = new Mock<IOpenFileDialogService>();
            mockOpenFileDialog.Setup(m => m.ShowDialog()).Returns(true);
            mockOpenFileDialog.Setup(m => m.FileName).Returns(filePath);
            SymbolsProviderFromFile uut = new SymbolsProviderFromFile(mockOpenFileDialog.Object);
            string[] expectedResults = new string[] { "ABC", "DEF", "GHI" };

            // Act
            uut.GetSymbols();

            // Assert
            using (new AssertionScope())
            {
                uut.Succeeded.Should().BeTrue();
                uut.Symbols.Count.Should().Be(3);
                uut.Symbols.Should().Equal(expectedResults);
            }
        }

        [Fact]
        public void GetSymbols_ShouldNotFetchSymbols_WhenUserDidntSelectFile()
        {
            // Arrange
            Mock<IOpenFileDialogService> mockOpenFileDialog = new Mock<IOpenFileDialogService>();
            mockOpenFileDialog.Setup(m => m.ShowDialog()).Returns(false);
            SymbolsProviderFromFile uut = new SymbolsProviderFromFile(mockOpenFileDialog.Object);

            // Act
            uut.GetSymbols();

            // Assert
            using (new AssertionScope())
            {
                uut.Succeeded.Should().BeFalse();
                uut.Symbols.Should().BeEmpty();
            }
        }
    }
}
