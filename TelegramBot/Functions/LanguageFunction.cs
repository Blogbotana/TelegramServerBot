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

        private IReplyMarkup GetButtonsLanguage()//Lang_ + IETF_LanguageTag
        {
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData("Русский", "Lang_ru") } ,
                new []{InlineKeyboardButton.WithCallbackData("English", "Lang_en") } ,
                new []{InlineKeyboardButton.WithCallbackData("Dutch", "Lang_nl") },
                new []{InlineKeyboardButton.WithCallbackData("Français", "Lang_fr") },
                new []{InlineKeyboardButton.WithCallbackData("Deutsch", "Lang_de") } ,
                new []{InlineKeyboardButton.WithCallbackData("Italiano", "Lang_it") } ,
                new []{InlineKeyboardButton.WithCallbackData("Español", "Lang_es") } ,
                new []{InlineKeyboardButton.WithCallbackData("日本語", "Lang_ja") },
                new []{InlineKeyboardButton.WithCallbackData("简体中文", "Lang_zh") } ,
                new []{InlineKeyboardButton.WithCallbackData("Čeština", "Lang_cs") },
                new []{InlineKeyboardButton.WithCallbackData("Português", "Lang_pt") } ,
                new []{InlineKeyboardButton.WithCallbackData("Magyar", "Lang_hu") } ,
                new []{InlineKeyboardButton.WithCallbackData("Polski", "Lang_pl") }
            };

            return new InlineKeyboardMarkup(buttons);
        }

    }
}
