using CommonUI.ViewModels;
using CommonUI.ViewModels.Contracts;
using CommonUI.Views;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommonUI.Tests
{
    public class BackgroundWorkerWindowWrapperTests
    {
        private Mock<IBackgroundWorkerWindow> mockWindow;
        private Mock<IBackgroundWorkerViewModel> mockVM;

        public BackgroundWorkerWindowWrapperTests()
        {
            SetupMockVM();
            SetupMockWindow();
        }

        [Fact]
        public void GetResult_ShouldAssingJobToViewModel()
        {
            // Arrange
            BackgroundWorkerWindowWrapper window = new BackgroundWorkerWindowWrapper(mockWindow.Object);

            // Act
            DoWorkEventHandler job = (sender, e) => { };
            window.GetResult(job);

            // Assert
            mockWindow.Object.ViewModel.BackgroundWorkerJob.Should().BeSameAs(job);
        }

        [Fact]
        public void GetResult_ShouldDisplayWindow()
        {
            // Arrange
            BackgroundWorkerWindowWrapper window = new BackgroundWorkerWindowWrapper(mockWindow.Object);

            // Act
            window.GetResult(null!);

            // Assert
            mockWindow.Verify(m => m.ShowDialog(), Times.Once());
        }

        [MemberNotNull(nameof(mockVM))]
        private void SetupMockVM()
        {
            mockVM = new Mock<IBackgroundWorkerViewModel>();
            mockVM.SetupAllProperties();
        }

        [MemberNotNull(nameof(mockWindow))]
        private void SetupMockWindow()
        {
            mockWindow = new Mock<IBackgroundWorkerWindow>();
            mockWindow.Setup(m => m.ViewModel).Returns(mockVM.Object);
        }
    }
}
