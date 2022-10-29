using Microsoft.IdentityModel.Tokens;
using ServerBot.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ServerBot.Services
{
    public class JWTTokenService
    {
        private const string Secret_Key = "cQfTjWnZr4u7w!z%C*F-JaNdRgUkXp2s";
        private static SymmetricSecurityKey SymmetricSecurityKey { get; } = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret_Key));
        private static JWTTokenService? tokenService;
        private JWTTokenService()
        { }

        public static JWTTokenService GetService()
        {
            tokenService ??= new JWTTokenService();
            return tokenService;
        }

        public string GenerateToken(UserDTO userDTO)
        {
            var credentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);
            DateTime expiry = DateTime.UtcNow.AddHours(2);
            int totalSeconds = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload
            {
                { "sub", "UserDTOResponse"},
                { "Name", userDTO.FirstName},
                { "Email", userDTO.Email},
                { "exp", totalSeconds},
                { "iss", "http://localhost:5007"},
                { "aud", "http://localhost:5007" }
            };
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(secToken);
        }
    }
}
