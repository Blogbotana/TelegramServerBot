using ServerBot.Enums;

namespace ServerBot.DTO.Response
{
    public class LicenseDTOResponse
    {
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Program { get; set; }
        public TypeOfLicenses TypeOfLicense { get; set; }
    }
}
