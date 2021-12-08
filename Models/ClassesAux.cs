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
        public Guid IdEvento  { get; set; }
        public List<BeneficiarioPayload>? Beneficiarios  { get; set; }
        public List<string>? Beneficios  { get; set; }
        public Dictionary<string, List<CpfQuantidade>>? BeneficioBeneficiario  { get; set; }
    }

    public partial class BeneficiarioPayload
    {
        public string nome{ get; set; } = null!;
        public DateTime? nascimento { get; set; }
        public int? edv { get; set; }
        public string? cpf { get; set; }
        public string? unidade { get; set; }
    }

    public partial class CpfQuantidade
    {
        public string? cpf { get; set; }
        public int? Quantidade { get; set; }
    }
}
