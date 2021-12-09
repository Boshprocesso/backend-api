using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class EventoBeneficiario
    {
        public Guid IdEvento { get; set; }
        public Guid IdBeneficiario { get; set; }

        public virtual Beneficiario IdBeneficiarioNavigation { get; set; } = null!;
        public virtual Evento IdEventoNavigation { get; set; } = null!;
    }
}
