using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    class LogDetails
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string msg)
        {
            logger.Debug(msg);
        }
        public void LogError(string msg)
        {
            logger.Error(msg);
        }
        public void LogInfo(string msg)
        {
            logger.Info(msg);
        }
        public void LogWarn(string msg)
        {
            logger.Warn(msg);
        }
    }
}
