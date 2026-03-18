using System.Collections.Generic;
using System.Linq;
using WordSearchGame.Core.Services;
using Xunit;

namespace WordSearchGame.Tests
{
    public class GameGeneratorTests
    {
        private readonly GameGenerator _generator;

        public GameGeneratorTests()
        {
            _generator = new GameGenerator();
        }

        [Fact]
        public void GenerateLevel_WithDuplicateWords_PlacesMultipleInstances()
        {
            // Arrange
            var wordList = new List<string> { "CAT", "CAT", "CAT" };
            int gridSize = 10;

            // Act
            var result = _generator.GenerateLevel(gridSize, gridSize, wordList);

            // Assert
            // It might fail to place all 3 depending on randomness and space, but in a 10x10 it should easily place 3 CATs
            Assert.True(result.Words.Count > 1, "Should have placed multiple instances of the duplicate word.");
            Assert.All(result.Words, w => Assert.Equal("CAT", w.Text));
            
            // Ensure they are distinct instances with potentially different coordinates
            var uniqueStarts = result.Words.Select(w => w.Start).Distinct().Count();
            Assert.Equal(result.Words.Count, uniqueStarts);
        }

        [Fact]
        public void GenerateLevel_WithUniqueWords_PlacesOnlyThoseWords()
        {
            // Arrange (simulating a predefined category)
            var wordList = new List<string> { "LION", "TIGER", "BEAR" };
            int gridSize = 10;

            // Act
            var result = _generator.GenerateLevel(gridSize, gridSize, wordList);

            // Assert
            Assert.Equal(3, result.Words.Count);
            Assert.Contains(result.Words, w => w.Text == "LION");
            Assert.Contains(result.Words, w => w.Text == "TIGER");
            Assert.Contains(result.Words, w => w.Text == "BEAR");
            
            // Ensure no duplicates were accidentally created
            var wordTexts = result.Words.Select(w => w.Text).ToList();
            Assert.Equal(wordTexts.Distinct().Count(), wordTexts.Count);
        }
    }
}
