using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class BeneficiarioBeneficioResgatar
    {
        public string? Beneficio  { get; set; }
        public Guid IdBeneficio  { get; set; }
        public int? Quantidade  { get; set; }
        public Guid IdBeneficiario  { get; set; }
        public string? Beneficiario  { get; set; }

    }

    public partial class BeneficiarioBeneficioEntregar
    {
        public Guid IdBeneficio  { get; set; }
        public Guid IdBeneficiario  { get; set; }
    }
}
