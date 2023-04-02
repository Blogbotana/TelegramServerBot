using TelegramBot.DTO.Response;

namespace ServerBot.DTO
{
    public class UserDTOResponse
    {
        public long TgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ComputerInformation { get; set; }
        public string LanguageCode { get; set; }
        public IEnumerable<LicenseDTOResponse> Licenses { get; set; }
    }
}
