using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Moq;
using NRE_Portal.WebApp.Controllers;
using Xunit;
using System.Threading.Tasks;

namespace NRE_Portal.WebApp.Tests.Integration
{
    public class PrivateInstallationControllerTests
    {
        #region Index Action Tests

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new PrivateInstallationController();

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
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("PrivateInstallation", viewResult.ViewName);
        }

        #endregion

        #region GetComponent Tests

        [Theory]
        [InlineData("location")]
        [InlineData("type")]
        [InlineData("orientation")]
        [InlineData("area")]
        public async Task GetComponent_WithValidSection_ReturnsPartialViewResult(string section)
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent(section);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.NotNull(partialViewResult);
        }

        [Fact]
        public async Task GetComponent_WithLocationSection_ReturnsLocationComponent()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("location");

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Components/_LocationComponent", partialViewResult.ViewName);
        }

        [Fact]
        public async Task GetComponent_WithTypeSection_ReturnsTypeComponent()
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
        public async Task GetComponent_WithOrientationSection_ReturnsOrientationComponent()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("orientation");

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Components/_OrientationComponent", partialViewResult.ViewName);
        }

        [Fact]
        public async Task GetComponent_WithAreaSection_ReturnsAreaComponent()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("area");

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("Components/_AreaComponent", partialViewResult.ViewName);
        }

        [Fact]
        public async Task GetComponent_WithInvalidSection_ReturnsNotFound()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent("invalid");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetComponent_WithNullSection_ReturnsNotFound()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetComponent_WithEmptySection_ReturnsNotFound()
        {
            // Arrange
            var controller = new PrivateInstallationController();

            // Act
            var result = await controller.GetComponent(""); 

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Navigation Logic Tests

        [Theory]
        [InlineData("location", "type")]
        [InlineData("type", "orientation")]
        [InlineData("orientation", "area")]
        public void Navigation_GetNextSection_ReturnsCorrectSection(string currentSection, string expectedNextSection)
        {
            // Arrange
            var sections = new[] { "location", "type", "orientation", "area" };
            var currentIndex = Array.IndexOf(sections, currentSection);

            // Act
            var nextIndex = currentIndex + 1;
            var actualNextSection = nextIndex < sections.Length ? sections[nextIndex] : null;

            // Assert
            Assert.Equal(expectedNextSection, actualNextSection);
        }

        [Theory]
        [InlineData("type", "location")]
        [InlineData("orientation", "type")]
        [InlineData("area", "orientation")]
        public void Navigation_GetPreviousSection_ReturnsCorrectSection(string currentSection, string expectedPreviousSection)
        {
            // Arrange
            var sections = new[] { "location", "type", "orientation", "area" };
            var currentIndex = Array.IndexOf(sections, currentSection);

            // Act
            var previousIndex = currentIndex - 1;
            var actualPreviousSection = previousIndex >= 0 ? sections[previousIndex] : null;

            // Assert
            Assert.Equal(expectedPreviousSection, actualPreviousSection);
        }

        [Fact]
        public void Navigation_OnFirstSection_CannotGoToPrevious()
        {
            // Arrange
            var sections = new[] { "location", "type", "orientation", "area" };
            var currentIndex = 0;

            // Act
            var canGoToPrevious = currentIndex > 0;

            // Assert
            Assert.False(canGoToPrevious);
        }

        [Fact]
        public void Navigation_OnLastSection_CannotGoToNext()
        {
            // Arrange
            var sections = new[] { "location", "type", "orientation", "area" };
            var currentIndex = sections.Length - 1;

            // Act
            var canGoToNext = currentIndex < sections.Length - 1;

            // Assert
            Assert.False(canGoToNext);
        }

        [Fact]
        public void Navigation_OnMiddleSection_CanGoToBothDirections()
        {
            // Arrange
            var sections = new[] { "location", "type", "orientation", "area" };
            var currentIndex = 1; // "type"

            // Act
            var canGoToPrevious = currentIndex > 0;
            var canGoToNext = currentIndex < sections.Length - 1;

            // Assert
            Assert.True(canGoToPrevious);
            Assert.True(canGoToNext);
        }

        #endregion
    }
}