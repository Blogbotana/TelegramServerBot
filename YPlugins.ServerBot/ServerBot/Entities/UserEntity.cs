namespace ServerBot.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public long TgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ComputerInformation { get; set; }
        public DateTime LastLogin { get; set; }
        public IEnumerable<LicenseEntity> Licenses { get; set; }
        public LanguageEntity Language { get; set; }
    }
}
