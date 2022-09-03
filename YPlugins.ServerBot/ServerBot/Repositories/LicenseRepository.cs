using ServerBot.Entities;
using System.Security.Cryptography.X509Certificates;

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

        public static LicenseEntity? GetLicenseByName(string name)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var licenses = context.Licenses.Where(u => u.Name == name).FirstOrDefault();

                return licenses;
            }
        }
    }
}
