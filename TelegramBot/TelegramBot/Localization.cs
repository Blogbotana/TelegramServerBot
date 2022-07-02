using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class Localization
    {
        public static string GetTranslation(string Text)
        {
            return Text;//TODO сделать метод который тут берет перевод -думаю лучше будет через ресурсы попробовать
        }

        public static string FromTranslation(string Translation)
        {
            switch (Translation)
            {
                case "База данных узлов":
                    return "DataBase";
                case "Техническая поддержка":
                    return "Support";
                case "Купить лицензию":
                    return "BuyLicense";
                default:
                    return Translation; ;
            }
            //тут будет метод, который возвращает действие кнопки, которую выбрал пользователь
        }
    }
}
