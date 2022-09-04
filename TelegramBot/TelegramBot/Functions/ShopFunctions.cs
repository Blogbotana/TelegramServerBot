using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Server;
using TelegramBot.DTO.Response;
using System.Reflection.Metadata.Ecma335;
using System.Configuration;

namespace TelegramBot
{
    public class ShopFunctions
    {
        public async Task<Message> BuyLisence(long IdChat, UserButtonsTeklaMenu userButtons)
        {
            return await TGBot.MyBot.BotClient.SendInvoiceAsync(IdChat, GetTitleOfLicense(userButtons), 
                GetDescriptionOfLecense(userButtons), userButtons.ToString(),
                GetShopToken(), "RUB", GetShopCart(userButtons),
                photoUrl: GetUrlOfLicense(userButtons), photoHeight: 248, photoWidth: 413,//Depends on picture that will be for plugin
                needName: true, needEmail: true, sendEmailToProvider: true,
                cancellationToken: TGBot.MyBot.CancellToken);
        }

        private IEnumerable<LabeledPrice> GetShopCart(UserButtonsTeklaMenu userButtons)//Creating a cart
        {
            switch (userButtons)
            {
                case UserButtonsTeklaMenu.ProfileChooser:
                    return new List<LabeledPrice>()
                    {
                        new LabeledPrice("License of Profile Chooser in Tekla for 1 year", 100000),//1000.00 currency
                    };
                case UserButtonsTeklaMenu.SteelSpecification:
                    return new List<LabeledPrice>()
                    {
                        new LabeledPrice("License of Specification Plugin for 1 year", 100000)
                    };
                case UserButtonsTeklaMenu.ExcelReportGenerator:
                    return new List<LabeledPrice>()
                    {
                        new LabeledPrice("License of Excel Report Generator for templates for 1 year", 100000)
                    };
                default:
                    return new List<LabeledPrice>()
                    {
                        new LabeledPrice("Unknown license", 0)
                    };
            }
        }

        private string GetShopToken()
        {
            return ConfigurationManager.AppSettings["PaymentTokenTest"];
        }

        public async Task PreCheckoutQueryReceived(PreCheckoutQuery preCheckoutQuery)
        {
            await TGBot.MyBot.BotClient.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, TGBot.MyBot.CancellToken);
        }

        public async Task<Message> SuccessfulPaymentRecived(SuccessfulPayment payment,long chatId)
        {
            await ServerAPI.GetInstance.SetEmailAndNameForUser(chatId, payment.OrderInfo.Email, payment.OrderInfo.Name);
            await ServerAPI.GetInstance.UserBoughtThisLicense(payment.InvoicePayload);

            await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId, "Успешно, вы молодец", cancellationToken: TGBot.MyBot.CancellToken);

            InputOnlineFile stiker = new InputOnlineFile("https://tlgrm.ru/_/stickers/5ba/fb7/5bafb75c-6bee-39e0-a4f3-a23e523feded/192/25.webp");
            return await TGBot.MyBot.BotClient.SendStickerAsync(chatId, stiker, cancellationToken: TGBot.MyBot.CancellToken);
        }

        private string GetTitleOfLicense(UserButtonsTeklaMenu userButtons)
        {
            switch (userButtons)
            {
                case UserButtonsTeklaMenu.ProfileChooser:
                    return "Profile Chooser for Tekla plugin";
                case UserButtonsTeklaMenu.SteelSpecification:
                    return "Specifications for Tekla plugin";
                case UserButtonsTeklaMenu.ExcelReportGenerator:
                    return "Excel report generator for templates in Tekla";
                default:
                    return "";
            }
        }

        private string GetDescriptionOfLecense(UserButtonsTeklaMenu userButtons)
        {
            switch (userButtons)
            {
                case UserButtonsTeklaMenu.ProfileChooser:
                    return "This is plugin that helps you to choose profiles quicker. More info: https://telegra.ph/Profile-Chooser-Plugin-08-30";//TODO Understand how to work with links
                case UserButtonsTeklaMenu.SteelSpecification:
                    return "This is plugin for analyzing and changing model. You can create your own specifications also";
                case UserButtonsTeklaMenu.ExcelReportGenerator:
                    return "This is plugin for generate rpt file from existing excel file for Tekla reports";
                default:
                    return "";
            }
        }

        private string GetUrlOfLicense(UserButtonsTeklaMenu userButtons)//TODO make real icons for plugins
        {
            switch (userButtons)
            {
                case UserButtonsTeklaMenu.ProfileChooser:
                    return "https://jurfininvest.ru/wp-content/uploads/2017/03/license2.png";
                case UserButtonsTeklaMenu.SteelSpecification:
                    return "https://jurfininvest.ru/wp-content/uploads/2017/03/license2.png";
                case UserButtonsTeklaMenu.ExcelReportGenerator:
                    return "https://jurfininvest.ru/wp-content/uploads/2017/03/license2.png";
                default:
                    return "";
            }
        }
    }
}
