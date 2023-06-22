using System.Text.Json.Serialization;


namespace Projekt_WPF_TODO_app.Logic
{
    public class User
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("password2")]
        public string? Password2 { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }


    }
}
