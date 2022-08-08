using ServerBot.Entities;
using ServerBot.Repositories;

namespace ServerBot.Services
{
    public class LanguageService
    {
        public Languages GetLanguagebyCode(string Code)
        {
            return LanguageRepository.GetLanguageByCode(Code);
        }
    }
}
