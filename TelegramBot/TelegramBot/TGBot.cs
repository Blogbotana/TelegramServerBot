using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class TGBot
    {
        ITelegramBotClient botClient = null;
        public void Launch()
        {
            botClient = new TelegramBotClient("5470325866:AAGNFZWfs8PxsSjDudFW_x_QGoReSIoPPOc");

            var cts = new CancellationToken();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts);

            var me = botClient.GetMeAsync().Result;
            //Как делать кнопки для бота, набор команд. Сделать просто диалог, 
            Console.WriteLine($"Start listening for @{me.Username}");


            // Send cancellation request to stop bot
            //cts.Cancel();
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;
            
            //var chatId = update.Message.Chat.Id;
            //var messageText = update.Message.Text;

            UserSendCommand(update, cancellationToken);
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

        void UserSendCommand(Update update, CancellationToken cancellationToken)
        {
            switch (update.Message.Text)
            {
                case "/start":
                    {
                        botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, 
                            text: "Приветственное сообщение", 
                            cancellationToken: cancellationToken);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
