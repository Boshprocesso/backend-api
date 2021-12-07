using System.Linq;
using System.Threading.Tasks;
using backend_api.Models;
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

        public async Task<Beneficiario[]> GetBeneficiarios()
        {
            IQueryable<Beneficiario> query = _context.Beneficiarios;
            query = query.AsNoTracking().OrderBy(c => c.IdBeneficiario);
            return await query.ToArrayAsync();
        }

        public async Task<dynamic> GetLogin(Login login)
        {
            IQueryable<Beneficiario> consulta = _context.Beneficiarios;
            var data = (from Beneficiario in consulta where Beneficiario.Cpf == login.cod select Beneficiario.DataNascimento);
            if (Convert.ToString(data.FirstOrDefault()).Split(' ')[0] == login.nascimento){
            var query = (from beneficiario in consulta
                        where beneficiario.Cpf == login.cod
                        select new 
                        {
                            codFuncionario = beneficiario.Cpf,
                            nomeFuncionario = beneficiario.NomeCompleto,
                            nascimento = beneficiario.DataNascimento
                        });
                return await query.ToArrayAsync();
            }
            return null;
        }
    }
    
}