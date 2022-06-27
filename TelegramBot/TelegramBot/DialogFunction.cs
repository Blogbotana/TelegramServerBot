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

        public async Task<Message> EditBuySpecification(long IdChat, int msgId)
        {
            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, text: Localization.GetTranslation("Стоимость 50$"),
                 replyMarkup: GetButtonsBuySpecification(), cancellationToken: TGBot.MyBot.CancellToken);
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
            string enum1 = "UserButtonsMainMenu.";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("База данных узлов"),
                enum1 + UserButtonsMainMenu.DataBase.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Техническая поддержка"),
                enum1+ UserButtonsMainMenu.Support.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Купить лицензию"),
                enum1+ UserButtonsMainMenu.UserButtonsBuyLicenseMenu.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyLicensePrograms()
        {
            string enum1 = "UserButtonsBuyLicenseMenu.";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Tekla Structures"), 
                enum1 + UserButtonsBuyLicenseMenu.TeklaStructures.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Autodesk Revit"), 
                enum1 + UserButtonsBuyLicenseMenu.Revit.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("AutoDesk Navis"), 
                enum1 + UserButtonsBuyLicenseMenu.Navis.ToString()) },
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                enum1 + UserButtonsBuyLicenseMenu.Back) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyTeklaLicensePrograms()
        {
            string enum1 = "UserButtonsTeklaMenu.";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Profile Chooser"),
                enum1 + UserButtonsTeklaMenu.ProfileChooser.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Specifications"),
                enum1 + UserButtonsTeklaMenu.SteelSpecification.ToString()) } ,
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("Excel Report Generator"),
                enum1 + UserButtonsTeklaMenu.ExcelReportGenerator.ToString()) },
                  new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                enum1 + UserButtonsTeklaMenu.Back.ToString()) },
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyNavisLicenseProgram()
        {
            string enum1 = "UserButtonsNavisMenu.";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                enum1 + UserButtonsNavisMenu.Back.ToString()) } ,
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuyRevitLicenseProgram()
        {
            string enum1 = "UserButtonsRevitMenu.";
            var buttons = new[]
            {
                new []{InlineKeyboardButton.WithCallbackData(Localization.GetTranslation("<<Назад"),
                enum1 + UserButtonsRevitMenu.Back.ToString()) } ,
            };

            return new InlineKeyboardMarkup(buttons);
        }

        private InlineKeyboardMarkup GetButtonsBuySpecification()
        {
            InlineKeyboardButton button = new InlineKeyboardButton("Pay 50$");
            button.Pay = true;
            button.CallbackData = "Pay";

            return new InlineKeyboardMarkup(new InlineKeyboardButton[] { button });
        }

    }
}
