using Newtonsoft.Json;

namespace webAPI.Models
{
    public class TerceiroModel
    {
        [JsonProperty]
        public Guid cod {get; set;}
        public string nascimento {get; set;}
        public string opcaoSelecionada {get; set;}
        public string identificacaoTerceiro {get; set;}
        public string nomeTerceiro {get; set;}
    }
}