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
            return (await _context.SaveChangesAsync()) > 0;}
        
        

        public async Task<Evento[]> GetAllEventos()
        {
            IQueryable<Evento> query = _context.Eventos;
            query = query.AsNoTracking().OrderBy(c => c.IdEvento);
            return await query.ToArrayAsync();
        }

        public async Task<EventoBeneficiario[]> GetAllEventoBeneficiarios()
        {
            IQueryable<EventoBeneficiario> query = _context.EventoBeneficiarios;
            query = query.AsNoTracking().OrderBy(c => c.IdEvento);
            return await query.ToArrayAsync();
        }

        public async Task<dynamic> GetBeneficiosFromEvento(Guid EventoId){

            IQueryable<EventoBeneficio> consultabb = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsultabb = _context.Beneficios;

            var query = (
                from b in benefconsultabb                
                join eb in consultabb on b.IdBeneficio equals eb.IdBeneficio
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

            IQueryable<EventoBeneficio> consultabb = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsultabb = _context.Beneficios;

            var query = (
                from b in benefconsultabb                
                join eb in consultabb on b.IdBeneficio equals eb.IdBeneficio
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
        
            IQueryable<Evento> consultabb = _context.Eventos;

            var resposta = (from c in consultabb
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

             IQueryable<Evento> consultabb = _context.Eventos;

             var resposta = (from c in consultabb
                            where c.NomeEvento == evento.NomeEvento &&
                            c.DataInicio == evento.DataInicio &&
                            c.DataTermino == evento.DataTermino
                            select c);
            resposta = resposta.AsNoTracking();
            return await resposta.ToArrayAsync();
         }



        public async Task RemoverEvento(Guid EventoId){

            IQueryable<Evento> consultabb = _context.Eventos; 
            
            var busca = (from c in consultabb
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

            IQueryable<EventoBeneficio> consultabb = _context.EventoBeneficios; 
            
            var busca = (from c in consultabb
            where c.IdEvento == EventoId &&
            c.IdBeneficio == BeneficioId
            select c).FirstOrDefault();               
            _context.EventoBeneficios.Remove(busca);
            await _context.SaveChangesAsync();         
        }




        public async Task<dynamic> GetLogin(Login login)
        {
            IQueryable<Beneficiario> consultabb = _context.Beneficiarios;
            var data = (from Beneficiario in consultabb 
                        where Beneficiario.Cpf == login.cod 
                        select Beneficiario.DataNascimento);
            var nascimentoLogin = login.nascimento.Split('-');
            Array.Reverse(nascimentoLogin);
            if (Convert.ToString(data.FirstOrDefault()).Split(' ')[0] == String.Join('/',nascimentoLogin)){
            var query = (from beneficiario in consultabb
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

        public async Task<dynamic> GetBeneficios(string cod)
        {
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios;
            IQueryable<Beneficiario> Beneficiarios = _context.Beneficiarios;
            IQueryable<Beneficio> Beneficios = _context.Beneficios;
            var id = (from beneficiario in Beneficiarios
                    where beneficiario.Cpf == cod
                    select beneficiario.IdBeneficiario);
            var queryBeneficios = (from bb in BeneficiarioBeneficios
                        join b in Beneficios on bb.IdBeneficio equals b.IdBeneficio
                        where bb.IdBeneficiario == id.FirstOrDefault()
                        select new{
                            idProduto = b.IdBeneficio,
                            beneficio = b.DescricaoBeneficio,
                            status = bb.Entregue,
                            quantidade = bb.Quantidade
                        });
            var query = from beneficiario in Beneficiarios
                        where beneficiario.IdBeneficiario == id.FirstOrDefault()
                        select new{
                            codFuncionario = cod,
                            nomeFuncionario = beneficiario.NomeCompleto,
                            beneficios = queryBeneficios.ToArray()
                        };

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