using Common.Contracts;
using DividendStatTool.Commands;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using FluentAssertions;
using Moq;
using System.ComponentModel;
using Xunit;

namespace DividendStatTool.Tests.Commands
{
    public class CommandSymbolsFromFileTests
    {
        private readonly Mock<IMainWindowViewModel> mockViewModel = new Mock<IMainWindowViewModel>();
        private readonly Mock<IOpenFileDialogService> mockOpenDialog = new Mock<IOpenFileDialogService>();
        private readonly Mock<IFileReader> mockFileReader = new Mock<IFileReader>();
        private readonly Mock<IFilterStringCollection> mockFilter = new Mock<IFilterStringCollection>();

        private CommandSymbolsFromFile GetUUT()
        {
            return new CommandSymbolsFromFile(mockViewModel.Object, mockOpenDialog.Object, mockFileReader.Object, mockFilter.Object);
        }

        [Fact]
        public void Execute_ShouldNotChangeViewModelState_WhenOpenDialogFails()
        {
            // Arrange
            BindingList<string> initialSymbols = new BindingList<string>() { "testSymbol1", "testSymbol2" };
            mockViewModel.Setup(m => m.Symbols).Returns(initialSymbols);
            mockOpenDialog.Setup(m => m.ShowDialog()).Returns(false);
            CommandSymbolsFromFile uut = GetUUT();

            // Act
            uut.Execute(null);

            // Assert
            mockViewModel.Object.Symbols.Should().Equal(initialSymbols);
        }

        [Fact]
        public void Execute_ShouldChangeViewModelState()
        {
            // Arrange
            mockViewModel.SetupGet(m => m.Symbols).Returns(new BindingList<string>());
            mockOpenDialog.Setup(m => m.ShowDialog()).Returns(true);
            string filePath = "testFilePath";
            mockOpenDialog.SetupGet(m => m.FileName).Returns(filePath);
            string[] readSymbols = new string[] { "testRead1", "testRead2" };
            mockFileReader.Setup(m => m.ReadAllLines(filePath)).Returns(readSymbols);
            string[] filteredSymbols = new string[] { "testFiltered1", "testFiltered2" };
            mockFilter.Setup(m => m.Filter(readSymbols)).Returns(filteredSymbols);
            CommandSymbolsFromFile uut = GetUUT();

            // Act
            uut.Execute(null);

            // Assert
            mockViewModel.Object.Symbols.Should().Equal(filteredSymbols);
        }
    }
}
