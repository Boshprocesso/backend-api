using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public class BenficiarioAux
    {
        public Guid EventoId { get; set; }
        public Beneficiario colaborador { get; set; }

        public List<dynamic> ListaBeneficios { get; set; }
    }
}