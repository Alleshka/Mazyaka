using Maze.Common.MazePackages;
using Maze.Server.Common;
using Maze.Server.Core.Executor;
using Maze.Server.Core.Validation;
using Maze.Server.MazeService.SessionService;
using System.Net;
using System.Threading;

namespace Maze.Server.Core.QueueHandler
{
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
                    package = _accessValidator.Validate(package, MazeDIContaner.Get<ISessionService>().GetUserRoleOrDefault(package.SecurityToken));
                    _mazePackageExecutor.Execute(package, endPoint);
                }
                else
                {
                    Thread.Sleep(Maze.Common.Constants.ServerConstants.PACKAGE_QUEUE_HANDLER_SLEEP);
                }
            }
        }
    }
}
