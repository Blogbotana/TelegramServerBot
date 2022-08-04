using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class TGBot
    {
        public TelegramBotClient BotClient { get; } = new TelegramBotClient(System.IO.File.ReadAllText("token.txt"));
        public CancellationToken CancellToken { get; } = new CancellationToken();
        private LanguageFunction languageFunction = new LanguageFunction();
        private CallbackHandler callbackHandler = new CallbackHandler();
        private DialogFunction dialogFunction = new DialogFunction();
        private SupportFunction supportFunction = new SupportFunction();
        private ShopFunctions shopFunctions = new ShopFunctions();
        private static TGBot? _myBot;

        public Dictionary<long, bool> IsGetMessagesAsSupport { get; set; } = new Dictionary<long, bool>();
        public Dictionary<long, string> UserLanguage { get; set; } = new Dictionary<long, string>();

        public Dictionary<long, Message> LastMessageFromBot { get; set; } = new Dictionary<long, Message>();

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
            //private set { }
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
            Console.WriteLine($"Start listening for @{me.Username}");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                if (!LastMessageFromBot.ContainsKey(update.Message.From.Id))
                    LastMessageFromBot.Add(update.Message.Chat.Id, null);

                if (!IsGetMessagesAsSupport.ContainsKey(update.Message.From.Id))
                    IsGetMessagesAsSupport.Add(update.Message.Chat.Id, false);
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

            try
            {
                await handler;
            }
#pragma warning disable CA1031
            catch (Exception exception)
#pragma warning restore CA1031
            {
                await HandlePollingErrorAsync(botClient, exception, cancellationToken);
            }
            // Only process Message updates: https://core.telegram.org/bots/api#message
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task BotOnMessageReceived(Message message)
        {
            if(message.ReplyToMessage != null && (IsGetMessagesAsSupport[message.ReplyToMessage.Chat.Id] || supportFunction.AdminID.Contains(message.ReplyToMessage.Chat.Id)))
            {
                await supportFunction.ReplyToUserTheAnswerFromSupport(message);
            }


            switch (message.Text)
            {
                case "/start":
                    {
                        IsGetMessagesAsSupport[message.From.Id] = false;
                        LastMessageFromBot[message.From.Id] = await dialogFunction.SendHelloMessage(message.Chat.Id);
                        break;
                    }
                case "/language":
                    {
                        IsGetMessagesAsSupport[message.From.Id] = false;
                        LastMessageFromBot[message.From.Id] = await languageFunction.SendLanguageMessageToUser(message.Chat.Id);
                        break;
                    }
                case "/help":
                    {
                        LastMessageFromBot[message.From.Id] = await supportFunction.SendSupportMessage(message.From.Id);
                        break;
                    }
                case "/home":
                    {
                        IsGetMessagesAsSupport[message.From.Id] = false;
                        LastMessageFromBot[message.From.Id] = await dialogFunction.SendHelloMessage(message.Chat.Id);
                        break;
                    }
                default:
                    {
                        if (IsGetMessagesAsSupport[message.From.Id])
                        {
                            await supportFunction.SupportMessageToAdmin(message);
                        }

                        if(message.SuccessfulPayment != null)
                        {
                            await shopFunctions.SuccessfulPaymentRecived(message.Chat.Id);
                        }
                    }
                    break;
            }
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
            await callbackHandler.CallbackQueryReceived(update.CallbackQuery!, LastMessageFromBot[update.CallbackQuery.From.Id]);
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
            return HandlePollingErrorAsync(BotClient, new Exception(",kf"), CancellToken);
        }

    }
}
