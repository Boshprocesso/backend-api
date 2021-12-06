using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class EventoBeneficio
    {
        public Guid? IdEvento { get; set; }
        public Guid? IdBeneficio { get; set; }

        public virtual Beneficio? IdBeneficioNavigation { get; set; }
        public virtual Evento? IdEventoNavigation { get; set; }
    }
}
