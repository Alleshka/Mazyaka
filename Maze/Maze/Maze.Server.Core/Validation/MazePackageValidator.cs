using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.AutofacContainer;
using Maze.Server.Core.Access;

namespace Maze.Server.Core.Validation
{
    public class MazePackageValidator : IAccessValidator
    {
        private IAccessList _accessList;

        public MazePackageValidator()
        {
            _accessList = MazeAutofacContainer.Instance.GetImplementation<IAccessList>();;
        }

        public IMazePackage Validate(IMazePackage package, MazeUserRole role)
        {
            var hasAccess = _accessList.HasAccess(package, role);

            if (!hasAccess)
            {
                // Если нет доступа, то заменяем пакет
                package = MazeAutofacContainer.Instance.GetImplementation<IPackageFactory>().AccessDeniedResponse();
            }

            return package;
        }
    }
}
