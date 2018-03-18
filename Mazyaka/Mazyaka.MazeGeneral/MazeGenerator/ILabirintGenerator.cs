using Mazyaka.MazeGeneral.MazeModel;

namespace Mazyaka.MazeGeneral.MazeGenerator
{
    public interface ILabirintGenerator
    {
        MazeStruct Generate(int size);
    }
}