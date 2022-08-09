using ServerBot.Entities;
using ServerBot.Repositories;
using ServerBot.DTO;
using AutoMapper;

namespace ServerBot.Services
{
    public static class TgUserService
    {
        static MapperConfiguration configUser = new MapperConfiguration(cfg => cfg.CreateMap<UserEntity, UserDTO>());
        public static void CreateUser(UserDTO user)
        {
            var mapper = new Mapper(configUser);
            var userEntity = mapper.Map<UserEntity>(user);
            if (userEntity == null)
                throw new Exception("Не сработал Mapper");

            var result = TgUserRepository.CreateUser(userEntity);

            if(result <= 0)
            {
                throw new Exception("Не добавили пользователя" + userEntity.TgId + " " + userEntity.Email);
            }
        }
    }
}
