using System;
using System.Collections.Generic;

namespace webAPI.Models
{
    public partial class BeneficiarioBeneficioEntregar
    {
        public Guid IdBeneficio  { get; set; }
        public Guid IdBeneficiario  { get; set; }
    }

    public partial class Payload
    {
        public List<BeneficiarioPayload>? Beneficiarios  { get; set; }
        public List<string>? Beneficios  { get; set; }
        public Object? BeneficioBeneficiario  { get; set; }
    }

    public partial class BeneficiarioPayload
    {
        public string nome{ get; set; } = null!;
        public DateTime? nascimento { get; set; }
        public int? edv { get; set; }
        public string? cpf { get; set; }
        public string? unidade { get; set; }
    }
}
