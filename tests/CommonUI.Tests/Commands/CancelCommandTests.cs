using CommonUI.Commands;
using CommonUI.ViewModels.Contracts;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommonUI.Tests.Commands
{
    public class CancelCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue()
        {
            // Arrange
            CancelCommand uut = new CancelCommand(null!);

            // Act
            bool result = uut.CanExecute(null);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Execute_ShouldCancelViewModel()
        {
            // Arrange
            Mock<ICancelViewModel> mockViewModel = new Mock<ICancelViewModel>();
            CancelCommand uut = new CancelCommand(mockViewModel.Object);

            // Act
            uut.Execute(null);

            // Assert
            mockViewModel.Verify(m => m.Cancel(), Times.Once);
            // verify dialog result
        }
    }
}
