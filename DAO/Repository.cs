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

        public async Task<EventoBeneficio[]> GetBeneficoFromEvento(Guid EventoId){

            IQueryable<EventoBeneficio> consulta = _context.EventoBeneficios;                       
            consulta = consulta.OrderBy(c => c.IdBeneficio).Where(e => e.IdEvento == EventoId);
            
            //consulta.ToList();
           
            
            return await consulta.ToArrayAsync();;
            




        }
    }
    
}