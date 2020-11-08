using Maze.Common.MazePackages;
using Maze.Server.Core.Executor;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionStorage;
using Maze.Server.Core.Validation;
using System;
using System.Net;
using System.Threading;

namespace Maze.Server.Core.PackageQueue
{
    public interface IMazePackageQueueHandler
    {
        void Start();
        void Stop();

        void AddPackage(IMazePackage mazePackage, IPEndPoint endPoint);
    }

    public class SimpleMazePackageQueueHandler : IMazePackageQueueHandler
    {
        private IMazePackageQueue _mazePackageQueue;

        private IMazePackageExecutor _mazePackageExecutor;
        private IAccessValidator _accessValidator;

        private bool _isStopped;

        public SimpleMazePackageQueueHandler()
        {
            // TODO: Вынести в конфигурацию сервера?
            _mazePackageQueue = new SimpleMazePackageQueue();

            _mazePackageExecutor = new SimpleMazePackageExecutor();

            _accessValidator = new MazePackageValidator();
        }

        public void AddPackage(IMazePackage mazePackage, IPEndPoint endPoint)
        {
            _mazePackageQueue.Add(mazePackage, endPoint);
        }

        public void Start()
        {
            _isStopped = false;
            Thread receiveThread = new Thread(new ThreadStart(HandlePackage));
            receiveThread.Start();
        }

        public void Stop()
        {
            _isStopped = true;
        }

        public void HandlePackage()
        {
            while (!_isStopped)
            {
                var package = _mazePackageQueue.GetPackage(out var endPoint);
                if (package != null)
                {
                    package = _accessValidator.Validate(package, MazeServiceStorage.Instance.GetService<ISessionStorage>().GetUserRoleOrNull(package.SecurityToken));
                    _mazePackageExecutor.Execute(package, endPoint);
                }
                else
                {
                    Thread.Sleep(Common.Constants.ServerConstants.PACKAGE_QUEUE_HANDLER_SLEEP);
                }
            }
        }
    }
}
