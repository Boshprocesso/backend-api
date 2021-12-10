using System.Collections.Generic;
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

        public async Task<EventoBeneficiario[]> GetAllEventoBeneficiarios()
        {
            IQueryable<EventoBeneficiario> query = _context.EventoBeneficiarios;
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
            IQueryable<DateTime?>? data;
            String[]? nascimentoLogin;
            IQueryable<Beneficiario> consultabb = _context.Beneficiarios;
            if(login.cod.Length == 14)
            { 
                data = (from Beneficiario in consultabb.AsNoTracking()
                            where Beneficiario.Cpf == login.cod 
                            select Beneficiario.DataNascimento);
                nascimentoLogin = login.nascimento.Split('-');
                Array.Reverse(nascimentoLogin);
                if (Convert.ToString(data.FirstOrDefault()).Split(' ')[0] == String.Join('/',nascimentoLogin)){
                var query = (from beneficiario in consultabb.AsNoTracking()
                            where beneficiario.Cpf == login.cod
                            select new 
                            {
                                codFuncionario = beneficiario.IdBeneficiario,
                                nomeFuncionario = beneficiario.NomeCompleto,
                                nascimento = beneficiario.DataNascimento,
                                administrativo = true,
                                entregaproduto = true
                            });
                    return await query.ToArrayAsync();
                }
                return null;
            }
                data = (from Beneficiario in consultabb 
                            where Beneficiario.Edv == Convert.ToInt32(login.cod) 
                            select Beneficiario.DataNascimento);
                nascimentoLogin = login.nascimento.Split('-');
                Array.Reverse(nascimentoLogin);
                if (Convert.ToString(data.FirstOrDefault()).Split(' ')[0] == String.Join('/',nascimentoLogin)){
                var query = (from beneficiario in consultabb.AsNoTracking()
                            where beneficiario.Edv == Convert.ToInt32(login.cod)
                            select new 
                            {
                                codFuncionario = beneficiario.IdBeneficiario,
                                nomeFuncionario = beneficiario.NomeCompleto,
                                nascimento = beneficiario.DataNascimento,
                                administrativo = true,
                                entregaproduto = true
                            });
                var resposta = from beneficiario in consultabb.AsNoTracking()
                            where beneficiario.Edv == Convert.ToInt32(login.cod)
                            select new {
                                login = query.ToArray()
                            };
                    return await resposta.ToArrayAsync();
                }
                return null;
            
        }

        public async Task<dynamic> GetBeneficios(Guid cod)
        {
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios;
            IQueryable<Beneficiario> Beneficiarios = _context.Beneficiarios;
            IQueryable<Beneficio> Beneficios = _context.Beneficios;
            IQueryable<Terceiro> Terceiros = _context.Terceiros;


            var consultaBeneficios = (from bb in BeneficiarioBeneficios.AsNoTracking()
                        join b in Beneficios.AsNoTracking() on bb.IdBeneficio equals b.IdBeneficio
                        where bb.IdBeneficiario == cod
                        select new
                        {
                            idProduto = b.IdBeneficio,
                            beneficio = b.DescricaoBeneficio,
                            status = bb.Entregue,
                            quantidade = bb.Quantidade
                        });

            var respostaBeneficiario = from beneficiario in Beneficiarios.AsNoTracking()
                        where beneficiario.IdBeneficiario == cod
                        select new
                        {
                            codFuncionario = cod,
                            nomeFuncionario = beneficiario.NomeCompleto,
                            beneficios = consultaBeneficios.ToArray()
                        };
            
            List<dynamic> respostaTerceiro = new List<dynamic>();

            var listaTerceiros = (from bb in BeneficiarioBeneficios where bb.IdTerceiro == cod select bb).GroupBy(s => s.IdBeneficiario).Select(s => s.First()).ToArray().Select(s => s.IdBeneficiario).ToArray();
            
            foreach(Guid elem in listaTerceiros)
            {    
                var consultaTerceiro = (from bb in BeneficiarioBeneficios.AsNoTracking()
                                        join b in Beneficios.AsNoTracking() on bb.IdBeneficio equals b.IdBeneficio
                                        where bb.IdBeneficiario == elem
                                        select new
                                        {
                                            idProduto = b.IdBeneficio,
                                            beneficio = b.DescricaoBeneficio,
                                            status = bb.Entregue,
                                            quantidade = bb.Quantidade
                                        });
                var terceiro = (from b in Beneficiarios.AsNoTracking()
                                where b.IdBeneficiario == elem
                                select new
                                {
                                    codFuncionario = elem,
                                    nomeFuncionario = b.NomeCompleto,
                                    beneficios = consultaTerceiro.ToList()
                                });
                respostaTerceiro.AddRange(terceiro.ToList());
            };
            
            var resposta =  from beneficiario in Beneficiarios.AsNoTracking()
                            where beneficiario.IdBeneficiario == cod
                            select new
                            {
                                beneficios = respostaBeneficiario.ToArray(),
                                terceiros = respostaTerceiro
                             };

            return await resposta.ToListAsync();
        }

        public async Task<dynamic> GetBeneficiosParaEntregar(Guid idEvento, Guid idIlha, string identificacao)
        {
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios.AsNoTracking();
            IQueryable<EventoBeneficio> EventoBeneficios = _context.EventoBeneficios.AsNoTracking();
            IQueryable<EventoBeneficiario> EventoBeneficiarios = _context.EventoBeneficiarios.AsNoTracking();
            IQueryable<IlhaBeneficio> ilhaBeneficios = _context.IlhaBeneficios.AsNoTracking();
            IQueryable<Beneficiario> Beneficiarios = _context.Beneficiarios.AsNoTracking();
            IQueryable<Beneficio> Beneficios = _context.Beneficios.AsNoTracking();
            IQueryable<Terceiro> Terceiros = _context.Terceiros.AsNoTracking();
            Guid idTerceiro, idBeneficiario;
            
            try {
                if(identificacao.Length != 11) {
                    idBeneficiario = 
                    (from beneficiario in Beneficiarios
                    where beneficiario.Edv == Convert.ToInt32(identificacao)
                    select beneficiario.IdBeneficiario).First();
                }else {
                    string buscarIdentificacao = Convert.ToUInt64(identificacao).ToString(@"000\.000\.000\-00");

                    idBeneficiario = 
                        (from beneficiario in Beneficiarios
                        where beneficiario.Cpf == buscarIdentificacao
                        select beneficiario.IdBeneficiario).First();
                }
            } catch {
                idBeneficiario = Guid.Empty;
            }
            
            try {
                string buscarIdentificacao;

                if(identificacao.Length != 11) {
                    buscarIdentificacao = 
                    (from beneficiario in Beneficiarios
                    where beneficiario.Edv == Convert.ToInt32(identificacao)
                    select beneficiario.Cpf).First();
                } else {
                    buscarIdentificacao = Convert.ToUInt64(identificacao).ToString(@"000\.000\.000\-00");
                }

                idTerceiro = 
                    (from terceiro in Terceiros
                    where terceiro.Identificacao == buscarIdentificacao
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
            where (beneficiarioBeneficio.IdBeneficiario == idBeneficiario
                    || beneficiarioBeneficio.IdTerceiro == idTerceiro)
            select new {
                Beneficio = beneficio.DescricaoBeneficio,
                IdBeneficio = beneficio.IdBeneficio,
                Quantidade = beneficiarioBeneficio.Quantidade,
                Entregue = beneficiarioBeneficio.Entregue,
                IdBeneficiario = beneficiario.IdBeneficiario,
                Beneficiario = beneficiario.NomeCompleto
            };

            query = query.Where(result => 
                EventoBeneficiarios.Any(eb => eb.IdEvento == idEvento && eb.IdBeneficiario == result.IdBeneficiario));
            
            query = query.Where(result => 
                EventoBeneficios.Any(eb => eb.IdEvento == idEvento && eb.IdBeneficio == result.IdBeneficio));
            
            query = query.Where(result => 
                ilhaBeneficios.Any(eb => eb.IdIlha == idIlha && eb.IdBeneficio == result.IdBeneficio));

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

        public async Task carregarBeneficiarios(Guid idEvento, List<BeneficiarioPayload> BeneficiariosParaInserir)
        {
            IQueryable<EventoBeneficiario> eventoBeneficiarios = _context.EventoBeneficiarios.AsNoTracking();
            IQueryable<Beneficiario> beneficiarios = _context.Beneficiarios.AsNoTracking();

            List<Beneficiario> beneficiariosDoEvento = (
                from beneficiario in beneficiarios
                join eventoBeneficiario in eventoBeneficiarios
                on beneficiario.IdBeneficiario equals eventoBeneficiario.IdBeneficiario
                where eventoBeneficiario.IdEvento == idEvento
                select beneficiario
            ).ToList();
            
            BeneficiariosParaInserir = BeneficiariosParaInserir.Where(bpi => !beneficiariosDoEvento.Any(b => b.Cpf == bpi.cpf)).ToList();
            
            List<Beneficiario> beneficiariosRelacao = new List<Beneficiario>();
            foreach (var novoBeneficiario in BeneficiariosParaInserir)
            {
                Beneficiario beneficiario = new Beneficiario {
                    NomeCompleto = novoBeneficiario.nome,
                    Cpf = novoBeneficiario.cpf,
                    Edv = novoBeneficiario.edv,
                    Unidade = novoBeneficiario.unidade,
                    DataNascimento = novoBeneficiario.nascimento,
                    DataInclusao = DateTime.Now,
                    ResponsavelInclusao = "Import Process"
                };

                var beneficiarioRepetido = beneficiariosRelacao
                    .Any(beneficiarioNaLista => beneficiarioNaLista.Cpf == beneficiario.Cpf);

                if(!beneficiarioRepetido)
                {
                    var beneficiarioNaTabela = beneficiarios
                        .Where(beneficiarioNaTabela => beneficiarioNaTabela.Cpf == beneficiario.Cpf).FirstOrDefault();

                    if(beneficiarioNaTabela == null)
                    {
                        beneficiariosRelacao.Add(beneficiario);
                        _context.Beneficiarios.Add(beneficiario);
                    }else {
                        beneficiariosRelacao.Add(beneficiarioNaTabela);
                    }
                }
            }

            await _context.SaveChangesAsync();

            foreach (Beneficiario beneficiario in beneficiariosRelacao)
            {
                EventoBeneficiario eventoBeneficiario = new EventoBeneficiario {
                    IdEvento = idEvento,
                    IdBeneficiario = beneficiario.IdBeneficiario
                };

                _context.EventoBeneficiarios.Add(eventoBeneficiario);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task carregarBeneficios(Guid idEvento, List<string> BeneficiosParaInserir)
        {
            IQueryable<EventoBeneficio> eventoBeneficios = _context.EventoBeneficios.AsNoTracking();
            IQueryable<Beneficio> beneficios = _context.Beneficios.AsNoTracking();

            List<Beneficio> beneficiosDoEvento = (
                from beneficio in beneficios
                join EventoBeneficio in eventoBeneficios
                on beneficio.IdBeneficio equals EventoBeneficio.IdBeneficio
                where EventoBeneficio.IdEvento == idEvento
                select beneficio
            ).ToList();
            
            BeneficiosParaInserir = BeneficiosParaInserir.Where(bpi => !beneficiosDoEvento.Any(b => b.DescricaoBeneficio == bpi)).ToList();
            
            List<Beneficio> beneficiosRelacao = new List<Beneficio>();
            foreach (var descricaoBeneficio in BeneficiosParaInserir)
            {
                Beneficio beneficio = new Beneficio {
                    DescricaoBeneficio = descricaoBeneficio
                };

                var beneficioRepetido = beneficiosRelacao
                    .Any(beneficioNaLista => beneficioNaLista.DescricaoBeneficio == beneficio.DescricaoBeneficio);

                if(!beneficioRepetido)
                {
                    var beneficioNaTabela = beneficios
                        .Where(beneficioNaTabela => beneficioNaTabela.DescricaoBeneficio == beneficio.DescricaoBeneficio).FirstOrDefault();

                    if(beneficioNaTabela == null)
                    {
                        beneficiosRelacao.Add(beneficio);
                        _context.Beneficios.Add(beneficio);
                    }else {
                        beneficiosRelacao.Add(beneficioNaTabela);
                    }
                }
            }

            await _context.SaveChangesAsync();

            foreach (Beneficio beneficio in beneficiosRelacao)
            {
                EventoBeneficio eventoBeneficio = new EventoBeneficio {
                    IdEvento = idEvento,
                    IdBeneficio = beneficio.IdBeneficio
                };

                _context.EventoBeneficios.Add(eventoBeneficio);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task carregarBeneficiarioBeneficio(Guid idEvento, Dictionary<string, List<CpfQuantidade>> BeneficioBeneficiario)
        {
            IQueryable<BeneficiarioBeneficio> beneficiarioBeneficios = _context.BeneficiarioBeneficios.AsNoTracking();

            List<Beneficiario> beneficiariosDoEvento = (
                from beneficiario in _context.Beneficiarios.AsNoTracking()
                join eventoBeneficiario in _context.EventoBeneficiarios.AsNoTracking()
                on beneficiario.IdBeneficiario equals eventoBeneficiario.IdBeneficiario
                where eventoBeneficiario.IdEvento == idEvento
                select beneficiario
            ).ToList();

            List<Beneficio> beneficiosDoEvento = (
                from beneficio in _context.Beneficios.AsNoTracking()
                join EventoBeneficio in _context.EventoBeneficios.AsNoTracking()
                on beneficio.IdBeneficio equals EventoBeneficio.IdBeneficio
                where EventoBeneficio.IdEvento == idEvento
                select beneficio
            ).ToList();

            foreach (KeyValuePair<string, List<CpfQuantidade>> entrada in BeneficioBeneficiario)
            {
                Beneficio? beneficio = beneficiosDoEvento.Where(b => b.DescricaoBeneficio == entrada.Key).FirstOrDefault();

                if(beneficio != null) {
                    foreach (CpfQuantidade cpfQuantidade in entrada.Value)
                    {
                        Beneficiario beneficiario = beneficiariosDoEvento.Where(b => b.Cpf == cpfQuantidade.cpf).First();

                        BeneficiarioBeneficio beneficiarioBeneficio = new BeneficiarioBeneficio {
                            IdBeneficiario = beneficiario.IdBeneficiario,
                            IdBeneficio = beneficio.IdBeneficio,
                            Quantidade = cpfQuantidade.Quantidade
                        };

                        var local = _context.BeneficiarioBeneficios.Local
                            .FirstOrDefault(beneficiarioBeneficioLocal => beneficiarioBeneficioLocal.IdBeneficiario == beneficiarioBeneficio.IdBeneficiario
                                && beneficiarioBeneficioLocal.IdBeneficio == beneficiarioBeneficio.IdBeneficio);

                        var beneficiarioBeneficioNaTabela = beneficiarioBeneficios
                            .Where(beneficiarioBeneficioNaTabela => beneficiarioBeneficioNaTabela.IdBeneficiario == beneficiarioBeneficio.IdBeneficiario
                                && beneficiarioBeneficioNaTabela.IdBeneficio == beneficiarioBeneficio.IdBeneficio).FirstOrDefault();

                        if(beneficiarioBeneficioNaTabela == null && local == null)
                        {
                            _context.BeneficiarioBeneficios.Add(beneficiarioBeneficio);
                        }
                    }
                }
            }
            
            await _context.SaveChangesAsync();
        }
    }
    
}
