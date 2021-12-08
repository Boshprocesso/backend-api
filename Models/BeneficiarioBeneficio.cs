using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class BeneficiarioBeneficio
    {
        public Guid IdBeneficiario { get; set; }
        public Guid IdBeneficio { get; set; }
        public Guid? IdTerceiro { get; set; }
        public string? Entregue { get; set; }
        public int? Quantidade { get; set; }

        public virtual Beneficiario? IdBeneficiarioNavigation { get; set; }
        public virtual Beneficio? IdBeneficioNavigation { get; set; }
        public virtual Terceiro? IdTerceiroNavigation { get; set; }
    }
}
