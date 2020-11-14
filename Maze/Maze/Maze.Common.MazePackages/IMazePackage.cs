namespace Maze.Common.MazePackages
{
    /// <summary>
    /// Базовый интерфейс пакета для передачи через TCP
    /// </summary>
    public interface IMazePackage
    {
        string TypeName { get; }
        string SecurityToken { get; set; }
    }
}
