using System;
using ServerBot.Enums;

namespace ServerBot.Entities
{
    public class LicenseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Program { get; set; }
        public TypeOfLicenses TypeOfLicense { get; set; }
    }
}
