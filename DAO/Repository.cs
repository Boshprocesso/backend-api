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

        public async Task<dynamic> GetBeneficiosFromEvento(Guid EventoId){

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

        public async Task<dynamic> GetUmBeneficioFromEventobyId(Guid EventoId, Guid BeneficioId){

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

        public async Task<dynamic> GetUmBeneficioFromEvento(Guid EventoId, Beneficio beneficio){

            IQueryable<EventoBeneficio> consulta = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsulta = _context.Beneficios;

            var busca = (
                from b in benefconsulta
                where b.DescricaoBeneficio == beneficio.DescricaoBeneficio                
                select b.IdBeneficio).First();

                var query = (
                from b in benefconsulta                
                join eb in consulta on b.IdBeneficio equals eb.IdBeneficio
                where eb.IdEvento == EventoId &&
                eb.IdBeneficio == busca
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

        public async Task EditarEvento(Guid EventoId, Evento novoEvento){
            
           
            Evento nEvento = new Evento();
            nEvento.IdEvento = EventoId;
            nEvento.NomeEvento = novoEvento.NomeEvento;
            nEvento.DescricaoEvento = novoEvento.DescricaoEvento;
            nEvento.DataInicio = novoEvento.DataInicio;
            nEvento.DataTermino = novoEvento.DataTermino;         

            _context.Eventos.Update(nEvento);
            await _context.SaveChangesAsync();  


        }

        public async Task inserirBeneficio(Beneficio beneficio){

            _context.Beneficios.Add(beneficio);
            await _context.SaveChangesAsync();
        }

        public async Task inserirBeneficioEvento(Guid EventoId,Beneficio beneficio){


            IQueryable<EventoBeneficio> consulta = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsulta = _context.Beneficios;

           var query = (
                from b in benefconsulta        
                where b.DescricaoBeneficio == beneficio.DescricaoBeneficio 
                select b.IdBeneficio).First();

            EventoBeneficio ev = new EventoBeneficio();
            ev.IdEvento = EventoId;
            ev.IdBeneficio = query;

            _context.EventoBeneficios.Add(ev);
            await _context.SaveChangesAsync();
            
        }

        public async Task editarBenficioFromEvento(Guid EvendoId, Guid BeneficioId, Beneficio Descbeneficio)
        {

                IQueryable<Beneficio> benefconsulta = _context.Beneficios;                

                var query = (
                from b in benefconsulta        
                where b.IdBeneficio == BeneficioId 
                select b.IdBeneficio).First();

                Beneficio ben = new Beneficio();
                ben.IdBeneficio = query;
                ben.DescricaoBeneficio = Descbeneficio.DescricaoBeneficio;

                _context.Beneficios.Update(ben);
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




        public async Task<dynamic> GetLogin(Login login)
        {
            IQueryable<Beneficiario> consulta = _context.Beneficiarios;
            var data = (from Beneficiario in consulta where Beneficiario.Cpf == login.cod select Beneficiario.DataNascimento);
            var nascimentoLogin = login.nascimento.Split('-');
            Array.Reverse(nascimentoLogin);
            if (Convert.ToString(data.FirstOrDefault()).Split(' ')[0] == String.Join('/',nascimentoLogin)){
            var query = (from beneficiario in consulta
                        where beneficiario.Cpf == login.cod
                        select new 
                        {
                            codFuncionario = beneficiario.Cpf,
                            nomeFuncionario = beneficiario.NomeCompleto,
                            nascimento = beneficiario.DataNascimento,
                            administrativo = true,
                            entregaproduto = true
                        });
                return await query.ToArrayAsync();
            }
            return null;
        }

    }
    
}