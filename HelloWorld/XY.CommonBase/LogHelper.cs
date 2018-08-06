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
            Console.WriteLine(message);
            logger.Trace(message);

        }

        public static void Trace(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            logger.Trace(message,args);

        }

        public static void Debug(string message)
        {
            Console.WriteLine(message);
            logger.Debug(message);
        }

        public static void Debug(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            logger.Debug(message, args);
        }

        public static void Info(string message)
        {
            Console.WriteLine(message);
            logger.Info(message);
        }

        public static void Info(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            logger.Info(message, args);
        }

        public static void Warn(string message)
        {
            Console.WriteLine(message);
            logger.Warn(message);
        }

        public static void Warn(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            logger.Warn(message, args);
        }

        public static void Error(string message)
        {
            Console.WriteLine(message);
            logger.Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            logger.Error(message, args);
        }

        public static void Fatal(string message)
        {
            Console.WriteLine(message);
            logger.Fatal(message);
        }

        public static void Fatal(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            logger.Fatal(message, args);
        }
    }
}
