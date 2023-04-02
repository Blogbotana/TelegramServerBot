using Telegram.Bot.Types;

namespace TelegramBot
{
    public class UserInfo
    {
        public string LanguageCode { get; set; }
        public bool IsNeedSupport { get; set; }
        public Message? LastMessage { get; set; }

        public UserInfo(string? langCode)
        {
            if (langCode is null)
                LanguageCode = "en";
            else
                LanguageCode = langCode;

            IsNeedSupport = false;
            LastMessage = null;
        }
    }
}
