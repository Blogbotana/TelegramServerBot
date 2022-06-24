using System;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class Program
    {
        static void Main()
        {
            TGBot bot = TGBot.MyBot;
            bot.Launch();
            Console.ReadLine();
        }
    }
}
