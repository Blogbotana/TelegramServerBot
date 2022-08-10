using System.ComponentModel;

namespace ServerBot.Entities
{
    public class LanguageEntity
    {
        public int Id { get; set; }
        public string IETF_LanguageTag { get; set; }
        public string EnglishName { get; set; }
    }
}
