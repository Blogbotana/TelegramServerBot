using Microsoft.EntityFrameworkCore;
using ServerBot.Entities;

namespace ServerBot.Repositories
{
    public static class TgUserRepository
    {
        public static void CreateUser(TgUserEntity user)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {

                var lang = applicationContext.Languages.FirstOrDefault(r => r.Code == user.Language.Code);
                user.Language = lang;
                applicationContext.TgUsers.Add(user);
                applicationContext.SaveChanges();
               }
        }

        public static TgUserEntity GetUserByTGId(long TgID)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                TgUserEntity result = applicationContext.TgUsers.Include(e => e.Language).Where(u => u.TGId == TgID).FirstOrDefault();
                return result;
            }
        }
    }
}
