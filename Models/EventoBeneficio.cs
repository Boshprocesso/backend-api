using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class EventoBeneficio
    {
        public Guid IdEvento { get; set; }
        public Guid IdBeneficio { get; set; }

        //public Beneficio beneficio { get; set; }

        //public Evento evento { get; set; }

        public virtual Beneficio? IdBeneficioNavigation { get; set; }
        public virtual Evento? IdEventoNavigation { get; set; }
    }
}
