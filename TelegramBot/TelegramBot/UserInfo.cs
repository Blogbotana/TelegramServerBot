using Telegram.Bot.Types;

namespace TelegramBot
{
    public class UserInfo
    {
        public string? LanguageCode { get; set; }
        public bool IsNeedSupport { get; set; }
        public Message? LastMessage { get; set; }

        public UserInfo(string? langCode)
        {
            langCode = LanguageCode;
            IsNeedSupport = false;
            LastMessage = null;
        }
    }
}
