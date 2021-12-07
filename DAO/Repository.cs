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

        public async Task<dynamic> GetBeneficosFromEvento(Guid EventoId){

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
            return await query.ToArrayAsync();    
        }

        public async Task<dynamic> GetUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId){

            IQueryable<EventoBeneficio> consulta = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsulta = _context.Beneficios;

            var query = (
                from b in benefconsulta                
                join eb in consulta on b.IdBeneficio equals eb.IdBeneficio
                where eb.IdEvento == EventoId &&
                eb.IdBeneficio == BeneficioId
                select new  {
                    IdEvento = eb.IdEvento,
                    IdBeneficio = b.IdBeneficio,
                    descricaoBeneficio = b.DescricaoBeneficio
                });

            query = query.AsNoTracking().OrderBy(c => c.IdBeneficio);         
            return await query.ToArrayAsync();


        }

         public async Task<dynamic> GetEventobyId(Guid EventoId){
        
            IQueryable<Evento> consulta = _context.Eventos;

            var resposta = (from c in consulta
                            where c.IdEvento == EventoId
                            select new {
                            nomeEvento = c.NomeEvento,
                            descricaoEvento = c.DescricaoEvento,
                            dataInicio = c.DataInicio,
                            dataFim = c.DataTermino,
                            idEvento = c.IdEvento});
                
                resposta = resposta.AsNoTracking();
                return await resposta.ToArrayAsync();
         }

         public async Task<dynamic> GetEvento(Evento evento){

             IQueryable<Evento> consulta = _context.Eventos;

             var resposta = (from c in consulta
                            where c.NomeEvento == evento.NomeEvento &&
                            c.DataInicio == evento.DataInicio &&
                            c.DataTermino == evento.DataTermino
                            select c);
            resposta = resposta.AsNoTracking();
            return await resposta.ToArrayAsync();
         }



        public async Task RemoverEvento(Guid EventoId){

            IQueryable<Evento> consulta = _context.Eventos; 
            
            var busca = (from c in consulta
            where c.IdEvento == EventoId
            select c).FirstOrDefault();               
            _context.Eventos.Remove(busca);
            await _context.SaveChangesAsync();         
        }

        public async Task inserirEvento(Evento evento){

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

        }

        public async Task RemoverUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId){

            IQueryable<EventoBeneficio> consulta = _context.EventoBeneficios; 
            
            var busca = (from c in consulta
            where c.IdEvento == EventoId &&
            c.IdBeneficio == BeneficioId
            select c).FirstOrDefault();               
            _context.EventoBeneficios.Remove(busca);
            await _context.SaveChangesAsync();         
        }


    }
    
}