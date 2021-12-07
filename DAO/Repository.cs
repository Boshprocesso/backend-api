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

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Beneficiario[]> GetAllBeneficiarios()
        {
            IQueryable<Beneficiario> query = _context.Beneficiarios;
            query = query.AsNoTracking().OrderBy(c => c.IdBeneficiario);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventos()
        {
            IQueryable<Evento> query = _context.Eventos;
            query = query.AsNoTracking().OrderBy(c => c.IdEvento);
            return await query.ToArrayAsync();
        }

        public async Task<dynamic> GetBeneficoFromEvento(Guid EventoId){

            IQueryable<EventoBeneficio> consulta = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsulta = _context.Beneficios;

            var query = (
                from b in benefconsulta                
                join eb in consulta on b.IdBeneficio equals eb.IdBeneficio
                where eb.IdEvento == EventoId
                select new  {
                    IdEvento = eb.IdEvento,
                    IdBeneficio = b.IdBeneficio,
                    descricaoBeneficio = b.DescricaoBeneficio
                });

            query = query.AsNoTracking().OrderBy(c => c.IdBeneficio);
            
            //consulta.ToList();           
            
            return await query.ToArrayAsync();    
        }

        /*public async Task RemoverEvento(Guid EventoId){

            IQueryable<Evento> consulta = _context.Eventos; 
            
            var busca = (from c in consulta
            where c.IdEvento == EventoId
            select c).FirstOrDefault();

            if (busca != null){
                consulta.Remove();
            }


        }*/




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