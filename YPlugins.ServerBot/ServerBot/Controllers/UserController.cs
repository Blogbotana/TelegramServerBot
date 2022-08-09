using Microsoft.AspNetCore.Mvc;
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
    }
}
