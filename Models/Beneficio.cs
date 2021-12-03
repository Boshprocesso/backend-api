using System;
using System.Collections.Generic;

namespace boschBeneficios.Models
{
    public partial class Beneficio
    {
        public Guid IdBeneficio { get; set; }
        public string DescricaoBeneficio { get; set; } = null!;
    }
}
