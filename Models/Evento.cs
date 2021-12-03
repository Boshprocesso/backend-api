using System;
using System.Collections.Generic;

namespace boschBeneficios.Models
{
    public partial class Evento
    {
        public Guid IdEvento { get; set; }
        public string NomeEvento { get; set; } = null!;
        public string? DescricaoEvento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public string? Inativo { get; set; }
    }
}
