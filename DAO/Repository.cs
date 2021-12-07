using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webAPI.Models;

namespace webAPI.DAO
{
    public class Repository : IRepository
    {
        private readonly BOSHBENEFICIOContext _context;
        public Repository (BOSHBENEFICIOContext context){
            _context = context;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Beneficiario[]> GetBeneficiarios()
        {
            IQueryable<Beneficiario> query = _context.Beneficiarios;
            query = query.AsNoTracking().OrderBy(c => c.IdBeneficiario);
            return await query.ToArrayAsync();
        }

        public async Task<BeneficiarioBeneficioResgatar[]> GetBeneficiosParaEntregar(string identificacao)
        {
                Guid idTerceiro, idBeneficiario;
                
                try {
                    if(identificacao.Length < 14) {
                        idBeneficiario = 
                        (from beneficiario in _context.Beneficiarios.AsNoTracking()
                        where beneficiario.Edv == Convert.ToInt32(identificacao)
                        select beneficiario.IdBeneficiario).First();
                    }else {
                        idBeneficiario = 
                            (from beneficiario in _context.Beneficiarios.AsNoTracking()
                            where beneficiario.Cpf == identificacao
                            select beneficiario.IdBeneficiario).First();
                    }
                } catch {
                    idBeneficiario = Guid.Empty;
                }
                
                try {
                    if(identificacao.Length < 14) {
                        identificacao = 
                        (from beneficiario in _context.Beneficiarios.AsNoTracking()
                        where beneficiario.Edv == Convert.ToInt32(identificacao)
                        select beneficiario.Cpf).First();
                    }
                    idTerceiro = 
                        (from terceiro in _context.Terceiros.AsNoTracking()
                        where terceiro.Identificacao == identificacao
                        select terceiro.IdTerceiro).First();
                } catch {
                    idTerceiro = Guid.Empty;
                }

                IQueryable<BeneficiarioBeneficioResgatar> query =
                from beneficiario in _context.Beneficiarios.AsNoTracking()
                join beneficiarioBeneficio in _context.BeneficiarioBeneficios
                on beneficiario.IdBeneficiario equals beneficiarioBeneficio.IdBeneficiario
                join beneficio in _context.Beneficios
                on beneficiarioBeneficio.IdBeneficio equals beneficio.IdBeneficio
                where beneficiarioBeneficio.Entregue == "0" && 
                    (beneficiarioBeneficio.IdBeneficiario == idBeneficiario || beneficiarioBeneficio.IdTerceiro == idTerceiro)
                select new BeneficiarioBeneficioResgatar {
                    Beneficio = beneficio.DescricaoBeneficio,
                    IdBeneficio = beneficio.IdBeneficio,
                    Quantidade = beneficiarioBeneficio.Quantidade,
                    IdBeneficiario = beneficiario.IdBeneficiario,
                    Beneficiario = beneficiario.NomeCompleto

                };

                return await query.ToArrayAsync();
        }

        public void entregarBeneficios(List<BeneficiarioBeneficioEntregar> beneficiosEntregues)
        {
            var BeneficiariosIds = beneficiosEntregues.Select(b => b.IdBeneficiario);

                IQueryable<BeneficiarioBeneficio> updateQuery =
                from beneficiarioBeneficio in _context.BeneficiarioBeneficios.AsNoTracking()
                where BeneficiariosIds.Contains(beneficiarioBeneficio.IdBeneficiario)
                    && beneficiarioBeneficio.Entregue == "0"
                select beneficiarioBeneficio;
                
                foreach (BeneficiarioBeneficio beneficiarioBeneficio in updateQuery)
                {
                    bool BeneficioCorreto = beneficiosEntregues.Any(
                        b => b.IdBeneficiario == beneficiarioBeneficio.IdBeneficiario
                            && b.IdBeneficio == beneficiarioBeneficio.IdBeneficio);

                    if(BeneficioCorreto) {
                        beneficiarioBeneficio.Entregue = "1";
                        _context.BeneficiarioBeneficios.Update(beneficiarioBeneficio);
                    }
                }
        }
    }
    
}