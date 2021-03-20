using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Server.AutofacContainer;
using System;
using System.Net;

namespace Maze.Server.MazeConfiguration
{
    public interface IMazeConfigurator<T>
    {
        void Configurate(T item);
    }

    public interface IServiceConfigurator : IMazeConfigurator<MazeAutofacContainer>
    {
    }

    public interface IImplementationConfigurator : IMazeConfigurator<MazeAutofacContainer>
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
