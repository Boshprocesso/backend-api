using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webAPI.Models;
using System;



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
        
        
// EVENTOS
        public async Task<Evento[]> GetAllEventos()
        {
            IQueryable<Evento> query = _context.Eventos;
            query = query.AsNoTracking().OrderBy(c => c.IdEvento);
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

        // BENEFICIO 
        public async Task<Beneficio[]> GetAllBeneficios()
        {
            IQueryable<Beneficio> query = _context.Beneficios;
            query = query.AsNoTracking().OrderBy(c => c.IdBeneficio);
            return await query.ToArrayAsync();
        }
        public async Task inserirBeneficio(Beneficio beneficio){

            _context.Beneficios.Add(beneficio);
            await _context.SaveChangesAsync();
        }
        public async Task DeletarBeneficio (Guid BeneficioId){

            IQueryable<Beneficio> consultabb = _context.Beneficios; 
            
            var busca = (from c in consultabb
            where c.IdBeneficio == BeneficioId
            select c).FirstOrDefault();               
            _context.Beneficios.Remove(busca);
            await _context.SaveChangesAsync(); 

        }

        public async Task<dynamic> GetBeneficiobyId(Guid BeneficioId){

             IQueryable<Beneficio> consultabb = _context.Beneficios;

            var resposta = (from c in consultabb
                            where c.IdBeneficio == BeneficioId
                            select c);                            
                
                resposta = resposta.AsNoTracking();
                return await resposta.ToArrayAsync();
                //return resposta;
         }
        




        // BENEFICIO -> EVENTO 
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
        public async Task RemoverBeneficioFromEventoBeneficio(Guid BeneficioId){

            IQueryable<EventoBeneficio> consultaeb = _context.EventoBeneficios; 
            
            var busca = (from c in consultaeb
            where c.IdBeneficio == BeneficioId
            select c.IdBeneficio).ToList();

            foreach(Guid Id in busca){

                var query = (from c in consultaeb
                            where c.IdBeneficio == Id
                            select c).First();

            _context.EventoBeneficios.Remove(query);            
            }               
            
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
        public async Task editarBenficio(Guid BeneficioId, Beneficio Descbeneficio)
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

        public async Task RemoverEventoFromEventoBeneficio(Guid EventoId){

            IQueryable<EventoBeneficio> consultaeb = _context.EventoBeneficios; 
            
            var busca = (from c in consultaeb
            where c.IdEvento == EventoId
            select c.IdEvento).ToList();

            foreach(Guid Id in busca){

                var query = (from c in consultaeb
                            where c.IdEvento == Id
                            select c).First();

            _context.EventoBeneficios.Remove(query);            
            }               
            
            await _context.SaveChangesAsync();

        }



        // COLABORADOR

        public async Task<dynamic> GetAllColaboradores(){

            IQueryable<Beneficiario> query = _context.Beneficiarios;
            query = query.AsNoTracking().OrderBy(c => c.IdBeneficiario);
            return await query.ToArrayAsync();
        }

        public async Task<dynamic> GetColaboradorbyEdv(int edv){

            IQueryable<Beneficiario> consultabb = _context.Beneficiarios;

            var resposta = (from c in consultabb
                            where c.Edv == edv
                            select c);                            
                
                resposta = resposta.AsNoTracking();
                return await resposta.ToArrayAsync();
        }

        public async Task<dynamic> GetColaboradorbyId(Guid ColabId){

            IQueryable<Beneficiario> consultabb = _context.Beneficiarios;

            var resposta = (from c in consultabb
                            where c.IdBeneficiario == ColabId
                            select c);                            
                
                resposta = resposta.AsNoTracking();
                return await resposta.ToArrayAsync();

        }
        public async Task inserirColaborador(Beneficiario colaborador){

            _context.Beneficiarios.Add(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task EditarColaborador (int edv, Beneficiario colaborador){

             IQueryable<Beneficiario> benefconsulta = _context.Beneficiarios;                

                var query = (
                from b in benefconsulta        
                where b.Edv == edv 
                select b.IdBeneficiario).First();

                Beneficiario colab = new Beneficiario();
                colab.NomeCompleto = colaborador.NomeCompleto;
                colab.Cpf = colaborador.Cpf;
                colab.DataNascimento = colaborador.DataNascimento;
                colab.Unidade=colaborador.Unidade;                
                colab.DataInclusao = colaborador.DataInclusao;
                colab.ResponsavelInclusao = colaborador.ResponsavelInclusao;
                colab.Edv = edv;
                colab.IdBeneficiario = query;         

                _context.Beneficiarios.Update(colab);
                await _context.SaveChangesAsync();
        }

        public async Task DeletarColaborador (int edv)
        {

            IQueryable<Beneficiario> consultabb = _context.Beneficiarios; 
            
            var busca = (from c in consultabb
            where c.Edv == edv
            select c).FirstOrDefault();               
            _context.Beneficiarios.Remove(busca);
            await _context.SaveChangesAsync(); 

        }



        //BENEFICIO -> COLABORADOR

        private Guid GetIdfromColaborador(int edv)
        {
            IQueryable<Beneficiario> Colabconsulta = _context.Beneficiarios;

            var queryColaborador = (
                from c in Colabconsulta        
                where c.Edv == edv 
                select c.IdBeneficiario).First();

                return queryColaborador;
        }

        public async Task inserirBeneficioColaborador(int edv,Beneficio beneficio){


            IQueryable<BeneficiarioBeneficio> consulta = _context.BeneficiarioBeneficios; 
            IQueryable<Beneficio> benefconsulta = _context.Beneficios;
            IQueryable<Beneficiario> Colabconsulta = _context.Beneficiarios;


           var queryBeneficio = (
                from b in benefconsulta        
                where b.DescricaoBeneficio == beneficio.DescricaoBeneficio 
                select b.IdBeneficio).First();
            
            var queryColaborador = (
                from c in Colabconsulta        
                where c.Edv == edv 
                select c.IdBeneficiario).First();



            BeneficiarioBeneficio BB = new BeneficiarioBeneficio();
            BB.IdBeneficiario = queryColaborador;
            BB.IdBeneficio = queryBeneficio;

            _context.BeneficiarioBeneficios.Add(BB);
            await _context.SaveChangesAsync();
            
        }
        
        public async Task<dynamic> GetBeneficiosFromColaborador(int edv){

        IQueryable<BeneficiarioBeneficio> consultabb = _context.BeneficiarioBeneficios; 
        IQueryable<Beneficio> benefconsultabb = _context.Beneficios;

            var colabId = GetIdfromColaborador(edv);

            var queryBeneficios = (
                from b in benefconsultabb                
                join bb in consultabb on b.IdBeneficio equals bb.IdBeneficio
                where bb.IdBeneficiario == colabId
                select new  {
                    codFuncionario = edv,
                    IdBeneficio = b.IdBeneficio,
                    descricaoBeneficio = b.DescricaoBeneficio
                });

            queryBeneficios = queryBeneficios.AsNoTracking().OrderBy(c => c.IdBeneficio);         
            return await queryBeneficios.ToArrayAsync();
        }

        public async Task RemoverColaboradorfromBeneficiarioBeneficios(int edv){
             
             var colabId = GetIdfromColaborador(edv);

             IQueryable<BeneficiarioBeneficio> consultaeb = _context.BeneficiarioBeneficios; 
            
            var busca = (from c in consultaeb
            where c.IdBeneficiario == colabId
            select c.IdBeneficiario).ToList();

            foreach(Guid Id in busca){

                var query = (from c in consultaeb
                            where c.IdBeneficiario == Id
                            select c).First();

            _context.BeneficiarioBeneficios.Remove(query);            
            }               
            
            await _context.SaveChangesAsync();
             
             }


        // EVENTO -> COLABORADOR

        public async Task<dynamic> GetAllBeneficiariosEvento(Guid EventoId)
        {
            

            IQueryable<EventoBeneficiario> consultaeb = _context.EventoBeneficiarios; 
            IQueryable<Beneficiario> consultaColab = _context.Beneficiarios;

            var query = (
                from b in consultaColab               
                join eb in consultaeb on b.IdBeneficiario equals eb.IdBeneficiario
                where eb.IdEvento == EventoId
                select new  {
                    
                    IdEvento = eb.IdEvento,
                    Edv = b.Edv,
                    NomeCompleto = b.NomeCompleto,
                    Cpf = b.Cpf,                    
                    Unidade=b.Unidade,                
                    DataInclusao = b.DataInclusao                    
                });

            query = query.OrderBy(c => c.Edv);         
            return await query.ToArrayAsync();
        }

        public async Task inserirBeneficiarioEvento(Guid EventoId,int edv){

            IQueryable<EventoBeneficiario> consulta = _context.EventoBeneficiarios; 
            IQueryable<Beneficiario> benefconsulta = _context.Beneficiarios;
            IQueryable<Evento> Eventoconsulta = _context.Eventos;

            Guid ColabId = GetIdfromColaborador(edv);


           var queryColaborador = (
                from b in benefconsulta        
                where b.IdBeneficiario == ColabId 
                select b.IdBeneficiario).First();
            
            var queryEvento = (
                from c in Eventoconsulta        
                where c.IdEvento == EventoId 
                select c.IdEvento).First();



            EventoBeneficiario BB = new EventoBeneficiario();
            BB.IdBeneficiario = queryColaborador;
            BB.IdEvento = queryEvento;

            _context.EventoBeneficiarios.Add(BB);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverUmBeneficiarioFromEvento(Guid EventoId, int edv)
        {
            var colabId = GetIdfromColaborador(edv);

             IQueryable<EventoBeneficiario> consultaeb = _context.EventoBeneficiarios;             
            
            var busca = (from c in consultaeb
            where c.IdEvento == EventoId &&
            c.IdBeneficiario == colabId
            select c).FirstOrDefault(); 

            _context.EventoBeneficiarios.Remove(busca);
            await _context.SaveChangesAsync();
             
             }
        
        public async Task<dynamic> GetUmColaboradorFromEventobyEdv(Guid EventoId, int edv){

            IQueryable<EventoBeneficiario> consulta = _context.EventoBeneficiarios; 
            IQueryable<Beneficiario> benefconsulta = _context.Beneficiarios;

            Guid colabId = GetIdfromColaborador(edv);

            var busca = (
                from b in benefconsulta
                where b.IdBeneficiario == colabId                
                select b);

                var query = (
                from b in benefconsulta                
                join eb in consulta on colabId equals eb.IdBeneficiario
                where eb.IdEvento == EventoId &&
                eb.IdBeneficiario == colabId
                select new  {                    
                    IdEvento = eb.IdEvento,
                    Edv = b.Edv,
                    NomeCompleto = b.NomeCompleto,
                    Cpf = b.Cpf,                    
                    Unidade=b.Unidade,                
                    DataInclusao = b.DataInclusao              
                    
                });

            query = query.AsNoTracking().OrderBy(c => c.Edv);         
            return await query.ToArrayAsync();
        }

        /*public async Task <dynamic> GetAllBeneficiariosBeneficiosFromEvento(Guid EventoId){

            //GetBeneficiariosEvento
            IQueryable<EventoBeneficiario> consultaeb = _context.EventoBeneficiarios; 
            IQueryable<Beneficiario> consultaColab = _context.Beneficiarios;

            var query = (
                from eb in consultaeb              
                where eb.IdEvento == EventoId
                select eb.IdBeneficiario).ToList();

            //GetEventoBeneficiario
                        

            foreach( Guid colab in query){
                await GetColaboradorbyId(colab);
                GetBeneficiosFromColaborador
                
            }

            

                

            
        }*/
                

        

        

        

        





        // GUSTAVO //

        public async Task<dynamic> GetLogin(Login login)
        {   
            IQueryable<DateTime?>? data;
            String[]? nascimentoLogin;
            IQueryable<Beneficiario> consultabb = _context.Beneficiarios;
            if(login.cod.Length == 11)
            { 
                data = (from Beneficiario in consultabb.AsNoTracking()
                            where Beneficiario.Cpf == login.cod 
                            select Beneficiario.DataNascimento);
                nascimentoLogin = login.nascimento.Split('-');
                Array.Reverse(nascimentoLogin);
                

                login.cod = login.cod.Trim();
                if(login.cod.Length == 11)
                {
                    login.cod = login.cod.Insert(9,"-");
                    login.cod = login.cod.Insert(6,".");
                    login.cod = login.cod.Insert(3,".");
                }
                                  
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
                            idBeneficio = b.IdBeneficio,
                            descricaoBeneficio = b.DescricaoBeneficio,
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
                                            idBeneficio = b.IdBeneficio,
                                            descricaoBeneficio = b.DescricaoBeneficio,
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

<<<<<<< HEAD
    // VINICIUS //
=======
        public async Task<dynamic> GetTerceiro(Guid cod)
        {
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios;
            IQueryable<Beneficiario> Beneficiarios = _context.Beneficiarios;
            IQueryable<Terceiro> Terceiros = _context.Terceiros;

            var consultaTerceiro = (from bb in BeneficiarioBeneficios.AsNoTracking()
                                    join t in Terceiros.AsNoTracking() on bb.IdTerceiro equals t.IdTerceiro
                                    where bb.IdBeneficiario == cod
                                    select new
                                    {
                                        identificacaoTerceiro = t.Identificacao,
                                        nomeTerceiro = t.Nome
                                    });
            var terceiro = consultaTerceiro.FirstOrDefault();
            
            return terceiro;
            
        }
        public async Task<dynamic> inserirTerceiro(TerceiroModel terceiro)
        {   
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios;
            IQueryable<Beneficiario> Beneficiarios = _context.Beneficiarios;
            IQueryable<Terceiro> Terceiros = _context.Terceiros;
            
            Guid codTerceiro;

            if(terceiro.opcaoSelecionada == "cpf")
            {   
                terceiro.identificacaoTerceiro = terceiro.identificacaoTerceiro.Trim();
                if(terceiro.identificacaoTerceiro.Length == 11)
                {
                    terceiro.identificacaoTerceiro = terceiro.identificacaoTerceiro.Insert(9,"-");
                    terceiro.identificacaoTerceiro = terceiro.identificacaoTerceiro.Insert(6,".");
                    terceiro.identificacaoTerceiro = terceiro.identificacaoTerceiro.Insert(3,".");
                }
                
                
                if((from b in Beneficiarios select b.Cpf).ToList().Contains(terceiro.identificacaoTerceiro))
                {
                    codTerceiro = (from b in Beneficiarios 
                                    where b.Cpf == terceiro.identificacaoTerceiro
                                    select b.IdBeneficiario).FirstOrDefault();

                    if(!((from t in Terceiros select t.IdTerceiro).ToList().Contains(codTerceiro)))
                    {
                        Terceiro novoTerceiro = new Terceiro();
                        novoTerceiro.IdTerceiro = codTerceiro;
                        novoTerceiro.Identificacao = terceiro.identificacaoTerceiro;
                        novoTerceiro.Nome = (from b in Beneficiarios where b.IdBeneficiario == codTerceiro select b.NomeCompleto).ToArray().FirstOrDefault();
                        novoTerceiro.DataIndicacao = DateTime.Today;

                        _context.Terceiros.Add(novoTerceiro);
                    }
                    (from bb in BeneficiarioBeneficios 
                        where bb.IdBeneficiario == terceiro.cod select bb).ToList()
                                                                                    .ForEach(elem => elem.IdTerceiro = codTerceiro);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if(!((from t in Terceiros select t.Identificacao).ToList().Contains(terceiro.identificacaoTerceiro)))
                    {
                        Terceiro novoTerceiro = new Terceiro();
                        novoTerceiro.Identificacao = terceiro.identificacaoTerceiro;
                        novoTerceiro.Nome = terceiro.nomeTerceiro;
                        novoTerceiro.DataIndicacao = DateTime.Today; 

                        _context.Terceiros.Add(novoTerceiro);
                        await _context.SaveChangesAsync();
                    }
                    codTerceiro = (from t in Terceiros where t.Identificacao == terceiro.identificacaoTerceiro select t.IdTerceiro).ToArray().FirstOrDefault();
                    (from bb in BeneficiarioBeneficios 
                        where bb.IdBeneficiario == terceiro.cod select bb).ToList()
                                                                          .ForEach(elem => elem.IdTerceiro = ((from t in Terceiros
                                                                                                               where t.Identificacao == terceiro.identificacaoTerceiro
                                                                                                               select t.IdTerceiro).ToArray().FirstOrDefault()));
                    
                }
            }
           else
           {
                var edv = Convert.ToInt32(terceiro.identificacaoTerceiro);
                codTerceiro = (from b in Beneficiarios 
                                   where b.Edv == edv
                                   select b.IdBeneficiario).FirstOrDefault();

                if(!((from t in Terceiros select t.IdTerceiro).ToList().Contains(codTerceiro)))
                {
                    Terceiro novoTerceiro = new Terceiro();
                    novoTerceiro.IdTerceiro = codTerceiro;
                    novoTerceiro.Identificacao = terceiro.identificacaoTerceiro;
                    novoTerceiro.Nome = (from b in Beneficiarios where b.IdBeneficiario == codTerceiro select b.NomeCompleto).ToArray().FirstOrDefault();
                    novoTerceiro.DataIndicacao = DateTime.Today;

                    _context.Terceiros.Add(novoTerceiro);
                }
                (from bb in BeneficiarioBeneficios 
                    where bb.IdBeneficiario == terceiro.cod select bb).ToList()
                                                                      .ForEach(elem => elem.IdTerceiro = codTerceiro);
            }
            await _context.SaveChangesAsync();

            var resposta = (from t in Terceiros
                            where t.IdTerceiro == codTerceiro
                            select new
                            {
                                identificacaoTerceiro = t.Identificacao,
                                nomeTerceiro = t.Nome
                            });

            return await resposta.ToArrayAsync();
        }

        public async Task<dynamic> removerTerceiro(Guid idBeneficiario, string identificacaoTerceiro)
        {
            IQueryable<Terceiro> Terceiros = _context.Terceiros;
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios;

            var codTerceiro = (from t in Terceiros where t.Identificacao == identificacaoTerceiro select t.IdTerceiro).ToList().FirstOrDefault();

            (from bb in BeneficiarioBeneficios 
             where bb.IdBeneficiario == idBeneficiario && bb.IdTerceiro == codTerceiro
             select bb).ToList()
                       .ForEach(elem => elem.IdTerceiro = null); 
                        
            await _context.SaveChangesAsync();
            
            return ((from t in Terceiros where t.IdTerceiro == codTerceiro select new{
                                                                                        identificaoTerceiro = " ",
                                                                                        nomeTerceiro = " "
                                                                                     }));
        }
>>>>>>> 5a574cda921c3945881d356c9ded9e977b7a7aef
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
