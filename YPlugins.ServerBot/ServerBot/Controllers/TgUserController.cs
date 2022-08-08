using Microsoft.AspNetCore.Mvc;
using ServerBot.Entities;
using ServerBot.Services;

namespace ServerBot.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TgUserController : ControllerBase
    {
        private TgUserService UserService = new TgUserService();
        private LanguageService LanguageService = new LanguageService();    

        [HttpGet(Name = "user/{tgId}")]
        public TgUserEntity GetUser(long tgId)
        {
            return UserService.GetTgUser(tgId);
        }

        [HttpPost (Name = "user/create")]
        public bool CreateUser([FromBody] TgUserEntity tgUser)
        {
            Languages languages = LanguageService.GetLanguagebyCode(tgUser.Language.Code);
            tgUser.Language = languages;
            return UserService.CreateUser(tgUser) != null;
        }
    }
}
