using System;
using System.Collections.Generic;

namespace boschBeneficios.Models
{
    public partial class IlhaBeneficio
    {
        public Guid? IdIlha { get; set; }
        public Guid? IdBeneficio { get; set; }

        public virtual Beneficio? IdBeneficioNavigation { get; set; }
        public virtual Ilha? IdIlhaNavigation { get; set; }
    }
}
