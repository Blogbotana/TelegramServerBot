using Microsoft.AspNetCore.Mvc;
using ServerBot.DTO;
using ServerBot.Services;

namespace ServerBot.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private TgUserService userService = TgUserService.GetService();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("user/create")]
        public bool CreateUser([FromBody] UserDTO tgUser)
        {
            return userService.CreateUser(tgUser);
        }
    }
}
