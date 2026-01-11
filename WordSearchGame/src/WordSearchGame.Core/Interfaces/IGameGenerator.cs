using WordSearchGame.Core.Models;

namespace WordSearchGame.Core.Interfaces;

public interface IGameGenerator
{
    (Grid Grid, List<Word> Words) GenerateLevel(int rows, int cols, List<string> wordList);
}
