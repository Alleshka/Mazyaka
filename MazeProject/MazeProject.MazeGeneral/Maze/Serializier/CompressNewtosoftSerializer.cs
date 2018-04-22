using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze.Serializier
{
    public class CompressNewtosoftSerializer : ISerializer
    {
        public byte[] ToByte<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public T ToObject<T>(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public T ToObject<T>(string strFormat)
        {
            throw new NotImplementedException();
        }

        public string ToStringFormat<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
