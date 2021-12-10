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
            List<Beneficiario> ColaboradoresEvento = (
                from beneficiarios in _context.Beneficiarios                
                join beneficiariosEvento in _context.EventoBeneficiarios on beneficiarios.IdBeneficiario  equals beneficiariosEvento.IdBeneficiario            
                where beneficiariosEvento.IdEvento == EventoId                 
                select beneficiarios).ToList();
            
            List<BenficiarioAux> ListacompletaColaboradoresEvento = new List<BenficiarioAux>();
            
            
            foreach (Beneficiario colaborador in ColaboradoresEvento)
            {
                BenficiarioAux beneficiarioaux = new BenficiarioAux();
                
                beneficiarioaux.EventoId = EventoId;
                beneficiarioaux.colaborador = colaborador;                
                beneficiarioaux.ListaBeneficios = (                                    
                                            from benficioscolaborador in  _context.BeneficiarioBeneficios 
                                            join beneficios in _context.Beneficios on benficioscolaborador.IdBeneficio equals beneficios.IdBeneficio
                                            where benficioscolaborador.IdBeneficiario == colaborador.IdBeneficiario && benficioscolaborador.IdBeneficio == beneficios.IdBeneficio
                                        select new
                                        {                                       
                                        idproduto = beneficios.IdBeneficio,
                                        descricao = beneficios.DescricaoBeneficio,
                                        quantidade = benficioscolaborador.Quantidade,
                                        status = benficioscolaborador.Entregue}).ToList<dynamic>();
                
                ListacompletaColaboradoresEvento.Add(beneficiarioaux);
                                        
                

            }
         return ListacompletaColaboradoresEvento;      
                
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
                                  
                if (Convert.ToString(data.FirstOrDefault()).Split(' ')[0] == String.Join('/',nascimentoLogin))
                {
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
                var resposta = (from beneficiario in consultabb.AsNoTracking()
                            where beneficiario.Edv == Convert.ToInt32(login.cod)
                            select new 
                            {
                                codFuncionario = beneficiario.IdBeneficiario,
                                nomeFuncionario = beneficiario.NomeCompleto,
                                nascimento = beneficiario.DataNascimento,
                                administrativo = true,
                                entregaproduto = true
                            });
                            
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
                        novoTerceiro.DataIndicacao = DateTime.Now;

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

        public void entregarBeneficios(BeneficiarioBeneficioEntregar beneficioEntregue)
        {
            IQueryable<BeneficiarioBeneficio> BeneficiarioBeneficios = _context.BeneficiarioBeneficios.AsNoTracking();

            var beneficioTabela =
                (from beneficiarioBeneficio in BeneficiarioBeneficios
                where beneficioEntregue.IdBeneficiario == beneficiarioBeneficio.IdBeneficiario && beneficioEntregue.IdBeneficio == beneficiarioBeneficio.IdBeneficio
                    && beneficiarioBeneficio.Entregue == "0"
                select beneficiarioBeneficio).FirstOrDefault();

                if(beneficioTabela != null) {
                    beneficioTabela.Entregue = "1";
                    _context.BeneficiarioBeneficios.Update(beneficioTabela);
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
