

using System.Text.Json.Serialization;

namespace CarRental.Authentication.Models.DTOs.Outgoing
{
    public class AuthModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public List<string> Roles { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
