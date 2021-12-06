using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class Terceiro
    {
        public Guid IdTerceiro { get; set; }
        public string Nome { get; set; } = null!;
        public string Identificacao { get; set; } = null!;
        public DateTime? DataIndicacao { get; set; }
    }
}
