using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public enum UserButtonsMainMenu//TODO make many files
    {
        Support,
        DataBase,
        UserButtonsBuyLicenseMenu,
    }

    public enum UserButtonsBuyLicenseMenu
    {
        TeklaStructures,
        Revit,
        Navis,
        Back
    }

    public enum UserButtonsTeklaMenu
    {
        ProfileChooser,
        SteelSpecification,
        ExcelReportGenerator,
        Back
    }

    public enum UserButtonsNavisMenu
    {
        Back
    }

    public enum UserButtonsRevitMenu
    {
        Back
    }
}
