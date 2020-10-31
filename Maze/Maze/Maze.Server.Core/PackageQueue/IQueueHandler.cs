using Maze.Common.MazePackages;
using Maze.Server.Core.Access;
using Maze.Server.Core.Executor;
using Maze.Server.Core.SessionStorage;
using System;
using System.Threading;

namespace Maze.Server.Core.PackageQueue
{
    public interface IMazePackageQueueHandler
    {
        void Start();
        void Stop();

        void AddPackage(IMazePackage mazePackage);
    }

    public class SimpleMazePackageQueueHandler : IMazePackageQueueHandler
    {
        private IMazePackageQueue _mazePackageQueue;
        private IAccessList _accessList;
        private ISessionStorage _sessionStorage;
        private IMazePackageExecutor _mazePackageExecutor;

        private bool _isStopped;

        public SimpleMazePackageQueueHandler()
        {
            _mazePackageQueue = new SimpleMazePackageQueue();
            _accessList = new SimpleAccessList();
            _sessionStorage = new DumpSessionStorage();
            _mazePackageExecutor = new SimpleMazePackageExecutor();
        }

        public void AddPackage(IMazePackage mazePackage)
        {
            _mazePackageQueue.Add(mazePackage);
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
                var package = _mazePackageQueue.GetPackage();
                if (package != null)
                {
                    var role = _sessionStorage.GetUserRoleOrNull(package.SecurityToken);

                    // TODO: Передача валидатору
                    var hasAccess = _accessList.HasAccess(package, role);

                    if (!hasAccess)
                    {
                        package = SimplePackageFactory.GetInstance().HasNotAccessPackage();
                    }

                    _mazePackageExecutor.Execute(package);
                }
                else
                {
                    Thread.Sleep(Common.Constants.ServerConstants.PACKAGE_QUEUE_HANDLER_SLEEP);
                }
            }
        }
    }
}
