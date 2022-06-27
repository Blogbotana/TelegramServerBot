using System.Text;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class SupportFunction
    {
        public long[] AdminID { get; } = new long[] { 509537294 };

        public async Task<Message> ReplyToUserSupport(long IdChat)
        {
            TGBot.MyBot.IsGetMessagesAsSupport = true;

            return await TGBot.MyBot.BotClient.SendTextMessageAsync(IdChat, 
                text: Localization.GetTranslation("Опишите вашу проблему подробно и если обязательно, прикрепите скрины ошибок, без них будет сложно вам помочь"),
                cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditSupportMessage(long IdChat, int msgId)
        {
            TGBot.MyBot.IsGetMessagesAsSupport = true;

            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, 
                text: Localization.GetTranslation("Опишите вашу проблему подробно и если обязательно, прикрепите скрины ошибок, без них будет сложно вам помочь"),
                cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async void SupportMessageToAdmin(Message message)
        {
            if (AdminID.Contains(message.Chat.Id))
                return;

            string introduction = "Было получено обращение от\n" + message.Chat.FirstName + " " + message.Chat.LastName + " "
                + " @" + message.Chat.Username + " " + message.Chat.Id;


            foreach (long chatIdAdmin in AdminID)
            {
                await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: chatIdAdmin, text: introduction, entities: GetMessageEntity(introduction),
                    cancellationToken: TGBot.MyBot.CancellToken);

                await TGBot.MyBot.BotClient.ForwardMessageAsync(chatIdAdmin, message.Chat.Id, message.MessageId,
                    cancellationToken: TGBot.MyBot.CancellToken);
            }
            Encoding encoding = Encoding.UTF8;
            var array = encoding.GetBytes(Localization.GetTranslation("Спасибо"));
            string thanks = Encoding.UTF8.GetString(array).ToLower();
            string msg = message.Text.ToString().ToLower();

            if (msg.Contains(thanks))
            {
                await CloseSupport(message);
            }
        }

        private IEnumerable<MessageEntity> GetMessageEntity(string text)
        {
            List<MessageEntity> entities = new List<MessageEntity>();
            MessageEntity entity = new MessageEntity();
            entity.Type = MessageEntityType.Italic;
            entity.Length = text.Length;
            entities.Add(entity);
            return entities;
        }

        public async Task<Message> ReplyToUserTheAnswerFromSupport(Message message)
        {
            if (message.Text.ToLower().StartsWith("/close"))
            {
                return await CloseSupport(message);
            }
            if (message.Text.ToLower().StartsWith("/send"))//TODO обработать ответ от пользователя, которому нельзя пересылать сообщения
            {
                return null;
            }
            else
            {
                if (message.ReplyToMessage.ForwardFrom == null)//TODO сделать чтобы работало с контентом типа картинок и стикеров
                    return await TGBot.MyBot.BotClient.SendTextMessageAsync(message.From.Id, "Ошибка произошла");
                else
                    return await TGBot.MyBot.BotClient.SendTextMessageAsync(message.ReplyToMessage.ForwardFrom.Id, message.Text);
            }

        }

        public async Task<Message> CloseSupport(Message message)
        {
            long chatId = 0;
            TGBot.MyBot.IsGetMessagesAsSupport = false;

            //if(message.ReplyToMessage != null)
            //{
            //    chatId = message.ReplyToMessage.ForwardFrom.Id;
            //}
            //else
                chatId = message.Chat.Id;

            foreach (long chatIdAdmin in AdminID)
            {
                string adminmsg = "Обращение закрыто для " + chatId;
                await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: chatIdAdmin, text: adminmsg, entities: GetMessageEntity(adminmsg),
                    cancellationToken: TGBot.MyBot.CancellToken);
            }

            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId, text: "Ваше обращение закрыто, спасибо ☺️",
                    cancellationToken: TGBot.MyBot.CancellToken);
        }
    }
}
