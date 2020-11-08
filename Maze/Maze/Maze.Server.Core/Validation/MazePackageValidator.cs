using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.Core.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Validation
{
    public class MazePackageValidator : IAccessValidator
    {
        private IAccessList _accessList;

        public MazePackageValidator()
        {
            _accessList = new SimpleAccessList();
        }

        public IMazePackage Validate(IMazePackage package, MazeUserRole role)
        {
            // TODO: Передача валидатору
            var hasAccess = _accessList.HasAccess(package, role);

            if (!hasAccess)
            {
                package = SimplePackageFactory.GetInstance().AccessDeniedResponse();
            }

            return package;
        }
    }
}
