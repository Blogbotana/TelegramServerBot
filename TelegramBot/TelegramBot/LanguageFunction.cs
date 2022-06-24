using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class LanguageFunction
    {
        public async Task SendLanguageMessageToUser(ChatId chatid)
        {
            await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: chatid, text: "👋 Please, select your language.",
                replyMarkup: GetButtonsLanguage(),
                            cancellationToken: TGBot.MyBot.CancellToket);
        }

        private IReplyMarkup GetButtonsLanguage()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>()
            {
                InlineKeyboardButton.WithCallbackData("Русский", "Rus") , InlineKeyboardButton.WithCallbackData("English", "Eng")
            };

            return new InlineKeyboardMarkup(buttons);
        }

    }
}
