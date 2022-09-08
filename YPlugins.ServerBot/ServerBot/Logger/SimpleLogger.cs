using log4net;

namespace ServerBot.Logger
{
    public class SimpleLogger : ILogger
    {
        private readonly ILog Logger4Net = LogManager.GetLogger(typeof(SimpleLogger));
        private static SimpleLogger thisLogger;

        public static SimpleLogger Current
        {
            get
            {
                if (thisLogger == null)
                {
                    thisLogger = new SimpleLogger();
                }
                return thisLogger;
            }
        }

        public void Debug(object message)
        {
            Logger4Net.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            Logger4Net.Debug(message, exception);
        }

        public void Info(object message)
        {
            Logger4Net.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            Logger4Net.Info(message, exception);
        }

        public void Warn(object message)
        {
            Logger4Net.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            Logger4Net.Warn(message, exception);
        }

        public void Error(object message)
        {
            Logger4Net.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            Logger4Net.Error(message, exception);
        }

        public void Fatal(object message)
        {
            Logger4Net.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Logger4Net.Fatal(message, exception);
        }
    }
}
