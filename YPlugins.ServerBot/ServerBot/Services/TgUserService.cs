using ServerBot.Entities;
using ServerBot.Repositories;
using ServerBot.DTO;
using AutoMapper;

namespace ServerBot.Services
{
    public static class TgUserService
    {
        static MapperConfiguration configUser = new MapperConfiguration(cfg => cfg.CreateMap<UserEntity, UserDTO>());
        static MapperConfiguration configUser1 = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserEntity>()); 
        public static int CreateUser(UserDTO user)
        {
            var mapper = new Mapper(configUser1);
            var userEntity = mapper.Map<UserEntity>(user);
            if (userEntity == null)
                throw new Exception("Не сработал Mapper");

            userEntity.Language = new LanguageEntity() { CodeTelegram = "ru" };//TODO Исправить костыль тут

            var result = TgUserRepository.CreateUser(userEntity);

            return result;
        }
    }
}
