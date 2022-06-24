using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class TGBot
    {
        public TelegramBotClient BotClient { get; } = new TelegramBotClient("5470325866:AAGNFZWfs8PxsSjDudFW_x_QGoReSIoPPOc");
        public CancellationToken CancellToket { get; } = new CancellationToken();
        private LanguageFunction languageFunction = new LanguageFunction();
        private CallbackHandler callbackHandler = new CallbackHandler();
        private static TGBot? _myBot;

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

            BotClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, CancellToket);

            BotClient.SetMyCommandsAsync(GetBotsCommands(), scope: new BotCommandScopeDefault(), cancellationToken: CancellToket);

            

            var me = BotClient.GetMeAsync().Result;
            //Как делать кнопки для бота, набор команд. Сделать просто диалог, 
            Console.WriteLine($"Start listening for @{me.Username}");


            
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                UpdateType.Message => BotOnMessageReceived(update.Message!),
                UpdateType.EditedMessage => BotOnMessageReceived(update.EditedMessage!),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery!),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery!),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult!),
                _ => null
                //_ => UnknownUpdateHandlerAsync(botClient, update)
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
            //if (update.Type != UpdateType.Message)
            //    return;
            //// Only process text messages
            //if (update.Message!.Type != MessageType.Text)
            //    return;

            //var chatId = update.Message.Chat.Id;
            //var messageText = update.Message.Text;
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
            
            switch (message.Text)
            {
                case "/start":
                    {
                        await languageFunction.SendLanguageMessageToUser(message.Chat.Id);  
                        break;
                    }
                case "/language":
                    {
                        break;
                    }
                case "/help":
                    {
                        break;
                    }
                default:
                    break;
            }
        }

        async Task SendTextMessageToUser(ChatId chatid, string Text)
        {
            await BotClient.SendTextMessageAsync(chatId: chatid,
                              text: Text,
                              cancellationToken: CancellToket);
        }



        private IEnumerable<BotCommand> GetBotsCommands()
        {
            return new List<BotCommand>()
            {
                new BotCommand() { Command = "/help" , Description = "Getting help from bot"},
                new BotCommand() { Command = "/language", Description = "Set the language again"}
            };
        }

        private async Task BotOnCallbackQueryReceived(CallbackQuery query)
        {
            await callbackHandler.CallbackQueryReceived(query);
        }

        private async Task BotOnInlineQueryReceived(InlineQuery query)
        {

        }

        private async Task BotOnChosenInlineResultReceived(ChosenInlineResult inlineResult)
        {

        }
    }
}
