using ServerBot.Entities;
using ServerBot.Repositories;
using ServerBot.DTO;
using AutoMapper;

namespace ServerBot.Services
{
    public class TgUserService
    {
        public void CreateUser(UserDTO user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserEntity, UserDTO>());
        }
    }

    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Person, Student>();
        }
    }
}
