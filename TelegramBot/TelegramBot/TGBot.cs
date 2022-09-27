using System.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using TelegramBot.Logger;
using TelegramBot.Server;

namespace TelegramBot
{
    public class TGBot
    {
        public TelegramBotClient BotClient { get; } = new TelegramBotClient(ConfigurationManager.AppSettings["TokenTGBot"]);
        public CancellationToken CancellToken { get; } = new CancellationToken();
        private LanguageFunction languageFunction = new LanguageFunction();
        private CallbackHandler callbackHandler = new CallbackHandler();
        private DialogFunction dialogFunction = new DialogFunction();
        private SupportFunction supportFunction = new SupportFunction();
        private ShopFunctions shopFunctions = new ShopFunctions();
        private static TGBot? _myBot;
        private ILogger _logger = BotLogger.GetInstance();
        //TODO сделать проверку последнего сообщения от пользователя in query
        public Dictionary<long, UserInfo> Users { get; set; } = new Dictionary<long, UserInfo>();//TODO сделать при завершении консоли сохранение всех юзеров

        private TGBot()
        {

        }

        public static TGBot MyBot
        {
            get
            {
                if (_myBot == null)
                    _myBot = new TGBot();
                return _myBot;
            }
        }

        public void Launch()
        {
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            BotClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, CancellToken);

            BotClient.SetMyCommandsAsync(GetBotsCommands(), scope: new BotCommandScopeDefault(), cancellationToken: CancellToken);

            var me = BotClient.GetMeAsync().Result;
            _logger.Info("Bot " + me + "started successfully");
            Console.WriteLine($"Start listening for @{me.Username}");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Message != null)
                {
                    if (!Users.ContainsKey(update.Message.From.Id))
                    {
                        ServerAPI.GetInstance.RegisterUser(update.Message.From);
                        Users.Add(update.Message.Chat.Id, new UserInfo(update.Message.From.LanguageCode));
                    }
                }

                Task? handler = update.Type switch
                {
                    UpdateType.Message => BotOnMessageReceived(update.Message!),
                    UpdateType.EditedMessage => BotOnMessageReceived(update.EditedMessage!),
                    UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update),
                    UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery!),
                    UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult!),
                    UpdateType.PreCheckoutQuery => BotOnPreCheckoutQueryReceived(update.PreCheckoutQuery!),
                    UpdateType.ShippingQuery => BotOnShippingQueryReceived(update.ShippingQuery!),
                    _ => null
                };

                await handler;
            }
#pragma warning disable CA1031
            catch (Exception exception)
#pragma warning restore CA1031
            {
                await HandlePollingErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            BotLogger.GetInstance().Error(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task<Message> BotOnMessageReceived(Message message)
        {
            if (message.ReplyToMessage != null && (Users[message.ReplyToMessage.Chat.Id].IsNeedSupport || supportFunction.AdminID.Contains(message.ReplyToMessage.Chat.Id)))
            {
                return await supportFunction.ReplyToUserTheAnswerFromSupport(message);
            }


            switch (message.Text)
            {
                case "/start":
                    {
                        Users[message.From.Id].IsNeedSupport = false;
                        Users[message.From.Id].LastMessage = await dialogFunction.SendHelloMessage(message.Chat.Id);
                        break;
                    }
                case "/language":
                    {
                        Users[message.From.Id].IsNeedSupport = false;
                        Users[message.From.Id].LastMessage = await languageFunction.SendLanguageMessageToUser(message.Chat.Id);
                        break;
                    }
                case "/help":
                    {
                        Users[message.From.Id].LastMessage = await supportFunction.SendSupportMessage(message.From.Id);
                        break;
                    }
                case "/home":
                    {
                        Users[message.From.Id].IsNeedSupport = false;
                        Users[message.From.Id].LastMessage = await dialogFunction.SendHelloMessage(message.Chat.Id);
                        break;
                    }
                case string data when data.ToLower().StartsWith("/send"):
                    {
                        await supportFunction.ReplyToUserTheAnswerFromSupport(message);
                        break;
                    }
                default:
                    {
                        if (Users[message.From.Id].IsNeedSupport)
                        {
                            return await supportFunction.SupportMessageToAdmin(message);
                        }

                        if (message.SuccessfulPayment != null)
                        {
                            return await shopFunctions.SuccessfulPaymentRecived(message.SuccessfulPayment, message.Chat.Id);
                        }

                        if (message.Caption != null)
                        {
                            if (message.Caption.ToLower().StartsWith("/send"))
                            {
                                await supportFunction.ReplyToUserTheAnswerFromSupport(message);
                            }
                        }
                    }
                    break;
            }
            return null;
        }

        private IEnumerable<BotCommand> GetBotsCommands()
        {
            return new List<BotCommand>()
            {
                new BotCommand() { Command = "/help" , Description = "Getting help from bot"},
                new BotCommand() { Command = "/language", Description = "Set the language again"},
                new BotCommand() { Command = "/home", Description = "Go to home page"},
                new BotCommand() { Command = "/start", Description = "Start this Bot"}
            };
        }

        private async Task BotOnCallbackQueryReceived(Update update)
        {
            await callbackHandler.CallbackQueryReceived(update.CallbackQuery!, Users[update.CallbackQuery.From.Id].LastMessage);
        }

        private async Task BotOnInlineQueryReceived(InlineQuery query)
        {

        }

        private async Task BotOnChosenInlineResultReceived(ChosenInlineResult inlineResult)
        {

        }

        private async Task BotOnPreCheckoutQueryReceived(PreCheckoutQuery preCheckoutQuery)
        {
            await shopFunctions.PreCheckoutQueryReceived(preCheckoutQuery);
        }

        private Task BotOnShippingQueryReceived(ShippingQuery shippingQuery)
        {
            return HandlePollingErrorAsync(BotClient, new Exception("No shipping"), CancellToken);
        }

    }
}
