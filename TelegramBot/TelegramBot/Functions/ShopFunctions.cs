using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Server;
using TelegramBot.DTO.Response;

namespace TelegramBot
{
    public class ShopFunctions
    {
        public async Task<Message> BuyTeklaLisence(long IdChat, UserButtonsTeklaMenu userButtons)
        {
            if(userButtons != UserButtonsTeklaMenu.Back)
            {
                return await TGBot.MyBot.BotClient.SendInvoiceAsync(IdChat, GetTitleOfLicense(userButtons), GetDescriptionOfLecense(userButtons), userButtons.ToString(),
                   GetShopToken(), "RUB", GetButtonsBuySpecification(), cancellationToken: TGBot.MyBot.CancellToken);
            }
            return null;
        }

        private IEnumerable<LabeledPrice> GetButtonsBuySpecification()
        {
            return new List<LabeledPrice>()//TODO Оформить красиво
            {
                new LabeledPrice("Label", 10000)//100.00 currency
            };
        }

        private string GetShopToken()
        {
            return System.IO.File.ReadAllText("payment.txt");
        }

        public async Task PreCheckoutQueryReceived(PreCheckoutQuery preCheckoutQuery)
        {
            await TGBot.MyBot.BotClient.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, TGBot.MyBot.CancellToken);
        }

        public async Task<Message> SuccessfulPaymentRecived(SuccessfulPayment payment,long chatId)
        {
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
                    return "";
                case UserButtonsTeklaMenu.SteelSpecification:
                    return "Specifications for Tekla";
                case UserButtonsTeklaMenu.ExcelReportGenerator:
                    return "";
                default:
                    return "";
            }
        }

        private string GetDescriptionOfLecense(UserButtonsTeklaMenu userButtons)//TODO оформить красиво
        {
            switch (userButtons)
            {
                case UserButtonsTeklaMenu.ProfileChooser:
                    return "";
                case UserButtonsTeklaMenu.SteelSpecification:
                    return "Description of License";
                case UserButtonsTeklaMenu.ExcelReportGenerator:
                    return "";
                default:
                    return "";
            }
        }
    }
}
