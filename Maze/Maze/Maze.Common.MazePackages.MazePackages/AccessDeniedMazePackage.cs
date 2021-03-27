namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет с сообщением о запрете доступа
    /// </summary>
    internal class AccessDeniedMazePackage : BaseMazePackageResponce
    {
        public string Message { get; set; } = "Доступ запрещён";
    }
}
