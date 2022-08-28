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
        public async Task<Message> SendLanguageMessageToUser(ChatId chatid)
        {
            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: chatid, text: "👋 Please, select your language.",
                replyMarkup: GetButtonsLanguage(),
                            cancellationToken: TGBot.MyBot.CancellToken);
        }

        private IReplyMarkup GetButtonsLanguage()
        {
            var buttons = new[]//TODO исправить языки
            {
                new []{InlineKeyboardButton.WithCallbackData("Русский", "Lang_ru") } ,
                new []{InlineKeyboardButton.WithCallbackData("English", "Lang_en") } ,
                new []{InlineKeyboardButton.WithCallbackData("Dutch", "Lang_Nld") },
                new []{InlineKeyboardButton.WithCallbackData("Français", "Lang_Fra") },
                new []{InlineKeyboardButton.WithCallbackData("Deutsch", "Lang_Deu") } ,
                new []{InlineKeyboardButton.WithCallbackData("Italiano", "Lang_Ita") } ,
                new []{InlineKeyboardButton.WithCallbackData("Español", "Lang_Esp") } ,
                new []{InlineKeyboardButton.WithCallbackData("日本語", "Lang_Jpn") },
                new []{InlineKeyboardButton.WithCallbackData("简体中文", "Lang_Chs") } ,
                new []{InlineKeyboardButton.WithCallbackData("中国传统", "Lang_Cht") } ,
                new []{InlineKeyboardButton.WithCallbackData("Čeština", "Lang_Csy") },
                new []{InlineKeyboardButton.WithCallbackData("Português", "Lang_Ptb") } ,
                new []{InlineKeyboardButton.WithCallbackData("Magyar", "Lang_Hun") } ,
                new []{InlineKeyboardButton.WithCallbackData("Polskie", "Lang_Plk") }
            };

            return new InlineKeyboardMarkup(buttons);
        }

    }
}
