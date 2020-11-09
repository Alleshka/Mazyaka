using Maze.Common.MazePackages.MazePackages;
using System.IO;
using System.IO.Compression;

namespace Maze.Common.MazePackages.Parsers
{
    public class JsonCompressedMazePackageParser : JsonMazePackageParser
    {
        public override byte[] GetBytes(IMazePackage package)
        {
            var data = base.GetBytes(package);
            using (var outputStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal))
                {
                    using (var memoryStream = new MemoryStream(data))
                    {
                        memoryStream.CopyTo(gzipStream);
                    }
                }
                return outputStream.ToArray();
            }
        }

        public override IMazePackage GetPackage(byte[] bytes)
        {
            using (var result = new MemoryStream())
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        gzipStream.CopyTo(result);
                    }
                }
                return base.GetPackage(result.ToArray());
            }
        }
    }
}