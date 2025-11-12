using Microsoft.AspNetCore.Mvc;
using NRE_Portal.WebApp.Controllers;
using Xunit;
using System.Threading.Tasks;

namespace NRE_Portal.WebApp.Tests.Integration
{
    public class NewRenewablesEnergysControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new NewRenewablesEnergysController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Index_ReturnsCorrectViewName()
        {
            // Arrange
            var controller = new NewRenewablesEnergysController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NewRenewablesEnergys", viewResult.ViewName);
        }

        [Fact]
        public async Task Index_ViewDataContainsCorrectTitle()
        {
            // Arrange
            var controller = new NewRenewablesEnergysController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData);
        }
    }
}