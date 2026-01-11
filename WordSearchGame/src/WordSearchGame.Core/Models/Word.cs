using WordSearchGame.Core.Enums;

namespace WordSearchGame.Core.Models;

public class Word
{
    public string Text { get; set; } = string.Empty;
    public Coordinates Start { get; set; } = new(0, 0);
    public Coordinates End { get; set; } = new(0, 0);
    public Direction Direction { get; set; }
    public bool IsFound { get; set; }
    public string Color { get; set; } = string.Empty; // Hex code or CSS class
}
