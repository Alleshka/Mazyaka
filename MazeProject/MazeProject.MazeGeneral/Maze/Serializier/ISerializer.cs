using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze.Serializier
{
    public interface ISerializer
    {
        byte[] ToBytes<T>(T obj, Type[] types = null);
        String ToStringFormat<T>(T obj, Type[] types = null);

        T ToObject<T>(byte[] bytes, Type[] types = null);
        T ToObject<T>(String strFormat, Type[] types = null);
    }
}
