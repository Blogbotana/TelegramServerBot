namespace ServerBot.DTO
{
    public class UserDTO
    {
        public long TgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ComputerInformation { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
