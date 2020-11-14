using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.Logging
{
    public class MazeLogManager
    {
        private static MazeLogManager _instance;
        public static MazeLogManager Instance
        {
            get
            {
                return _instance ??= new MazeLogManager();
            }
        }

        private MazeLogManager()
        {

        }

        private Dictionary<string, Logger> _loggers = new Dictionary<string, Logger>();

        public void Write(string msg, params string[] loggers)
        {
            foreach (var logger in loggers)
            {
                if (_loggers.TryGetValue(logger, out var curLogger))
                {
                    curLogger.Log(LogLevel.Info, msg);
                }
            }
        }

        public void Write(string msg, Action action, params string[] loggers)
        {
            var dtStart = DateTime.Now;
            Write("Begin " + msg, loggers);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Write($"ERROR: {msg}. Time: {DateTime.Now - dtStart}{Environment.NewLine}{ex}", loggers);
                throw;
            }
            finally
            {
                Write($"End {msg}. Time: {DateTime.Now - dtStart}", loggers);
            }
        }


        public T Write<T>(string msg, Func<T> func, params string[] loggers)
        {
            var dtStart = DateTime.Now;
            Write("Begin " + msg, loggers);

            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Write($"ERROR: {msg}. Time: {DateTime.Now - dtStart}{Environment.NewLine}{ex}", loggers);
                throw;
            }
            finally
            {
                Write($"End {msg}. Time: {DateTime.Now - dtStart}", loggers);
            }
        }

        public void AddLogger(string loggerName)
        {
            var logger = LogManager.GetLogger(loggerName);
            _loggers[loggerName] = logger;
        }
    }
}
