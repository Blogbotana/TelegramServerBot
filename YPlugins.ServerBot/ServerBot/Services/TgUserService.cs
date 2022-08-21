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
        private static MapperConfiguration getUser = new MapperConfiguration(ctg => ctg.CreateMap<UserEntity, UserDTOResponse>());

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

        public UserDTOResponse? GetUserByTgId(long tgId)
        {
            var mapper = new Mapper(getUser);
            var userEntity = TgUserRepository.GetUserByTgId(tgId);
            if (userEntity == null)
                return null;

            var userDTO = mapper.Map<UserDTOResponse>(userEntity);
            if (userDTO == null)
                throw new Exception("Не сработал Mapper");

            return userDTO;
        }

        public UserDTOResponse? GetUserByEmail(string email)
        {
            var mapper = new Mapper(getUser);
            var userEntity = TgUserRepository.GetUserByEmail(email);
            if (userEntity == null)
                return null;

            var userDTO = mapper.Map<UserDTOResponse>(userEntity);
            if (userDTO == null)
                throw new Exception("Не сработал Mapper");

            return userDTO;
        }
    }
}
