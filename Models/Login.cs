using Newtonsoft.Json;

namespace backend_api.Models
{
    public class Login
    {
        [JsonProperty]
        public string cod {get; set;}
        public string nascimento {get; set;}
    }
}