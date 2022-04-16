using DividendCalculation.Commands;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace DividendStatTool.Tests.Commands
{
    public class FetchSymbolsFromFileCommandTests
    {
        [Fact]
        public void Execute_ShouldPopulateSymbols_WhenProviderSucceeded()
        {
            // Arrange
            Mock<ISymbolsProvider> mockSymbolsProvider = new Mock<ISymbolsProvider>();
            mockSymbolsProvider.Setup(m => m.Succeeded).Returns(true);
            List<string> expectedSymbols = new List<string>() { "testSymbol1", "testSymbol2", "testSymbol3" };
            mockSymbolsProvider.Setup(m => m.Symbols).Returns(expectedSymbols);
            Mock<IMainWindowViewModel> mockViewModel = new Mock<IMainWindowViewModel>();
            mockViewModel.SetupGet(m => m.Symbols).Returns(new BindingList<string>());
            FetchSymbolsFromFileCommand uut = new FetchSymbolsFromFileCommand(mockViewModel.Object, mockSymbolsProvider.Object);

            // Act
            uut.Execute(null);

            // Assert
            using (new AssertionScope())
            {
                mockViewModel.Object.Symbols.Should().Equal(expectedSymbols);
            }
        }

        [Fact]
        public void Execute_ShouldNotPopulateSymbols_WhenProviderNotSucceeded()
        {
            // Arrange
            Mock<ISymbolsProvider> mockSymbolsProvider = new Mock<ISymbolsProvider>();
            mockSymbolsProvider.Setup(m => m.Succeeded).Returns(false);
            Mock<IMainWindowViewModel> mockViewModel = new Mock<IMainWindowViewModel>();
            BindingList<string> expectedSymbols = new BindingList<string>() { "testSymbol1", "testSymbol2", "testSymbol3" };
            mockViewModel.SetupGet(m => m.Symbols).Returns(expectedSymbols);
            FetchSymbolsFromFileCommand uut = new FetchSymbolsFromFileCommand(mockViewModel.Object, mockSymbolsProvider.Object);

            // Act
            uut.Execute(null);

            // Assert
            // Assert
            using (new AssertionScope())
            {
                mockViewModel.Object.Symbols.Should().Equal(expectedSymbols);
            }
        }
    }
}
