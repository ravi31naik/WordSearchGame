using System;
using System.Collections.Generic;

namespace WordSearchGame.Core.Services;

public class ColorSelector
{
    // Define a palette of visually distinct pastel colors
    private static readonly string[] Palette = 
    {
        "#ffadad", // Light Red
        "#ffd6a5", // Light Orange
        "#fdffb6", // Light Yellow
        "#caffbf", // Light Green
        "#9bf6ff", // Light Cyan
        "#a0c4ff", // Light Blue
        "#bdb2ff", // Light Purple
        "#ffc6ff", // Light Pink
        "#e2f0cb", // Pale Lime
        "#b5ead7", // Mint
        "#ff9aa2"  // Salmon
    };

    private int _currentIndex = 0;
    private readonly Random _random = new();
    private List<string> _shuffledPalette = new();

    public ColorSelector()
    {
        ShufflePalette();
    }

    public string GetNextColor()
    {
        if (_currentIndex >= _shuffledPalette.Count)
        {
            ShufflePalette();
            // Reset index since we refilled the deck
            _currentIndex = 0; 
        }

        return _shuffledPalette[_currentIndex++];
    }

    public void Reset()
    {
        _shuffledPalette.Clear();
        ShufflePalette();
        _currentIndex = 0;
    }

    private void ShufflePalette()
    {
        string? lastColor = _shuffledPalette.Count > 0 ? _shuffledPalette[^1] : null;

        _shuffledPalette = new List<string>(Palette);
        int n = _shuffledPalette.Count;
        while (n > 1)
        {
            n--;
            int k = _random.Next(n + 1);
            string value = _shuffledPalette[k];
            _shuffledPalette[k] = _shuffledPalette[n];
            _shuffledPalette[n] = value;
        }

        // Avoid same adjacent color when refilling the deck
        if (lastColor != null && _shuffledPalette[0] == lastColor)
        {
            // Swap first and second
            if (_shuffledPalette.Count > 1)
            {
                var temp = _shuffledPalette[0];
                _shuffledPalette[0] = _shuffledPalette[1];
                _shuffledPalette[1] = temp;
            }
        }
    }
}
