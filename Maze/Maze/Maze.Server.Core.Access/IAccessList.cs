using Maze.Common.MazePackages;
using Maze.Common.Model;

namespace Maze.Server.Core.Access
{
    /// <summary>
    /// Интерфейс листа с доступом к выполнению пакета
    /// Проверяет по пакету и роли права на выполнение операции
    /// </summary>
    public interface IAccessList
    {
        bool HasAccess(IMazePackage package, MazeUserRole role);
    }
}
