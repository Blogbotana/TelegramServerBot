using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Localization;

namespace TelegramBot
{
    public class DialogFunction
    {
        public async Task<Message> SendHelloMessage(long IdChat)
        {
            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: IdChat, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate("tg_Hello"),
                replyMarkup: GetButtonsMainMenu(TGBot.MyBot.Users[IdChat].LanguageCode), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditHelloMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate("tg_Hello"),
                replyMarkup: GetButtonsMainMenu(TGBot.MyBot.Users[IdChat].LanguageCode), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync( IdChat, msgId, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate("tg_ChooseProgram"),
                replyMarkup: GetButtonsBuyLicensePrograms(TGBot.MyBot.Users[IdChat].LanguageCode), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyTeklaLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate("tg_ChoosePlugin"),
                replyMarkup: GetButtonsBuyTeklaLicensePrograms(TGBot.MyBot.Users[IdChat].LanguageCode), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> SendCustomMessage(long IdChat, string Text)
        {
            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: IdChat, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate(Text),
                replyMarkup: HideKeyBoard(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditCustomMessage(long IdChat, int msgId, string Text)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate(Text),
                 cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyNavisLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate("tg_Development"),
                 replyMarkup: GetButtonsBuyNavisLicenseProgram(TGBot.MyBot.Users[IdChat].LanguageCode), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyRevitLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Translator.GetLocalization(TGBot.MyBot.Users[IdChat].LanguageCode).GetTranslate("tg_Development"),
                 replyMarkup: GetButtonsBuyRevitLicenseProgram(TGBot.MyBot.Users[IdChat].LanguageCode), cancellationToken: TGBot.MyBot.CancellToken);
        }

        private IReplyMarkup HideKeyBoard()
        {
            return new ReplyKeyboardRemove();
        }

        private InlineKeyboardMarkup GetButtonsMainMenu(string lang)
        {
            string thisenum = typeof(UserButtonsMainMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Translator.GetLocalization(lang).GetTranslate("tg_b_mylicenses"),
                thisenum + UserButtonsMainMenu.DataBase.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Translator.GetLocalization(lang).GetTranslate("tg_Support"),
                thisenum+ UserButtonsMainMenu.Support.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Translator.GetLocalization(lang).GetTranslate("tg_BuyLicense"),
                thisenum+ UserButtonsMainMenu.UserButtonsBuyLicenseMenu.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyLicensePrograms(string lang)
        {
            string thisenum = typeof(UserButtonsBuyLicenseMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData("Tekla Structures",
                thisenum + UserButtonsBuyLicenseMenu.TeklaStructures.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData("Autodesk Revit",
                thisenum + UserButtonsBuyLicenseMenu.Revit.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData("AutoDesk Navis",
                thisenum + UserButtonsBuyLicenseMenu.Navis.ToString()) },
                new []{InlineKeyboardButton.WithCallbackData("<<" + Translator.GetLocalization(lang).GetTranslate("b_Undo"),
                thisenum + UserButtonsBuyLicenseMenu.Back.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyTeklaLicensePrograms(string lang)
        {
            string thisenum = typeof(UserButtonsTeklaMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData("Profile Chooser",
                thisenum + UserButtonsTeklaMenu.ProfileChooser.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData("Specifications",
                thisenum + UserButtonsTeklaMenu.SteelSpecification.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData("Excel Report Generator",
                thisenum + UserButtonsTeklaMenu.ExcelReportGenerator.ToString()) },
                  new []{InlineKeyboardButton.WithCallbackData("<<" + Translator.GetLocalization(lang).GetTranslate("b_Undo"),
                thisenum + UserButtonsTeklaMenu.Back.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyNavisLicenseProgram(string lang)
        {
            string thisenum = typeof(UserButtonsNavisMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData("<<" + Translator.GetLocalization(lang).GetTranslate("b_Undo"),
                thisenum + UserButtonsNavisMenu.Back.ToString()) } ,
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyRevitLicenseProgram(string lang)
        {
            string thisenum = typeof(UserButtonsRevitMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData("<<" + Translator.GetLocalization(lang).GetTranslate("b_Undo"),
                thisenum + UserButtonsRevitMenu.Back.ToString()) } ,
            };

            return new InlineKeyboardMarkup(buttons);
        }
    }
}
