using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Server;

namespace TelegramBot
{
    public class CallbackHandler
    {
        DialogFunction dialog = new DialogFunction();
        SupportFunction support = new SupportFunction();
        ShopFunctions _shopFunctions = new ShopFunctions();
        public async Task CallbackQueryReceived(CallbackQuery query, Message lastmessage)
        {
            switch (query.Data)
            {
                case string language when language.StartsWith("Lang_"):
                    {
                        await ServerAPI.GetInstance.SetThisLanguageForUser(query.From.Id, language.Replace("Lang_", ""));
                        await TGBot.MyBot.BotClient.DeleteMessageAsync(query.From.Id, lastmessage.MessageId, TGBot.MyBot.CancellToken);
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.SendHelloMessage(query.From.Id);

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
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await support.EditSupportMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string result when result == thisenum + UserButtonsMainMenu.DataBase.ToString():
                    {
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditCustomMessage(query.From.Id, lastmessage.MessageId, "Раздел в разработке");
                        break;
                    }
                case string result when result == thisenum + UserButtonsMainMenu.UserButtonsBuyLicenseMenu.ToString():
                    {
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
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
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyTeklaLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.Revit.ToString():
                    {
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyRevitLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.Navis.ToString():
                    {
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyNavisLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                case string data when data == thisenum + UserButtonsBuyLicenseMenu.Back.ToString():
                    {
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditHelloMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private async Task ReplyToUserBuyTeklaLicense(CallbackQuery query, Message lastmessage, string button)
        {
            var data = FromStringToTeklaMenuEmun(button);
            if(data == UserButtonsTeklaMenu.Back)
            {
                TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
            }
            else
            {
                TGBot.MyBot.Users[query.From.Id].LastMessage = await _shopFunctions.BuyLisence(query.From.Id, data);
            }

        }

        private async Task ReplyToUserBuyRevitLicense(CallbackQuery query, Message lastmessage, string button)
        {
            var thisenum = typeof(UserButtonsRevitMenu).Name + ".";
            switch (button)
            {
                case string data when data == thisenum + UserButtonsRevitMenu.Back.ToString():
                    {
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
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
                        TGBot.MyBot.Users[query.From.Id].LastMessage = await dialog.EditBuyLicenseMessage(query.From.Id, lastmessage.MessageId);
                        break;
                    }
                default:
                    break;
            }
        }

        private UserButtonsTeklaMenu FromStringToTeklaMenuEmun(string button)
        {
            var thisenum = typeof(UserButtonsTeklaMenu).Name + ".";
            switch (button)
            {
                case string data when data == thisenum + UserButtonsTeklaMenu.ProfileChooser.ToString():
                    {
                        return UserButtonsTeklaMenu.ProfileChooser;
                    }
                case string data when data == thisenum + UserButtonsTeklaMenu.SteelSpecification.ToString():
                    {
                        return UserButtonsTeklaMenu.SteelSpecification;
                    }
                case string data when data == thisenum + UserButtonsTeklaMenu.ExcelReportGenerator.ToString():
                    {
                        return UserButtonsTeklaMenu.ExcelReportGenerator;
                    }
                case string data when data == thisenum + UserButtonsTeklaMenu.Back.ToString():
                    {
                        return UserButtonsTeklaMenu.Back;
                    }
                default:
                    return UserButtonsTeklaMenu.Back;
            }
        }
    }
}
