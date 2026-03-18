using System.Collections.Generic;
using WordSearchGame.Core.Services;
using Xunit;

namespace WordSearchGame.Tests
{
    public class ColorSelectorTests
    {
        [Fact]
        public void GetNextColor_ReturnsDistinctColors_UntilPaletteExhausted()
        {
            // Arrange
            var selector = new ColorSelector();
            var seenColors = new HashSet<string>();
            int paletteSize = 11; // Based on the number of colors in ColorSelector.Palette

            // Act
            for (int i = 0; i < paletteSize; i++)
            {
                var color = selector.GetNextColor();
                seenColors.Add(color);
            }

            // Assert
            // Since it shuffles internally but cycles through the whole palette before repeating,
            // we should get exactly 'paletteSize' distinct colors in the first 'paletteSize' calls.
            Assert.Equal(paletteSize, seenColors.Count);
        }

        [Fact]
        public void GetNextColor_AvoidsImmediateConsecutiveDuplicates_WhenShuffling()
        {
            // Arrange
            var selector = new ColorSelector();
            int paletteSize = 11;
            
            // To test consecutive duplicates on reshuffle, we'll draw colors across 
            // the boundary of a reshuffle multiple times to ensure we never get a duplicate.
            
            string previousColor = string.Empty;

            // Act & Assert
            for (int i = 0; i < paletteSize * 5; i++) // Run through 5 full shuffles
            {
                var currentColor = selector.GetNextColor();
                
                if (i > 0)
                {
                    Assert.NotEqual(previousColor, currentColor);
                }
                
                previousColor = currentColor;
            }
        }
    }
}
