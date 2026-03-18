using Bunit;
using WordSearchGame.UI.Components;
using Xunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace WordSearchGame.Tests
{
    public class MainMenuTests : BunitContext
    {
        [Fact]
        public void MainMenu_UsesMinHeight_ToPreventOutOfBounds()
        {
            // Arrange & Act
            var cut = Render<MainMenu>();

            // Assert
            // The style tag should contain min-height: 100vh and justify-content: flex-start to prevent the top elements like the title from going out of bounds
            var styleElement = cut.Find("style");
            Assert.Contains("min-height: 100vh", styleElement.InnerHtml);
            Assert.Contains("justify-content: flex-start", styleElement.InnerHtml);
            
            // Also verify the title exists
            var title = cut.Find("h1");
            Assert.Equal("Word Search", title.TextContent);
        }
    }
}
