using System.Threading.Tasks;

namespace Maze.Client.Abstractions
{
    public interface IConnectable
    {
        Task ConnectAsync();
    }
}
