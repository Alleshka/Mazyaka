namespace Maze.Common.MazePackages
{
    /// <summary>
    /// Интерфейс парсера пакетов
    /// </summary>
    public interface IMazePackageParser
    {
        /// <summary>
        /// Превращает пакет в набор байт для передачи
        /// </summary>
        /// <param name="package">Передаваемый пакет</param>
        /// <returns>Набор байт</returns>
        byte[] GetBytes(IMazePackage package);

        /// <summary>
        /// Восстанавливает пакет из набора байт
        /// </summary>
        /// <param name="bytes">Набор байт</param>
        /// <returns>Восстановленный пакет</returns>
        IMazePackage GetPackage(byte[] bytes);
    }
}
