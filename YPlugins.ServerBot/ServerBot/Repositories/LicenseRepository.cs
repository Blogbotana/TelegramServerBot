using ServerBot.Entities;

namespace ServerBot.Repositories
{
    public static class LicenseRepository
    {

        public static IEnumerable<LicenseEntity> GetLicenseOfUser(UserEntity userEntity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var licenses = context.Users.Where(u => u.Id == userEntity.Id).FirstOrDefault().Licenses;

                return licenses;
            }
        }
    }
}
