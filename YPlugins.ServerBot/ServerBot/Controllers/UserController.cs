using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerBot.DTO;
using ServerBot.DTO.Response;
using ServerBot.Services;
using System.Data;

namespace ServerBot.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController
    {
        private readonly ILogger _logger;
        private TgUserService userService = TgUserService.GetService();
        private JWTTokenService jwtTokenService = JWTTokenService.GetService();

        public UserController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpPost("Create")]
        public bool CreateUser([FromBody] UserDTO tgUser)
        {
            try
            {
                return userService.CreateUser(tgUser);
            }
            catch (Exception e1)
            {
                _logger.Error("Error CreateUser", e1);
                return false;
            }
        }
        [HttpGet("jwtToken")]
        public string GetToken([FromBody] UserDTO tgUser)
        {
            return jwtTokenService.GenerateToken(tgUser);
        }
        //TODO fix Error: Don't amswer LanguageCode (this is null) 
        [HttpGet("GetUserByTG")]
        public UserDTOResponse? GetUser([FromQuery] long tgUserId)
        {
            try
            {
                return userService.GetUserByTgId(tgUserId);
            }
            catch (Exception e1)
            {
                _logger.Error("Error GetUserByTG?tgUserId=" + tgUserId.ToString(), e1);
                return null;
            }
            
        }

        //TODO fix Error: Don't amswer LanguageCode (this is null) 
        [HttpGet("GetUserByEmail")]
        public UserDTOResponse? GetUser([FromQuery] string email)
        {
            try
            {
                return userService.GetUserByEmail(email);
            }
            catch (Exception e1)
            {
                _logger.Error("Error GetUserByEmail?email=" + email, e1);
                return null;
            }
        }

        [HttpPut("SetLangForUserByTgId")]
        public void SetUserLanguage([FromQuery] long tgUserId, [FromBody] LanguageDTO language)
        {
            try
            {
                userService.SetThisLanguageForUser(tgUserId, language);
            }
            catch (Exception e1)
            {
                _logger.Error("Error SetLangForUserByTgId?tgUserId=" + tgUserId.ToString(), e1);
            }
        }

        [HttpPut("SetLangForUserByEmail")]
        public void SetUserLanguage([FromQuery] string email, [FromBody] LanguageDTO language)
        {
            try
            {
                userService.SetThisLanguageForUser(email, language);
            }
            catch (Exception e1)
            {
                _logger.Error("Error SetLangForUserByEmail?email=" + email, e1);
            }
        }

        [HttpPut("BoughtLicenseForYear")]
        [Authorize]
        public void UserBoughtLicenseForYear([FromQuery] long tgUserId, [FromBody] LicenseDTO license)
        {
            try
            {
                userService.ThisUserBoughtThisLicence(tgUserId, license, 365);
            }
            catch (Exception e1)
            {
                _logger.Error("Error BoughtLicenseForYear?tgUserId=" + tgUserId.ToString(), e1);
            }
        }


        [HttpPut("BoughtLicenseForExactDays")]
        [Authorize]
        public void UserBoughtLicenseForExactDays([FromQuery] long tgUserId, [FromQuery] int days, [FromBody] LicenseDTO license)
        {
            try
            {
                if (days > 0)
                    userService.ThisUserBoughtThisLicence(tgUserId, license, days);
            }
            catch (Exception e1)
            {
                _logger.Error("Error BoughtLicenseForExactDays?tgUserId=" + tgUserId.ToString(), e1);
            }
        }

        [HttpPut("SetdataByTgId")]
        public void SetThisEmailAndNameForUser([FromQuery] long tgUserId, [FromQuery] string email, [FromQuery] string name)
        {
            try
            {
                userService.SetEmailAndNameForTgUser(tgUserId, email, name);
            }
            catch (Exception e1)
            {
                _logger.Error("Error SetdataByTgId?tgUserId=" + tgUserId.ToString(), e1);
            }
            
        }

        [HttpPut("SetdataByEmail")]
        public void SetThisEmailAndNameForUser([FromQuery] string email, [FromQuery] long tgUserId, [FromQuery] string name)
        {
            try
            {
                userService.SetTgIdAndNameForEmailUser(email, tgUserId, name);
            }
            catch (Exception e1)
            {
                _logger.Error("Error SetdataByEmail?email=" + email, e1);
            }
            
        }
    }
}
