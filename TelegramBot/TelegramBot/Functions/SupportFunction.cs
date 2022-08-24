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
            TGBot.MyBot.IsGetMessagesAsSupport[IdChat] = true;

            return await TGBot.MyBot.BotClient.SendTextMessageAsync(IdChat, 
                text: Localization.GetTranslation("Опишите вашу проблему подробно и если обязательно, прикрепите скрины ошибок, без них будет сложно вам помочь"),
                cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> EditSupportMessage(long IdChat, int msgId)
        {
            TGBot.MyBot.IsGetMessagesAsSupport[IdChat] = true;//dgfsdfd Не работает TODO

            return await TGBot.MyBot.BotClient.EditMessageTextAsync(IdChat, msgId, 
                text: Localization.GetTranslation("Опишите вашу проблему подробно и если обязательно, прикрепите скрины ошибок, без них будет сложно вам помочь"),
                cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> SendSupportMessage(long IdChat)
        {
            TGBot.MyBot.IsGetMessagesAsSupport[IdChat] = true;

            return await TGBot.MyBot.BotClient.SendTextMessageAsync(IdChat,
                text: Localization.GetTranslation("Опишите вашу проблему подробно и если обязательно, прикрепите скрины ошибок, без них будет сложно вам помочь"),
                cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> SupportMessageToAdmin(Message message)
        {
            if (AdminID.Contains(message.Chat.Id))
                return message;

            string introduction = "Bot: Было получено обращение от\n" + message.Chat.FirstName + " " + message.Chat.LastName + " "
                + " @" + message.Chat.Username + " " + message.Chat.Id;


            foreach (long chatIdAdmin in AdminID)
            {
                await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: chatIdAdmin, text: introduction, entities: GetMessageEntity(introduction),
                    cancellationToken: TGBot.MyBot.CancellToken);

                await TGBot.MyBot.BotClient.ForwardMessageAsync(chatIdAdmin, message.Chat.Id, message.MessageId,
                    cancellationToken: TGBot.MyBot.CancellToken);
            }

            if(message.Text != null)
            {
                Encoding encoding = Encoding.UTF8;
                var array = encoding.GetBytes(Localization.GetTranslation("Спасибо"));
                string thanks = Encoding.UTF8.GetString(array).ToLower();
                string msg = message.Text.ToString().ToLower();

                if (msg.Contains(thanks))
                {
                   return await CloseSupport(message);
                }
            }
            return message;
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
            if(message.Text != null)
            {
                if (message.Text.ToLower().StartsWith("/close"))
                {
                    return await CloseSupport(message);
                }
                else
                {
                    return await ReplyToUser(message);
                }
            }
            else
            {
                return await ReplyToUser(message);
            }
 

        }

        public async Task<Message> CloseSupport(Message message)
        {
            TGBot.MyBot.IsGetMessagesAsSupport[message.Chat.Id] = false;

            long chatId = message.Chat.Id;

            foreach (long chatIdAdmin in AdminID)
            {
                string adminmsg = "Обращение закрыто для " + chatId;
                await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId: chatIdAdmin, text: adminmsg, entities: GetMessageEntity(adminmsg),
                    cancellationToken: TGBot.MyBot.CancellToken);
            }

            return await TGBot.MyBot.BotClient.SendTextMessageAsync(chatId, text: "Ваше обращение закрыто, спасибо ☺️",
                    cancellationToken: TGBot.MyBot.CancellToken);
        }

        public async Task<Message> ReplyToUser(Message message)
        {
            long id = GetIdOfUser(ref message);
            if (id == 0)
            {
                if (AdminID.Contains(message.From.Id))
                    return await TGBot.MyBot.BotClient.SendTextMessageAsync(message.From.Id, "Ошибка произошла, укажите ID пользователя");
                else
                    return await SupportMessageToAdmin(message);
            }
                
            
            if (message.Photo != null)
            {
                PhotoSize[] photo = message.Photo;
                InputOnlineFile file = new InputOnlineFile(photo[0].FileId);
                return await TGBot.MyBot.BotClient.SendPhotoAsync(id, file, message.Caption, cancellationToken: TGBot.MyBot.CancellToken);
            }
            if (message.Sticker != null)
            {
                InputOnlineFile striker = new InputOnlineFile(message.Sticker.FileId);
                return await TGBot.MyBot.BotClient.SendStickerAsync(id, striker, cancellationToken: TGBot.MyBot.CancellToken);
            }
            if (message.Audio != null)
            {
                InputOnlineFile audio = new InputOnlineFile(message.Audio.FileId);
                return await TGBot.MyBot.BotClient.SendAudioAsync(id, audio, cancellationToken: TGBot.MyBot.CancellToken);
            }
            if (message.Animation != null)
            {
                InputOnlineFile animation = new InputOnlineFile(message.Animation.FileId);
                return await TGBot.MyBot.BotClient.SendAnimationAsync(id, animation, cancellationToken: TGBot.MyBot.CancellToken);
            }
            if (message.Voice != null)
            {
                InputOnlineFile voice = new InputOnlineFile(message.Voice.FileId);
                return await TGBot.MyBot.BotClient.SendVoiceAsync(id, voice, cancellationToken: TGBot.MyBot.CancellToken);
            }
            if (message.Document != null)
            {
                InputOnlineFile document = new InputOnlineFile(message.Document.FileId);
                return await TGBot.MyBot.BotClient.SendDocumentAsync(id, document, cancellationToken: TGBot.MyBot.CancellToken);
            }
            if (message.Text != null)
                return await TGBot.MyBot.BotClient.SendTextMessageAsync(id, message.Text);

            return null;
        }

        private long GetIdOfUser(ref Message message)
        {
            if(message.ReplyToMessage != null)//Если админ отвечает пользователю
            {
                if (message.ReplyToMessage.ForwardFrom == null)//Если пользователь запретил пересылку сообщений
                {
                    if(message.ReplyToMessage.Text != null)
                    {
                        if (message.ReplyToMessage.Text.ToLower().StartsWith("bot:"))
                        {
                            string[] texts = message.ReplyToMessage.Text.Split(' ');
                            string idofuser = texts[texts.Length - 1];
                            long Id1;
                            if (long.TryParse(idofuser, out Id1))
                            {
                                return Id1;
                            }
                        }
                    }
                    return default(long);
                }
                else
                    return message.ReplyToMessage.ForwardFrom.Id;
            }
            else
            {
                string messageText = message.Text;
                if (messageText == null)
                {
                    messageText = message.Caption;
                }

                if (messageText.ToLower().StartsWith("/send"))
                {
                    string[] splited = messageText.Split(' ');
                    if (splited.Length > 2)
                    {
                        long chatid;
                        if (long.TryParse(splited[1], out chatid))
                        {
                            var enumerablesplit = splited.ToList();
                            enumerablesplit.RemoveAt(0);
                            enumerablesplit.RemoveAt(0);
                            messageText = String.Join(' ', enumerablesplit);

                            if (message.Text != null)
                                message.Text = messageText;
                            else if (message.Caption != null)
                                message.Caption = messageText;

                            return chatid;
                        }
                    }
                    return default(long);
                }
            }
            return default(long);
        }
    }
}
