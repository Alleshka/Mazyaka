using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze.Serializier
{
    public class NewtosoftJsonSerialzer : ISerializer
    {
        public byte[] ToBytes<T>(T obj, Type[] types = null)
        {
            throw new NotImplementedException();
        }

        public T ToObject<T>(byte[] bytes, Type[] types = null)
        {
            throw new NotImplementedException();
        }

        public T ToObject<T>(string strFormat, Type[] types = null)
        {
            throw new NotImplementedException();
        }

        public string ToStringFormat<T>(T obj, Type[] types = null)
        {
            throw new NotImplementedException();
        }
    }
}
