using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using Maze.Common.Model;
using Maze.Server.Core.Access;
using Maze.Server.Core.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        private bool _isStopped;

        public SimpleMazePackageQueueHandler()
        {
            _mazePackageQueue = new SimpleMazePackageQueue();
            _accessList = new SimpleAccessList();
            _sessionStorage = new DumpSessionStorage();
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
                    Console.WriteLine("Package = " + package.ToString());

                    // TODO: Получение роли пользователя
                    var role = _sessionStorage.GetUserRoleOrNull(package.SecurityToken);
                    Console.WriteLine($"Роль пользователя {role}");

                    // TODO: Передача валидатору
                    var hasAccess = _accessList.HasAccess(package, role);
                    Console.WriteLine($"HasAccess = {hasAccess}");

                    if (!hasAccess)
                    {
                        package = SimplePackageFactory.GetInstance().HasNotAccessPackage();
                    }

                    // TODO: Передача исполнителю


                    // После окончания исполнениня следующий

                }
                else
                {
                    Thread.Sleep(Common.Constants.ServerConstants.PACKAGE_QUEUE_HANDLER_SLEEP);
                }
            }
        }
    }
}
