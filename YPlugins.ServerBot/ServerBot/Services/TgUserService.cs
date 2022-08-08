using ServerBot.DTO.Response;
using ServerBot.Entities;
using ServerBot.Repositories;
using AutoMapper;
using Nelibur.ObjectMapper;

namespace ServerBot.Services
{
    public class TgUserService
    {
        
        public UserResponseDTO GetTgUser(long TGUserId)
        {
            TgUserEntity tgUser = TgUserRepository.GetUserByTGId(TGUserId);
            if (tgUser == null)
            {
                return null;
            }
            return TinyMapper.Map<UserResponseDTO>(tgUser);
        }

        public TgUserEntity CreateUser(TgUserEntity user)
        {
            TgUserEntity userEntity = null;
     //       if (userEntity == null)
       //     {
        //        TgUserRepository.CreateUser(user);
        //        userEntity = GetTgUser(user.TGId);
         //   }
            return userEntity;
        }
    }
}
