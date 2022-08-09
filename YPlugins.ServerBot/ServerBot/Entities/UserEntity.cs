using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerBot.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public long TgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ComputerInformation { get; set; }
        public DateTime LastLogin { get; set; }
        
        public IEnumerable<LicenseEntity>? Licenses { get; set; }

        [Required]
        public LanguageEntity Language { get; set; }
    }
}
