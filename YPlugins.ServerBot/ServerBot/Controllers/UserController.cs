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

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "user/{tgId}")]
        //public UserDTO GetUser(long tgId)
        //{
        //    return TgUserService.CreateUser(tgId);
        //}

        [HttpPost("user/create")]
        public bool CreateUser([FromBody] UserDTO tgUser)
        {
            return TgUserService.CreateUser(tgUser) == 1;
        }
    }
}
