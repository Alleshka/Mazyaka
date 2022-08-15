using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Server.Common;
using System;
using System.Net;

namespace Maze.Server.MazeConfiguration
{
    public interface IMazeConfigurator<T>
    {
        void Configurate(T item);
        void Configurate();
    }

    public interface IServiceConfigurator : IMazeConfigurator<MazeDIContaner>
    {
    }

    public interface IImplementationConfigurator : IMazeConfigurator<MazeDIContaner>
    {

    }

    public interface ILogsConfigurator : IMazeConfigurator<MazeLogManager>
    {
    }

    public interface IDataExchangeConfigurator
    {
        IDataExchanger Create(IMazePackageParser packageParser, Action<IMazePackage, IPEndPoint, IDataExchanger> onReceiveMessage);
    }
}
