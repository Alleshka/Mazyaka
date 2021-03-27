using Maze.Common.Model;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет регистрации пользователей
    /// </summary>
    internal class RegisterUserPackage : BaseMazePackageRequest
    {
        public string UserLogin { get; set; }

        public override MazeUserRole Roles => MazeUserRole.All;
    }

    /// <summary>
    /// Пакет с ответом регистрации 
    /// </summary>
    internal class RegisterUserResponePackage : BaseMazePackageResponce
    {
        public MazeUser MazeUser { get; set; }
    }
}
