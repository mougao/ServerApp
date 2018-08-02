using System;
using System.Collections.Generic;
using System.Text;

namespace XY.CommonBase
{
    public class LogHelper
    {
        //Trace,Debug,Info,Warn,Error,Fatal
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

    }
}
