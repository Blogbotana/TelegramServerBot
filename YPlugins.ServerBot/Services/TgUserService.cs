using ServerBot.Entities;
using ServerBot.Repositories;
using AutoMapper;
using ServerBot.DTO;
using ServerBot.DTO.Response;

namespace ServerBot.Services
{
    public class TgUserService
    {
        private static TgUserService? instance;
        private static MapperConfiguration fromUserDTOToUserEntity = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserEntity>());//From DTO To Entity. Static field is inquired by mapper
        //private static MapperConfiguration fromUserEntityToUserDTOResponse = new MapperConfiguration(ctg => ctg.CreateMap<UserEntity, UserDTOResponse>());
        private static MapperConfiguration fromLanguageEnityToLanguageDTOResponse = new MapperConfiguration(ctg => ctg.CreateMap<LanguageEntity, LanguageDTOResponse>());
        private static MapperConfiguration fromLanguageDTOToLanguageEntity = new MapperConfiguration(ctg => ctg.CreateMap<LanguageDTO, LanguageEntity>());
        //private static MapperConfiguration fromLicenseDTOToLicenseEntity = new MapperConfiguration(ctg => ctg.CreateMap<LicenseDTO, LicenseEntity>());
        private static MapperConfiguration fromUserEntityToUserDTOResponse = new MapperConfiguration(ctg => ctg.CreateMap<UserEntity, UserDTOResponse>().ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.IETF_LanguageTag)));

        public static TgUserService GetService()
        {
            instance ??= new TgUserService();

            return instance;
        }
        private TgUserService()
        {

        }

        public bool CreateUser(UserDTO user)
        {
            var mapper = new Mapper(fromUserDTOToUserEntity);
            var userEntity = mapper.Map<UserEntity>(user);
            if (userEntity == null)
                throw new Exception("Не сработал Mapper");

            userEntity.Language = LanguageRepository.GetLanguageByCode(user.LanguageCode);

            var result = TgUserRepository.CreateUser(userEntity);

            return result > 0;
        }

        public UserDTOResponse? GetUserByTgId(long tgId)
        {
            var mapper = new Mapper(fromUserEntityToUserDTOResponse);
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
            var mapper = new Mapper(fromUserEntityToUserDTOResponse);
            var userEntity = TgUserRepository.GetUserByEmail(email);
            if (userEntity == null)
                return null;

            var userDTO = mapper.Map<UserDTOResponse>(userEntity);
            if (userDTO == null)
                throw new Exception("Не сработал Mapper");

            return userDTO;
        }

        public LanguageDTOResponse? GetUserLanguageByTgId(long tgUserId)
        {
            var mapper = new Mapper(fromLanguageEnityToLanguageDTOResponse);
            var languageResponse = LanguageRepository.GetLanguageOfUser(tgUserId);
            if (languageResponse == null)
                return mapper.Map<LanguageDTOResponse>(LanguageRepository.GetDefaultLanguage());
            else
            {
                var languageDTO = mapper.Map<LanguageDTOResponse>(languageResponse);
                if(languageDTO == null)
                    throw new Exception("Не сработал Mapper");

                return languageDTO;
            }
        }

        public void SetThisLanguageForUser(long tgUserId, LanguageDTO language)
        {
            var mapper = new Mapper(fromLanguageDTOToLanguageEntity);
            var languageEntity = mapper.Map<LanguageEntity>(language);
            var userEntity = TgUserRepository.GetUserByTgId(tgUserId);

            if (userEntity == null)
                return;

            TgUserRepository.SetThisLanguageForUser(userEntity, languageEntity);
        }

        public void ThisUserBoughtThisLicence(long tgUserId, LicenseDTO license, int days)
        {
            var licenseEntity = LicenseRepository.GetLicenseByName(license.Name);
            if (licenseEntity == null)
                return;

            licenseEntity.ExpirationDate = DateTime.Now.AddDays(days);           

            var userEntity = TgUserRepository.GetUserByTgId(tgUserId);
            if (userEntity == null)
                return;
            TgUserRepository.SetThisLicenseForUser(userEntity, licenseEntity);
        }

        public void SetThisLanguageForUser(string email, LanguageDTO language)
        {
            var mapper = new Mapper(fromLanguageDTOToLanguageEntity);
            var languageEntity = mapper.Map<LanguageEntity>(language);
            var userEntity = TgUserRepository.GetUserByEmail(email);

            if (userEntity == null)
                return;

            TgUserRepository.SetThisLanguageForUser(userEntity, languageEntity);
        }

        internal void SetEmailAndNameForTgUser(long tgUserId, string email, string name)
        {
            var userEntity = TgUserRepository.GetUserByTgId(tgUserId);
            userEntity.Email = email;
            userEntity.FirstName = name;
            TgUserRepository.EditUser(userEntity);
        }

        internal void SetTgIdAndNameForEmailUser(string email, long tgUserId, string name)
        {
            var userEntity = TgUserRepository.GetUserByEmail(email);
            userEntity.TgId = tgUserId;
            userEntity.FirstName = name;
            TgUserRepository.EditUser(userEntity);
        }
    }
}
