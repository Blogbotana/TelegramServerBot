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
        DialogFunction dialog = new DialogFunction();
        SupportFunction support = new SupportFunction();
        public async Task CallbackQueryReceived(CallbackQuery query, Message lastmessage)
        {
            switch (query.Data)
            {
                case string language when language.StartsWith("Lang_"):
                    {
                        await TGBot.MyBot.BotClient.DeleteMessageAsync(query.From.Id, lastmessage.MessageId, TGBot.MyBot.CancellToken);
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.SendHelloMessage(query.From.Id);
                        //TODO Сделать запись в данных что пользователь хочет этот язык
                        break;
                    }
                case string button when button.StartsWith(typeof(UserButtonsMainMenu).Name):
                    {
                        await ReplyToUserButtonsMainMenu(query,lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith(typeof(UserButtonsBuyLicenseMenu).Name):
                    {
                        await ReplyToUserButtonsBuyLicenseMenu(query,lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith(typeof(UserButtonsTeklaMenu).Name):
                    {
                        await ReplyToUserBuyTeklaLicense(query, lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith(typeof(UserButtonsNavisMenu).Name):
                    {
                        await ReplyToUserBuyNavisLicense(query, lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith(typeof(UserButtonsRevitMenu).Name):
                    {
                        await ReplyToUserBuyRevitLicense(query, lastmessage, button);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserButtonsMainMenu(CallbackQuery query, Message lastmessage, string button)
        {
            var thisenum = typeof(UserButtonsMainMenu).Name + ".";
            switch (button)
            {
                case string result when result == thisenum + UserButtonsMainMenu.Support.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await support.EditSupportMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string result when result == thisenum + UserButtonsMainMenu.DataBase.GetType().ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditCustomMessage(query.From.Id, lastmessage.MessageId, "Раздел в разработке");
                        break;
                    }
                case string result when result == thisenum + UserButtonsMainMenu.UserButtonsBuyLicenseMenu.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserButtonsBuyLicenseMenu(CallbackQuery query, Message lastmessage, string button)
        {
            var thisenum = typeof(UserButtonsBuyLicenseMenu).Name + ".";
            switch (button)
            {
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.TeklaStructures.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyTeklaLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.Revit.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyRevitLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.Navis.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyNavisLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.Back.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditHelloMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserBuyTeklaLicense(CallbackQuery query, Message lastmessage, string button)
        {
            var thisenum = typeof(UserButtonsTeklaMenu).Name + ".";
            switch (button)
            {
                case string data when data == thisenum + UserButtonsTeklaMenu.ProfileChooser.ToString():
                    {

                        break;
                    }
                case string data when data == thisenum + UserButtonsTeklaMenu.SteelSpecification.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuySpecification(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsTeklaMenu.ExcelReportGenerator.ToString():
                    {

                        break;
                    }
                case string data when data == thisenum + UserButtonsTeklaMenu.Back.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserBuyRevitLicense(CallbackQuery query, Message lastmessage, string button)
        {
            var thisenum = typeof(UserButtonsRevitMenu).Name + ".";
            switch (button)
            {
                case string data when data == thisenum + UserButtonsRevitMenu.Back.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserBuyNavisLicense(CallbackQuery query, Message lastmessage, string button)
        {
            var thisenum = typeof(UserButtonsNavisMenu).Name + ".";
            switch (button)
            {
                case string data when data == thisenum + UserButtonsNavisMenu.Back.ToString():
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserBuySpecification(CallbackQuery query, Message lastmessage)
        {
            TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuySpecification(query.From.Id, lastmessage.MessageId);
        }
    }
}
