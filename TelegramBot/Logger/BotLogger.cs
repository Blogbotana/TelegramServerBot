using log4net;

namespace TelegramBot.Logger
{
    public class BotLogger : ILogger
    {
        private readonly ILog _logger4net = LogManager.GetLogger(typeof (BotLogger));
        private static BotLogger _instance;

        public static BotLogger GetInstance()
        {
            if(_instance == null)
                return _instance = new BotLogger();

            return _instance;
        }
        private BotLogger()
        {

        }

        public void Debug(object message)
        {
            _logger4net.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _logger4net.Debug(message, exception);
        }

        public void Error(object message)
        {
           _logger4net.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _logger4net.Error(message, exception);
        }

        public void Fatal(object message)
        {
            _logger4net.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _logger4net.Fatal(message, exception);
        }

        public void Info(object message)
        {
            _logger4net.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _logger4net.Info(message, exception);
        }

        public void Warn(object message)
        {
            _logger4net.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _logger4net.Warn(message, exception);
        }
    }
}
