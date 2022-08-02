using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazePackages.Parsers
{
    public abstract class BaseDecorator : IMazePackageParser
    {
        protected IMazePackageParser parser;

        public BaseDecorator(IMazePackageParser packageParser)
        {
            parser = packageParser;
        }

        public virtual byte[] GetBytes(IMazePackage package)
        {
            if (parser != null)
            {
                return parser.GetBytes(package);
            }
            else
            {
                return null;
            }
        }

        public virtual IMazePackage GetPackage(byte[] bytes)
        {
            if (parser != null)
            {
                return parser.GetPackage(bytes);
            }
            else
            {
                return null;
            }
        }
    }
}
