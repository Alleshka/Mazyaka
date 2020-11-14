using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.Core.Access;
using Maze.Server.ImplementationStorage;

namespace Maze.Server.Core.Validation
{
    public class MazePackageValidator : IAccessValidator
    {
        private IAccessList _accessList;

        public MazePackageValidator()
        {
            _accessList = MazeImplementationStorage.Instance.GetImplementation<IAccessList>();;
        }

        public IMazePackage Validate(IMazePackage package, MazeUserRole role)
        {
            var hasAccess = _accessList.HasAccess(package, role);

            if (!hasAccess)
            {
                // Если нет доступа, то заменяем пакет
                package = MazeImplementationStorage.Instance.GetImplementation<IPackageFactory>().AccessDeniedResponse();
            }

            return package;
        }
    }
}
