using System;
using ServerBot.Enums;

namespace ServerBot.Entities
{
    public class LicenseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public string ForProgram { get; set; }
        public TypeOfLicenses TypeOfLicense { get; set; }
    }
}
