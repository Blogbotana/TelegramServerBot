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
                case string button when button.StartsWith("UserButtonsMainMenu"):
                    {
                        await ReplyToUserButtonsMainMenu(query,lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith("UserButtonsBuyLicenseMenu"):
                    {
                        await ReplyToUserButtonsBuyLicenseMenu(query,lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith("UserButtonsTeklaMenu"):
                    {
                        await ReplyToUserBuyTeklaLicense(query, lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith("UserButtonsNavisMenu"):
                    {
                        await ReplyToUserBuyNavisLicense(query, lastmessage, button);
                        break;
                    }
                case string button when button.StartsWith("UserButtonsRevitMenu"):
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
            string enum1 = "UserButtonsMainMenu.";
            switch (button)
            {
                case string result when result == enum1 + "Support":
                    {
                        //await TGBot.MyBot.BotClient.DeleteMessageAsync(query.From.Id, lastmessage.MessageId, TGBot.MyBot.CancellToket);
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await support.EditSupportMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string result when result == enum1 + "DataBase":
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditCustomMessage(query.From.Id, lastmessage.MessageId, "Раздел в разработке");
                        break;
                    }
                case string result when result == enum1 + "UserButtonsBuyLicenseMenu":
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
            string enum1 = "UserButtonsBuyLicenseMenu.";
            switch (button)
            {
                case string data when data == enum1 + "TeklaStructures":
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyTeklaLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == enum1 + "Revit":
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyRevitLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == enum1 + "Navis":
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyNavisLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == enum1 + "Back":
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
            string enum1 = "UserButtonsTeklaMenu.";
            switch (button)
            {
                case string data when data == enum1 + "ProfileChooser":
                    {

                        break;
                    }
                case string data when data == enum1 + "SteelSpecification":
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuySpecification(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == enum1 + "ExcelReportGenerator":
                    {

                        break;
                    }
                case string data when data == enum1 + "Back":
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
            string enum1 = "UserButtonsRevitMenu.";
            switch (button)
            {
                case string data when data == enum1 + "Back":
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
            string enum1 = "UserButtonsNavisMenu.";
            switch (button)
            {
                case string data when data == enum1 + "Back":
                    {
                        TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserBuySpecification(CallbackQuery query, Message lastmessage, string button)
        {
            TGBot.MyBot.LastMessageFromBot[query.From.Id] = await dialog.EditBuySpecification(query.From.Id, lastmessage.MessageId);
        }
    }
}
