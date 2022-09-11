using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ServerBot
{
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; private set; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
