using ServerBot.Entities;
using System.Collections.Generic;

namespace ServerBot
{
    public static class MyDataInitializer
    {
        public static void SetData(ApplicationContext context)
        {
            context.Languages.Add(new LanguageEntity
            {
                EnglishName = "English",
                IETF_LanguageTag = "En"
            });

            context.SaveChangesAsync();
            // or execute custom sql here
        }

        public static void InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                
                SetData(db);
            }
        }
    }
}
