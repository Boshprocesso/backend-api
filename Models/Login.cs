using Newtonsoft.Json;

namespace webAPI.Models
{
    public class Login
    {
        [JsonProperty]
        public string cod {get; set;}
        public string nascimento {get; set;}
    }
}