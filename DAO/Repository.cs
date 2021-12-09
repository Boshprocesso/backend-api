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
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        

        public async Task<Evento[]> GetAllEventos()
        {
            IQueryable<Evento> query = _context.Eventos;
            query = query.AsNoTracking().OrderBy(c => c.IdEvento);
            return await query.ToArrayAsync();
        }

        public async Task<dynamic> GetBeneficiosFromEvento(Guid EventoId){

            IQueryable<EventoBeneficio> consultabb = _context.EventoBeneficios; 
            IQueryable<Beneficio> benefconsultabb = _context.Beneficios;

            IQueryable<Beneficio> query = (
                from b in benefconsultabb                
                join eb in consultabb on b.IdBeneficio equals eb.IdBeneficio
                where eb.IdEvento == EventoId
                select b);

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

        public async Task<dynamic> GetBeneficiosParaEntregar(Guid idEvento, string identificacao)
        {
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios.AsNoTracking();
            IQueryable<EventoBeneficio> EventoBeneficios = _context.EventoBeneficios.AsNoTracking();
            IQueryable<Beneficiario> Beneficiarios = _context.Beneficiarios.AsNoTracking();
            IQueryable<Beneficio> Beneficios = _context.Beneficios.AsNoTracking();
            IQueryable<Terceiro> Terceiros = _context.Terceiros.AsNoTracking();
            Guid idTerceiro, idBeneficiario;
            
            try {
                if(identificacao.Length < 14) {
                    idBeneficiario = 
                    (from beneficiario in Beneficiarios
                    where beneficiario.Edv == Convert.ToInt32(identificacao)
                    select beneficiario.IdBeneficiario).First();
                }else {
                    idBeneficiario = 
                        (from beneficiario in Beneficiarios
                        where beneficiario.Cpf == identificacao
                        select beneficiario.IdBeneficiario).First();
                }
            } catch {
                idBeneficiario = Guid.Empty;
            }
            
            try {
                if(identificacao.Length < 14) {
                    identificacao = 
                    (from beneficiario in Beneficiarios
                    where beneficiario.Edv == Convert.ToInt32(identificacao)
                    select beneficiario.Cpf).First();
                }
                idTerceiro = 
                    (from terceiro in Terceiros
                    where terceiro.Identificacao == identificacao
                    select terceiro.IdTerceiro).First();
            } catch {
                idTerceiro = Guid.Empty;
            }

            var query =
            from beneficiario in Beneficiarios
            join beneficiarioBeneficio in BeneficiarioBeneficios
            on beneficiario.IdBeneficiario equals beneficiarioBeneficio.IdBeneficiario
            join beneficio in Beneficios
            on beneficiarioBeneficio.IdBeneficio equals beneficio.IdBeneficio
            join eventoBeneficio in EventoBeneficios
            on beneficio.IdBeneficio equals eventoBeneficio.IdBeneficio
            where beneficiarioBeneficio.Entregue == "0" && 
                (beneficiarioBeneficio.IdBeneficiario == idBeneficiario
                    || beneficiarioBeneficio.IdTerceiro == idTerceiro)
                && eventoBeneficio.IdEvento == idEvento
            select new {
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
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios.AsNoTracking();
            var BeneficiariosIds = beneficiosEntregues.Select(b => b.IdBeneficiario);

            var query =
                from beneficiarioBeneficio in BeneficiarioBeneficios
                where BeneficiariosIds.Contains(beneficiarioBeneficio.IdBeneficiario)
                    && beneficiarioBeneficio.Entregue == "0"
                select beneficiarioBeneficio;
            
            foreach (BeneficiarioBeneficio beneficiarioBeneficio in query)
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

        public async Task carregarBeneficiarios(List<BeneficiarioPayload> BeneficiariosParaInserir)
        {
            List<Beneficiario> Beneficiarios = _context.Beneficiarios.AsNoTracking().ToList();

            BeneficiariosParaInserir = BeneficiariosParaInserir.Where(bpi => !Beneficiarios.Any(b => b.Cpf == bpi.cpf)).ToList();

            foreach (var beneficiarioParaInserir in BeneficiariosParaInserir)
            {
                Beneficiario beneficiario = new Beneficiario {
                    NomeCompleto = beneficiarioParaInserir.nome,
                    DataNascimento = beneficiarioParaInserir.nascimento,
                    Edv = beneficiarioParaInserir.edv,
                    Cpf = beneficiarioParaInserir.cpf,
                    Unidade = beneficiarioParaInserir.unidade,
                    DataInclusao = DateTime.Now,
                    ResponsavelInclusao = "Import Process"
                };
                
                _context.Beneficiarios.Add(beneficiario);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Beneficio>> carregarBeneficios(Guid idEvento, List<string> BeneficiosParaInserir)
        {
            IQueryable<EventoBeneficio> EventoBeneficios = _context.EventoBeneficios.AsNoTracking();
            IQueryable<Beneficio> beneficios = _context.Beneficios.AsNoTracking();
            List<Beneficio> listaBeneficios = new List<Beneficio>();

            List<Beneficio> beneficiosDoEvento = (
                from beneficio in beneficios
                join EventoBeneficio in EventoBeneficios
                on beneficio.IdBeneficio equals EventoBeneficio.IdBeneficio
                where EventoBeneficio.IdEvento == idEvento
                select beneficio
            ).ToList();
            
            BeneficiosParaInserir = BeneficiosParaInserir.Where(bpi => !beneficiosDoEvento.Any(b => b.DescricaoBeneficio == bpi)).ToList();
            
            foreach (var descricaoBeneficio in BeneficiosParaInserir)
            {
                Beneficio beneficio = new Beneficio {
                    DescricaoBeneficio = descricaoBeneficio
                };
                
                listaBeneficios.Add(beneficio);
                _context.Beneficios.Add(beneficio);
            }

            await _context.SaveChangesAsync();

            return listaBeneficios;
        }

        public void carregarEventoBeneficio(Guid idEvento, List<Beneficio> Beneficios)
        {
            foreach (Beneficio beneficio in Beneficios)
            {
                EventoBeneficio eventoBeneficio = new EventoBeneficio {
                    IdEvento = idEvento,
                    IdBeneficio = beneficio.IdBeneficio
                };

                _context.EventoBeneficios.Add(eventoBeneficio);
            }

        }

        public void carregarBeneficiarioBeneficio(List<Beneficio> Beneficios, List<Beneficiario> Beneficiarios, Dictionary<string, List<CpfQuantidade>> BeneficioBeneficiario)
        {

            foreach (KeyValuePair<string, List<CpfQuantidade>> entrada in BeneficioBeneficiario)
            {
                Beneficio? beneficio = Beneficios.Where(b => b.DescricaoBeneficio == entrada.Key).FirstOrDefault();

                if(beneficio != null) {
                    foreach (CpfQuantidade cpfQuantidade in entrada.Value)
                    {
                        Beneficiario beneficiario = Beneficiarios.Where(b => b.Cpf == cpfQuantidade.cpf).First();

                        BeneficiarioBeneficio beneficiarioBeneficio = new BeneficiarioBeneficio {
                            IdBeneficiario = beneficiario.IdBeneficiario,
                            IdBeneficio = beneficio.IdBeneficio,
                            Quantidade = cpfQuantidade.Quantidade
                        };

                        _context.BeneficiarioBeneficios.Add(beneficiarioBeneficio);
                    }
                }
            }
        }
    }
    
}