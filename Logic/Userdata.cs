using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekt_WPF_TODO_app.Logic
{
    public class Userdata
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
    }
}
