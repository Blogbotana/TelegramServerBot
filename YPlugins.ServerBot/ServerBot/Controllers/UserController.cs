﻿using log4net.Config;
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

        public UserController(ILogger logger)
        {

            _logger = logger;
        }

        [HttpPost("Create")]
        public bool CreateUser([FromBody] UserDTO tgUser)
        {
            _logger.Debug("DebugInfo");
            return userService.CreateUser(tgUser);
        }

        //TODO fix Error: Don't amswer LanguageCode (this is null) 
        [HttpGet("GetUserByTG")]
        public UserDTOResponse? GetUser([FromQuery] long tgUserId)
        {
            return userService.GetUserByTgId(tgUserId);
        }

        //TODO fix Error: Don't amswer LanguageCode (this is null) 
        [HttpGet("GetUserByEmail")]
        public UserDTOResponse? GetUser([FromQuery] string email)
        {
            return userService.GetUserByEmail(email);
        }

        [HttpGet("GetUserLanguage")]
        public LanguageDTOResponse? GetUserLanguage([FromQuery] long tgUserId)
        {
            return userService.GetUserLanguageByTgId(tgUserId);
        }

        [HttpPut("SetLangForUserByTgId")]
        public void SetUserLanguage([FromQuery] long tgUserId, [FromBody] LanguageDTO language)
        {
            userService.SetThisLanguageForUser(tgUserId, language);
        }

        [HttpPut("SetLangForUserByEmail")]
        public void SetUserLanguage([FromQuery] string email, [FromBody] LanguageDTO language)
        {
            userService.SetThisLanguageForUser(email, language);
        }

        [HttpPut("BoughtLicenseForYear")]//TODO need to protect from hack
        public void UserBoughtLicenseForYear([FromQuery] long tgUserId, [FromBody] LicenseDTO license)
        {
            userService.ThisUserBoughtThisLicence(tgUserId, license, 365);
        }


        [HttpPut("BoughtLicenseForExactDays")]//TODO need to protect from hack
        public void UserBoughtLicenseForExactDays([FromQuery] long tgUserId, [FromQuery] int days, [FromBody] LicenseDTO license)
        {
            //TODO securety with JWT token
            userService.ThisUserBoughtThisLicence(tgUserId, license, days);
        }

        [HttpPut("SetdataByTgId")]
        public void SetThisEmailAndNameForUser([FromQuery] long tgUserId, [FromQuery] string email, [FromQuery] string name)
        {
            userService.SetEmailAndNameForTgUser(tgUserId, email, name);
        }

        [HttpPut("SetdataByEmail")]
        public void SetThisEmailAndNameForUser([FromQuery] string email, [FromQuery] long tgUserId, [FromQuery] string name)
        {
            userService.SetTgIdAndNameForEmailUser( email, tgUserId, name);
        }
    }
}
