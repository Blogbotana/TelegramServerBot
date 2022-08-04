using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class DialogFunction
    {
        ShopFunctions shopFunctions = new ShopFunctions();
        public async Task<Message> SendHelloMessage(long IdChat)
        {
            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: IdChat, text: Localization.GetTranslation("Привет, это бот школы инженеров, где можно задать интересующие вас вопросы и купить лицензию. Добро пожаловать"),
                replyMarkup: GetButtonsMainMenu(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditHelloMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Localization.GetTranslation("Привет, это бот школы инженеров, где можно задать интересующие вас вопросы и купить лицензию. Добро пожаловать"),
                replyMarkup: GetButtonsMainMenu(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync( IdChat, msgId, text: Localization.GetTranslation("Выберите программу к которой хотите купить плагин"),
                replyMarkup: GetButtonsBuyLicensePrograms(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyTeklaLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Localization.GetTranslation("Выберите плагин"),
                replyMarkup: GetButtonsBuyTeklaLicensePrograms(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> SendCustomMessage(long IdChat, string Text)
        {
            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: IdChat, text: Localization.GetTranslation(Text),
                replyMarkup: HideKeyBoard(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditCustomMessage(long IdChat, int msgId, string Text)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Localization.GetTranslation(Text),
                 cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyNavisLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Localization.GetTranslation("Раздел в разработке"),
                 replyMarkup: GetButtonsBuyNavisLicenseProgram(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditBuyRevitLicenseMessage(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Localization.GetTranslation("Раздел в разработке"),
                 replyMarkup: GetButtonsBuyRevitLicenseProgram(), cancellationToken: TGBot.MyBot.CancellToken);
        }

        private IReplyMarkup ShowKeyBoard()
        {
            var replyKeyboardMarkup = new[]
            {
                new[]{ new KeyboardButton(Localization.GetTranslation("База данных узлов")) },
                new[]{ new KeyboardButton(Localization.GetTranslation("Техническая поддержка") ) },
                new[]{ new KeyboardButton ( Localization.GetTranslation("Купить лицензию")) }
            };
            var markup = new ReplyKeyboardMarkup(replyKeyboardMarkup);
            markup.ResizeKeyboard = true;
            return markup;
        }

        private IReplyMarkup HideKeyBoard()
        {
            return new ReplyKeyboardRemove();
        }

        private InlineKeyboardMarkup GetButtonsMainMenu()
        {
            string thisenum = typeof(UserButtonsMainMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("База данных узлов"),
                thisenum + UserButtonsMainMenu.DataBase.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Техническая поддержка"),
                thisenum+ UserButtonsMainMenu.Support.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Купить лицензию"),
                thisenum+ UserButtonsMainMenu.UserButtonsBuyLicenseMenu.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyLicensePrograms()
        {
            string thisenum = typeof(UserButtonsBuyLicenseMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Tekla Structures"),
                thisenum + UserButtonsBuyLicenseMenu.TeklaStructures.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Autodesk Revit"),
                thisenum + UserButtonsBuyLicenseMenu.Revit.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("AutoDesk Navis"),
                thisenum + UserButtonsBuyLicenseMenu.Navis.ToString()) },
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                thisenum + UserButtonsBuyLicenseMenu.Back.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyTeklaLicensePrograms()
        {
            string thisenum = typeof(UserButtonsTeklaMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Profile Chooser"),
                thisenum + UserButtonsTeklaMenu.ProfileChooser.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Specifications"),
                thisenum + UserButtonsTeklaMenu.SteelSpecification.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Excel Report Generator"),
                thisenum + UserButtonsTeklaMenu.ExcelReportGenerator.ToString()) },
                  new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                thisenum + UserButtonsTeklaMenu.Back.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyNavisLicenseProgram()
        {
            string thisenum = typeof(UserButtonsNavisMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                thisenum + UserButtonsNavisMenu.Back.ToString()) } ,
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyRevitLicenseProgram()
        {
            string thisenum = typeof(UserButtonsRevitMenu).Name + ".";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                thisenum + UserButtonsRevitMenu.Back.ToString()) } ,
            };

            return new InlineKeyboardMarkup(buttons);
        }
    }
}
