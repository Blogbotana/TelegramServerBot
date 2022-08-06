using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelegramBot
{
    public class ShopFunctions
    {
        public async Task<Message> BuyTeklaLisence(long IdChat, UserButtonsTeklaMenu userButtons)
        {
            if(userButtons == UserButtonsTeklaMenu.SteelSpecification)
            {
                return await TGBot.MyBot.BotClient.SendInvoiceAsync(IdChat, "Specifications for Tekla", "description", "Payment_SteelSpecification",
                   GetShopToken(), "RUB", GetButtonsBuySpecification(), cancellationToken: TGBot.MyBot.CancellToken);
            }
            else if (userButtons == UserButtonsTeklaMenu.ExcelReportGenerator)
            {

            }
            else if(userButtons == UserButtonsTeklaMenu.ProfileChooser)
            {

            }
            return null;
        }

        private IEnumerable<LabeledPrice> GetButtonsBuySpecification()
        {
            return new List<LabeledPrice>()
            {
                new LabeledPrice("Label", 10000)//100.00 currency
            };
        }
        //TODO сделать класс по работе из token txt
        private string GetShopToken()
        {
            return System.IO.File.ReadAllText("payment.txt");
        }

        public async Task PreCheckoutQueryReceived(PreCheckoutQuery preCheckoutQuery)
        {
            //TODO Тут будет обработка перед тем, как сказать, что успешная оплата
            await TGBot.MyBot.BotClient.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, TGBot.MyBot.CancellToken);

        }

        public async Task SuccessfulPaymentRecived(long chatId)
        {
            await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId, "Успешно, вы молодец", cancellationToken: TGBot.MyBot.CancellToken);

            InputOnlineFile stiker = new InputOnlineFile("https://tlgrm.ru/_/stickers/5ba/fb7/5bafb75c-6bee-39e0-a4f3-a23e523feded/192/25.webp");
            await TGBot.MyBot.BotClient.SendStickerAsync(chatId, stiker, cancellationToken: TGBot.MyBot.CancellToken);
        }
    }
}
