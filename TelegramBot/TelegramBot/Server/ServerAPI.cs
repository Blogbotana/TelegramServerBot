using Newtonsoft.Json;
using ServerBot.DTO;
using ServerBot.DTO.Response;
using System.Configuration;
using Telegram.Bot.Types;
using TelegramBot.DTO.Response;
using TelegramBot.Logger;

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
                    BotLogger.GetInstance().Fatal(e1);
                }
                return instance;
            }
        }

        public async Task AuthorizeBot()
        {
            PasswordDTO password = new PasswordDTO { Password = ConfigurationManager.AppSettings["Password"] };
            var response = await HTTP.GetInstance.GET(serverAddress + "User/jwtToken", password);
            HTTP.SetJWTToken(response);
            await Task.CompletedTask;
        }

        public void RegisterUser(User userfromTG)
        {
            try
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
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("RegisterUser", e1);
            }
        }

        public async Task<UserDTOResponse> GetUserByTgId(long Id)
        {
            try
            {
                var responce = await HTTP.GetInstance.GET(serverAddress + $"User/GetUserByTG?tgUserId={Id}");
                return JsonConvert.DeserializeObject<UserDTOResponse>(responce);
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("GetUserByTgId Id=" + Id.ToString(), e1);
                throw e1;
            }
        }

        public async Task<LanguageDTOResponse> GetUserLanguage(long Id)
        {
            try
            {
                var responce = await HTTP.GetInstance.GET(serverAddress + $"User/GetUserByTG?tgUserId={Id}");
                return JsonConvert.DeserializeObject<LanguageDTOResponse>(responce);
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("GetUserLanguage Id=" + Id.ToString(), e1);
                throw e1;
            }
        }

        public async Task SetThisLanguageForUser(long Id, string Code)
        {
            try
            {
                LanguageDTO lang = new LanguageDTO()
                {
                    IETF_LanguageTag = Code
                };
                await HTTP.GetInstance.PUT(serverAddress + $"User/SetLangForUserByTgId?tgUserId={Id}", lang);
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("SetThisLanguageForUser Id=" + Id.ToString(), e1);
                throw e1;
            }
        }

        public async Task<HttpResponseMessage> UserBoughtLicenseForYear(string name, long tgUserId)
        {
            try
            {
                LicenseDTO license = new LicenseDTO()
                {
                    Name = name
                };
                return await HTTP.GetInstance.PUT(serverAddress + $"User/BoughtLicenseForYear?tgUserId={tgUserId}", license);
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("UserBoughtLicenseForYear tgUserId=" + tgUserId.ToString(), e1);
                return null;
            }
        }
        public async Task<HttpResponseMessage> UserBoughtLicenseForExactDays(string name, long tgUserId, int days)
        {
            try
            {
                LicenseDTO license = new LicenseDTO()
                {
                    Name = name
                };
                return await HTTP.GetInstance.PUT(serverAddress + $"User/BoughtLicenseForExactDays?tgUserId={tgUserId}&days={days}", license);
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("UserBoughtLicenseForExactDays tgUserId=" + tgUserId.ToString(), e1);
                return null;
            }

        }

        public async Task<IEnumerable<LicenseDTOResponse>> GetAllLicensesOfUser(long Id)
        {
            try
            {
                UserDTOResponse user = await GetUserByTgId(Id);
                return user.Licenses;
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("GetAllLicensesOfUser Id=" + Id.ToString(), e1);
                return null;
            }
        }

        public async Task<HttpResponseMessage> SetEmailAndNameForUser(long tgId, string? email, string? name)
        {
            try
            {
                if (email == null || name == null)
                    return null;
                return await HTTP.GetInstance.PUT(serverAddress + $"User/SetdataByTgId?tgUserId={tgId}&email={email}&name={name}");
            }
            catch (Exception e1)
            {
                BotLogger.GetInstance().Error("SetEmailAndNameForUser tgId=" + tgId.ToString(), e1);
                return null;
            }
        }
    }
}
