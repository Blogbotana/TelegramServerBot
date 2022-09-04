using Newtonsoft.Json;
using ServerBot.DTO;
using ServerBot.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.DTO.Response;

namespace TelegramBot.Server
{
    public class ServerAPI
    {
        private const string serverAddress = "http://localhost:5007/";

        private static ServerAPI instance;
        private static object SyncObject = new object();
        private ServerAPI()
        {

        }

        public static ServerAPI GetInstance
        {
            get
            {
                try
                {
                    if (instance == null)
                        lock (SyncObject)
                        {
                            if (instance == null)
                                instance = new ServerAPI();
                        }
                    return instance;
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.ToString());
                }
                return instance;
            }
        }

        public void RegisterUser(User userfromTG)
        {
            UserDTO userDTO = new UserDTO()
            {
                ComputerInformation = null,
                Email = null,
                FirstName = userfromTG.FirstName,
                LastName = userfromTG.LastName,
                LanguageCode = userfromTG.LanguageCode,
                TgId = userfromTG.Id
            };

            var responce = HTTP.GetInstance.POST(serverAddress + "User/Create", userDTO);
        }

        public async Task<UserDTOResponse> GetUserByTgId(long Id)
        {
            var responce = await HTTP.GetInstance.GET(serverAddress + $"User/GetUserByTG?tgUserId={Id}");
            return JsonConvert.DeserializeObject<UserDTOResponse>(responce);
        } 

        public async Task<LanguageDTOResponse> GetUserLanguage(long Id)
        {
            var responce = await HTTP.GetInstance.GET(serverAddress + $"User/GetUserByTG?tgUserId={Id}");
            return JsonConvert.DeserializeObject<LanguageDTOResponse>(responce);
        }

        public async Task SetThisLanguageForUser(long Id, string Code)
        {
            LanguageDTO lang = new LanguageDTO()
            {
                IETF_LanguageTag = Code
            };
            await HTTP.GetInstance.PUT(serverAddress + $"User/SetLangForUserByTgId?tgUserId={Id}", lang);
        }

        public async Task UserBoughtLicenseForYear(string name, long tgUserId)
        {
            LicenseDTO license = new LicenseDTO()
            {
                Name = name
            };
            await HTTP.GetInstance.PUT(serverAddress + $"User/BoughtLicenseForYear?tgUserId={tgUserId}", license);
        }
        public async Task UserBoughtLicenseForExactDays(string name, long tgUserId, int days)
        {
            LicenseDTO license = new LicenseDTO()
            {
                Name = name
            };
            await HTTP.GetInstance.PUT(serverAddress + $"User/BoughtLicenseForExactDays?tgUserId={tgUserId}&days={days}", license);
        }

        public async Task<IEnumerable<LicenseDTOResponse>> GetAllLicensesOfUser(long Id)
        {
            UserDTOResponse user = await GetUserByTgId(Id);
            return user.Licenses;
        }

        public async Task SetEmailAndNameForUser(long tgId, string? email, string? name)
        {
            if(email == null || name == null)
                return;
            await HTTP.GetInstance.PUT(serverAddress + $"User/SetdataByTgId?tgUserId={tgId}&email={email}&name={name}");
        }
    }
}
