using System;
using System.Collections.Generic;


namespace boschBeneficios.Models
{
    public partial class Beneficiario
    {
        public Guid IdBeneficiario { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public DateTime? DataNascimento { get; set; }
        public int? Edv { get; set; }
        public string? Cpf { get; set; }
        public string? Unidade { get; set; }
        public DateTime? DataInclusao { get; set; }
        public string? ResponsavelInclusao { get; set; }
    }
}
