using log4net;
using log4net.Repository;
using System;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace ee.Framework.Log
{
    public static class Logger
    {

        public enum LoggerType
        {
            Default = 0,
            SystemLogger = 1,
            TransferLogger = 2,
        }

        private static ILog SystemLogger;
        private static ILog TransferLogger;


        public static bool Configure(string program)
        {

            ILoggerRepository repository = LogManager.CreateRepository("NETStandardRepository");
            log4net.GlobalContext.Properties["program"] = program;
            log4net.Config.XmlConfigurator.Configure(repository);

            var repo = log4net.LogManager.GetRepository(repository.Name);

            if (repo.Configured)
            {
                SystemLogger = LogManager.GetLogger(repository.Name,"SystemLog");
                TransferLogger = LogManager.GetLogger(repository.Name,"TransferLog");
            }
            return repo.Configured;
        }


        public static ILog GetLogger(LoggerType type = LoggerType.Default)
        {
            ILog logger = SystemLogger;
            switch (type)
            {
                case LoggerType.Default:
                case LoggerType.SystemLogger:
                    logger = SystemLogger;
                    break;
                case LoggerType.TransferLogger:
                    logger = TransferLogger;
                    break;
                default:
                    logger = SystemLogger;
                    break;
            }
            return logger;
        }

        public static void Debug(object message, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Debug(message);
        }
        public static void Debug(object message, Exception exception, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Debug(message, exception);
        }

        public static void Info(object message, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Info(message);
        }

        public static void Info(object message, Exception exception, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Info(message, exception);
        }

        public static void Warn(object message, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Warn(message);
        }

        public static void Warn(object message, Exception exception, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Warn(message, exception);
        }


        public static void Error(object message, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Error(message);
        }
        public static void Error(object message, Exception exception, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Error(message, exception);
        }

        public static void Fatal(object message, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Fatal(message);
        }

        public static void Fatal(object message, Exception exception, LoggerType type = LoggerType.Default)
        {
            GetLogger(type)?.Fatal(message, exception);
        }


    }
}
