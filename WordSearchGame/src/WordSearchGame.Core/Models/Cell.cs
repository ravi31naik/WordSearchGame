namespace WordSearchGame.Core.Models;

public class Cell
{
    public Coordinates Position { get; set; } = new(0, 0);
    public char Character { get; set; }
    public bool IsSelected { get; set; }
    // Could belong to multiple words, but usually we just want to know if it's "part of a found word" for coloring.
    // Simplifying for now: if it's found, we might want to know WHICH color to paint it.
    public string HighlightColor { get; set; } = string.Empty;
    public bool IsFound => !string.IsNullOrEmpty(HighlightColor);
}
