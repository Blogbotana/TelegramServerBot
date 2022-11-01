using System.Text.Json.Serialization;

namespace ServerBot.DTO
{
    public class PasswordDTO
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
