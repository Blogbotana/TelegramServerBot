using Newtonsoft.Json;
using ServerBot.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (instance == null)
                    lock (SyncObject)
                    {
                        if (instance == null)
                            instance = new ServerAPI();
                    }
                return instance;
            }
        }

        public void RegisterUser(User userfromTG)
        {
            UserDTO userDTO = new UserDTO()
            {
                ComputerInformation = "CompInfo",
                Email = userfromTG.Username,
                FirstName = userfromTG.FirstName,
                LastName = userfromTG.LastName,
                LanguageCode = userfromTG.LanguageCode,
                TgId = userfromTG.Id
            };


            //Dictionary<string, string> parameters = new Dictionary<string, string>
            //    {
            //         { "userJson", JsonConvert.SerializeObject(userDTO) }
            //    };
            //byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userDTO));

            HTTP.GetInstance.POST(serverAddress + "User/Create", JsonConvert.SerializeObject(userDTO));
        }
    }
}
