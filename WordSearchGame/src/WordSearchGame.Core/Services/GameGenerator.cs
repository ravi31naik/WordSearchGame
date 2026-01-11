using WordSearchGame.Core.Enums;
using WordSearchGame.Core.Interfaces;
using WordSearchGame.Core.Models;

namespace WordSearchGame.Core.Services;

public class GameGenerator : IGameGenerator
{
    private readonly Random _random = new();

    public (Grid Grid, List<Word> Words) GenerateLevel(int rows, int cols, List<string> wordList)
    {
        var grid = new Grid(rows, cols);
        var placedWords = new List<Word>();

        foreach (var wordText in wordList)
        {
            // Try to place the word
            bool placed = false;
            int attempts = 0;
            while (!placed && attempts < 100)
            {
                placed = TryPlaceWord(grid, wordText.ToUpperInvariant(), out Word? newWord);
                if (placed && newWord != null)
                {
                    placedWords.Add(newWord);
                }
                attempts++;
            }
        }

        FillEmptyCells(grid);

        return (grid, placedWords);
    }

    private bool TryPlaceWord(Grid grid, string text, out Word? placedWord)
    {
        placedWord = null;
        var directions = Enum.GetValues<Direction>();
        var dir = directions[_random.Next(directions.Length)];

        int startRow = _random.Next(grid.Rows);
        int startCol = _random.Next(grid.Columns);

        // Check bounds and overlap
        if (!CanPlace(grid, text, startRow, startCol, dir))
        {
            return false;
        }

        // Place it
        var endPos = Place(grid, text, startRow, startCol, dir);
        
        placedWord = new Word
        {
            Text = text,
            Start = new Coordinates(startRow, startCol),
            End = endPos,
            Direction = dir,
            IsFound = false
        };

        return true;
    }

    private bool CanPlace(Grid grid, string text, int r, int c, Direction dir)
    {
        int dr = 0, dc = 0;
        GetDeltas(dir, out dr, out dc);

        for (int i = 0; i < text.Length; i++)
        {
            int nr = r + i * dr;
            int nc = c + i * dc;

            if (nr < 0 || nr >= grid.Rows || nc < 0 || nc >= grid.Columns)
                return false; // Out of bounds

            var cellChar = grid.Cells[nr, nc].Character;
            if (cellChar != ' ' && cellChar != text[i])
                return false; // Collision with non-matching char
        }

        return true;
    }

    private Coordinates Place(Grid grid, string text, int r, int c, Direction dir)
    {
        int dr = 0, dc = 0;
        GetDeltas(dir, out dr, out dc);

        int nr = r;
        int nc = c;

        for (int i = 0; i < text.Length; i++)
        {
            nr = r + i * dr;
            nc = c + i * dc;
            grid.Cells[nr, nc].Character = text[i];
        }

        return new Coordinates(nr, nc);
    }

    private void GetDeltas(Direction dir, out int dr, out int dc)
    {
        dr = 0; dc = 0;
        switch (dir)
        {
            case Direction.Horizontal:          dr = 0; dc = 1; break;
            case Direction.Vertical:            dr = 1; dc = 0; break;
            case Direction.DiagonalDown:        dr = 1; dc = 1; break;
            case Direction.DiagonalUp:          dr = -1; dc = 1; break;
            case Direction.HorizontalReverse:   dr = 0; dc = -1; break;
            case Direction.VerticalReverse:     dr = -1; dc = 0; break;
            case Direction.DiagonalDownReverse: dr = -1; dc = -1; break; // This is actually Up-Left
            case Direction.DiagonalUpReverse:   dr = 1; dc = -1; break;  // This is Down-Left
        }
    }

    private void FillEmptyCells(Grid grid)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                if (grid.Cells[r, c].Character == ' ')
                {
                    grid.Cells[r, c].Character = chars[_random.Next(chars.Length)];
                }
            }
        }
    }
}
