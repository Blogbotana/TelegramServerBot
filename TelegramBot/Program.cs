using log4net.Config;

namespace TelegramBot
{
    class Program
    {
        static void Main()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Logger.config"));
            TGBot bot = TGBot.MyBot;
            bot.Launch();
            Console.ReadLine();
        }
    }
}
