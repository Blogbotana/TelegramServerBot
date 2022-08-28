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

        public UserDTOResponse GetUserByTgId(long Id)
        {
            var responce = HTTP.GetInstance.GET(serverAddress + $"User/GetUserByTG?tgUserId={Id}").Result;
            return JsonConvert.DeserializeObject<UserDTOResponse>(responce);
        } 

        public  LanguageDTOResponse GetUserLanguage(long Id)//TODO async
        {
            var responce = HTTP.GetInstance.GET(serverAddress + $"User/GetUserByTG?tgUserId={Id}").Result;
            return JsonConvert.DeserializeObject<LanguageDTOResponse>(responce);
        }

        public async Task SetThisLanguageForUser(string Code)
        {
            //TODO Сделать запись в данных что пользователь хочет этот язык
        }

        public async Task<bool> UserBoughtThisLicense(string name)
        {
            return false;//TODO Тут будет обработка перед тем, как сказать, что успешная оплата
        }
    }
}
