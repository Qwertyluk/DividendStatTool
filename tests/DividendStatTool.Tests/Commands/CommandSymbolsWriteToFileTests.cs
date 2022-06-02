using CommonUI.Contracts;
using DividendStatTool.Commands;
using DividendStatTool.Infrastructure;
using DividendStatTool.Infrastructure.Contracts;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TestsCommon;
using Xunit;

namespace DividendStatTool.Tests.Commands
{
    public class CommandSymbolsWriteToFileTests
    {
        private readonly Mock<IMainWindowViewModel> mockViewModel = new Mock<IMainWindowViewModel>();
        private readonly Mock<IOpenFileDialogService> mockOpenFileDialog = new Mock<IOpenFileDialogService>();
        private readonly Mock<ISymbolsFileWriter> symbolsWriter = new Mock<ISymbolsFileWriter>();
        private readonly Mock<IMessageHandler> mockMessageHandler = new Mock<IMessageHandler>();

        [Fact]
        public void Execute_ShouldNotCreateFile_WhenOpenFileDialogFails()
        {
            // Arrange
            mockViewModel.SetupGet(m => m.Symbols).Returns(new BindingList<string>());
            mockOpenFileDialog.Setup(m => m.ShowDialog()).Returns(false);
            CommandSymbolsWriteToFile uut
                = new CommandSymbolsWriteToFile(
                    mockViewModel.Object,
                    mockOpenFileDialog.Object,
                    symbolsWriter.Object,
                    mockMessageHandler.Object);

            // Act
            uut.Execute(null);

            // Assert
            symbolsWriter.Verify(m => m.WriteSymbols(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Execute_ShouldCreateCorrectFile_WhenOpenFileDialogSuccess()
        {
            // Arrange
            BindingList<string> symbols = new BindingList<string>()
            {
                "testSymbol1",
                "testSymbol2"
            };
            mockViewModel.SetupGet(m => m.Symbols).Returns(symbols);
            mockOpenFileDialog.Setup(m => m.ShowDialog()).Returns(true);
            string filePath = TestData.GetFullResourceDirectory($"{Guid.NewGuid()}_temp.txt");
            mockOpenFileDialog.Setup(m => m.FileName).Returns(filePath);
            ISymbolsFileWriter symbolsWriter = new SymbolsFileWriter();
            CommandSymbolsWriteToFile uut = new CommandSymbolsWriteToFile(
                mockViewModel.Object,
                mockOpenFileDialog.Object,
                symbolsWriter,
                mockMessageHandler.Object);

            // Act
            uut.Execute(null);

            // Assert
            File.Exists(filePath).Should().BeTrue();
            File.ReadAllLines(filePath).Should().BeEquivalentTo(symbols);
            File.Delete(filePath);
        }

        [Fact]
        public void Execute_ShouldShowMessage_WhenSaveSuccess()
        {
            // Arrange
            mockViewModel.SetupGet(m => m.Symbols).Returns(new BindingList<string>());
            mockOpenFileDialog.Setup(m => m.ShowDialog()).Returns(true);
            CommandSymbolsWriteToFile uut = new CommandSymbolsWriteToFile(
                mockViewModel.Object,
                mockOpenFileDialog.Object,
                symbolsWriter.Object,
                mockMessageHandler.Object);

            // Act
            uut.Execute(null);

            // Assert
            mockMessageHandler.Verify(m => m.HandleInfo(StringLiterals.Success, StringLiterals.SymbolsSavedToFileMessage), Times.Once);
        }
    }
}
