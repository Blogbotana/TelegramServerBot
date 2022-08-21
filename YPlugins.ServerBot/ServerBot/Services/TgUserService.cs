using ServerBot.Entities;
using ServerBot.Repositories;
using AutoMapper;
using ServerBot.DTO;

namespace ServerBot.Services
{
    public class TgUserService
    {
        private static TgUserService instance;
        private static MapperConfiguration configUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserEntity>());//From DTO To Entity. Static field is inquired by mapper


        public static TgUserService GetService()
        {
            if (instance == null)
                instance = new TgUserService();

            return instance;
        }
        private TgUserService()
        {

        }

        public bool CreateUser(UserDTO user)
        {
            var mapper = new Mapper(configUser);
            var userEntity = mapper.Map<UserEntity>(user);
            if (userEntity == null)
                throw new Exception("Не сработал Mapper");

            userEntity.Language = LanguageRepository.GetLanguageByCode(user.LanguageCode);

            var result = TgUserRepository.CreateUser(userEntity);

            return result > 0;
        }
    }
}
