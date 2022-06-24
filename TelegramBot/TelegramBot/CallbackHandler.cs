using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class CallbackHandler
    {
        public async Task CallbackQueryReceived(CallbackQuery query)
        {
            switch (query.Data)
            {
                case "Rus":
                    {
                        await TGBot.MyBot.BotClient.AnswerCallbackQueryAsync(
         callbackQueryId: query.Id,
         text: $"Настройки установлены");
                        break;
                    }
                default:
                    break;
            }
        }




    }
}
