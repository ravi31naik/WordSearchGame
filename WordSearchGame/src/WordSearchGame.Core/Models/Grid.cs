namespace WordSearchGame.Core.Models;

public class Grid
{
    public int Rows { get; }
    public int Columns { get; }
    public Cell[,] Cells { get; }

    public Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Cells = new Cell[rows, columns];
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Cells[r, c] = new Cell() { Position = new Coordinates(r, c), Character = ' ' };
            }
        }
    }
}
