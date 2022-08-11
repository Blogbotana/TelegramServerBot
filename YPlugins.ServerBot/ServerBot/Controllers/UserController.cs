using Microsoft.AspNetCore.Mvc;
using ServerBot.DTO;
using ServerBot.Services;

namespace ServerBot.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private TgUserService userService = TgUserService.GetService();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Create")]
        public bool CreateUser([FromBody] UserDTO tgUser)
        {
            return userService.CreateUser(tgUser);
        }

        //Error: Don't amswer LanguageCode (this is null) 
        [HttpGet("GetByTgId/{tgUserId}")]
        public UserDTOResponse? GetUser(long tgUserId)
        {
            return userService.GetUserByTgId(tgUserId);
        }

        //Error: Don't amswer LanguageCode (this is null) 
        [HttpGet("GetByEmail/{email}")]
        public UserDTOResponse? GetUser(string email)
        {
            return userService.GetUserByEmail(email);
        }
    }
}
