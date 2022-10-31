using System.Text.Json.Serialization;

namespace ServerBot.DTO.Request
{
    public class PasswordDTO
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
