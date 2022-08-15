using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.Common;
using Maze.Server.Core.Access;

namespace Maze.Server.Core.Validation
{
    public class MazePackageValidator : IAccessValidator
    {
        private IAccessList _accessList;

        public MazePackageValidator()
        {
            _accessList = MazeDIContaner.Get<IAccessList>();;
        }

        public IMazePackage Validate(IMazePackage package, MazeUserRole role)
        {
            var hasAccess = false;
            if (package is IMazePackageRequest request)
            {
                hasAccess = _accessList.HasAccess(request, role);
            }

            if (!hasAccess)
            {
                // Если нет доступа, то заменяем пакет
                package = MazeDIContaner.Get<IPackageFactory>().AccessDeniedResponse();
            }

            return package;
        }
    }
}
