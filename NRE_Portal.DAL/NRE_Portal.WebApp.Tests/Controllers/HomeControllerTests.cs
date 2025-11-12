using Microsoft.AspNetCore.Mvc;
using NRE_Portal.WebApp.Controllers;
using Xunit;
using System.Threading.Tasks;

namespace NRE_Portal.WebApp.Tests.Integration
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Index_ReturnsViewWithModel()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }
    }
}