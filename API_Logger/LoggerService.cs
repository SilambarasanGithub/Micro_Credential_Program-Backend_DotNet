using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using NLog;

namespace API_Logger
{
    public class LoggerService : ILog
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerService()
        {
            
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }
    }
}
