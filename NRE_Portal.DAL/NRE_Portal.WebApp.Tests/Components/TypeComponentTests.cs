using Microsoft.AspNetCore.Mvc;
using NRE_Portal.WebApp.Controllers;
using Xunit;
using System.Threading.Tasks;

namespace NRE_Portal.WebApp.Tests.Components
{
    public class TypeComponentTests
    {
        #region GetComponent Type Section Tests

        [Fact]
        public async Task GetComponent_WithTypeSection_ReturnsPartialViewResult()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("type");

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.NotNull(partialViewResult);
        }

        [Fact]
        public async Task GetComponent_WithTypeSection_ReturnsCorrectViewName()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("type");

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Components/_TypeComponent", partialViewResult.ViewName);
        }

        [Fact]
        public async Task GetComponent_WithTypeSection_ReturnsViewWithModel()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("type");

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.NotNull(partialViewResult.ViewData);
        }

        #endregion
    }
}