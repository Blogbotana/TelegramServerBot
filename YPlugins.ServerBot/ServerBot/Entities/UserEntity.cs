using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerBot.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public long? TgId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = "The email format is not valid")]
        public string Email { get; set; }
        public string? ComputerInformation { get; set; }
        public DateTime? LastLogin { get; set; }
        
        public IEnumerable<LicenseEntity>? Licenses { get; set; }

        [Required]
        public LanguageEntity Language { get; set; }
    }
}
