using ServerBot.Entities;

namespace ServerBot.Repositories
{
    public static class LicenseRepository
    {

        public static IEnumerable<LicenseEntity>? GetLicenseOfUser(int userId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var licenses = context.Users.Where(u => u.Id == userId).FirstOrDefault().Licenses;

                return licenses;
            }
        }
    }
}
