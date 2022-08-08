using Microsoft.AspNetCore.Mvc;
using ServerBot.DTO.Response;
using ServerBot.Entities;
using ServerBot.Services;

namespace ServerBot.Controllers
{

    [ApiController]
    [Route("/")]
    public class TgUserController : ControllerBase
    {
        private TgUserService UserService = new TgUserService();
        private LanguageService LanguageService = new LanguageService();
        
        [HttpGet]
        [Route("user/")]
        public UserResponseDTO GetUser([FromQuery]long tgId)
        {
            UserResponseDTO response = UserService.GetTgUser(tgId);
            if (response == null)
            {
                this.HttpContext.Response.StatusCode = 418;
            }
            return response;
        }

        [HttpPost]
        [Route("user/create/")]
        public bool CreateUser([FromBody] TgUserEntity tgUser)
        {
            Languages languages = LanguageService.GetLanguagebyCode(tgUser.Language.Code);
            tgUser.Language = languages;
            return UserService.CreateUser(tgUser) != null;
        }
    }
}
