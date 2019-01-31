using Colligo.O365Product.ServiceInterface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.LoggerService
{
    public class ApplicationLog : ILogManager
    {
        private static ILog logManager;

        /// <summary>
        /// error log for simple message
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Error(message, null);
        }

        /// <summary>
        /// Method for collection error
        /// </summary>
        /// <param name="exception"></param>
        public void Error(List<string> exception)
        {
            if (exception != null)
            {
                Error(string.Join(",", exception));
            }
        }

        /// <summary>
        /// Method for collection error
        /// </summary>
        /// <param name="exception"></param>
        public void Error(string message, List<string> exception, Exception ex = null)
        {
            if (exception != null)
            {
                Error(message + string.Join(",", exception), ex);
            }
        }

        /// <summary>
        /// log whole exception
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            LogManager.Error(ex);
        }

        public static ILog SetLogger(string loggerName)
        {
            return GetLoggerByName(loggerName);

        }
        public void Error(string message, Exception ex)
        {
            LogManager.Error(message, ex);
        }

        public void Info(string message)
        {
            LogManager.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            LogManager.InfoFormat(format, args);
        }

        public void Debug(string message)
        {
            LogManager.Debug(message);
        }
        public void Debug(string message, Exception ex)
        {
            LogManager.Debug(message, ex);
        }

        public void Debug(string format, params object[] args)
        {
            LogManager.DebugFormat(format, args);
        }

        public void Warn(string message)
        {
            LogManager.Warn(message);
        }

        private static ILog LogManager
        {
            get
            {
                if (logManager == null)
                {
                    SetLogger("File");
                }
                return logManager;
            }
            set
            {
                logManager = value;
            }
        }

        private static ILog GetLoggerByName(string loggerName)
        {

            logManager = log4net.LogManager.GetLogger(loggerName);

            return logManager;
        }
    }
}
