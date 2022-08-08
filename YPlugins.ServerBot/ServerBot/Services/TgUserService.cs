using ServerBot.Entities;
using ServerBot.Repositories;

namespace ServerBot.Services
{
    public class TgUserService
    {
        
        public TgUserEntity GetTgUser(long TGUserId)
        {
            return TgUserRepository.GetUserByTGId(TGUserId);
        }

        public TgUserEntity CreateUser(TgUserEntity user)
        {
            TgUserEntity userEntity = GetTgUser(user.TGId);
            if (userEntity == null)
            {
                TgUserRepository.CreateUser(user);
                userEntity = GetTgUser(user.TGId);
            }
            return userEntity;
        }
    }
}
