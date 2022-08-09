using ServerBot.Enums;

namespace ServerBot.DTO
{
    public class LicenseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public string ForProgram { get; set; }
        public TypeOfLicenses TypeOfLicense { get; set; }
    }
}
