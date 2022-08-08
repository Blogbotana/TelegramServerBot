using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerBot.Entities
{
    public class TgUserEntity
    {
        [Column("Id", TypeName = "int"), Key]
        public int Id { get; set; }
        public long TGId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        //public int LanguagesId { get; set; }
        public Languages Language { get; set; }

      
    }
}
