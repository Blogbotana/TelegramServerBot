﻿using Microsoft.EntityFrameworkCore;
using ServerBot.Entities;
using System;

namespace ServerBot.Repositories
{
    public static class TgUserRepository
    {
        /// <summary>
        /// Create user to database
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns>The number of state entries written to the database.</returns>
        /// <exception cref="Exception"></exception>
        public static int CreateUser(UserEntity userEntity)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var lang = applicationContext.Languages.FirstOrDefault(l =>
                l.Id == userEntity.Language.Id);

                if (lang == null)
                    lang = LanguageRepository.GetDefaultLanguage();

                userEntity.Language = lang;
                applicationContext.Users.Add(userEntity);
                return applicationContext.SaveChanges();
            }
        }

        public static UserEntity GetUserByTgId(long userTgId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                UserEntity user = context.Users.Where(u => u.TgId == userTgId).FirstOrDefault();

                if(user == null)
                    return null;

                context.Entry(user).Reference(t => t.Language).Load();
                context.Entry(user).Collection(c => c.Licenses).Load();
                return user;
            }
        }
    }
}
