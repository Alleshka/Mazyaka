using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.General;

namespace MazeProject.GameService
{
    public interface IManager<T>
    {
        void Add(T lobby);
        void Remove(Guid lobbyID);
        T this[Guid index]
        {
            get;
            set;
        }
    }
}
