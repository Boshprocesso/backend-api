using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        
        Task<bool> SaveChangesAsync();
        Task<Beneficiario[]> GetAllBeneficiarios();

        

        

        // Evento 
        Task<Evento[]> GetAllEventos();

        Task inserirEvento(Evento evento);
        Task EditarEvento(Guid EventoId, Evento novoEvento);
        Task RemoverEvento(Guid EventoId);

        Task<dynamic> GetEvento(Evento evento);
        Task<dynamic> GetEventobyId(Guid EventoId);

        // Beneficio

        Task<Beneficio[]> GetAllBeneficios();

        Task inserirBeneficio(Beneficio beneficio);
        Task editarBenficio(Guid BeneficioId,Beneficio Descbeneficio); // Edita na tabalea Beneficio
        Task<dynamic> GetBeneficiobyId(Guid BeneficioId);
        Task DeletarBeneficio (Guid BeneficioId); // FALTA

        // Beneficio-Evento

        Task<dynamic> GetBeneficiosFromEvento(Guid EventoId);

        Task<dynamic> GetUmBeneficioFromEventobyId(Guid EventoId, Guid BeneficioId);

        Task<dynamic> GetUmBeneficioFromEvento(Guid EventoId, Beneficio beneficio);          

        Task RemoverUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId); 
        Task inserirBeneficioEvento(Guid EventoId,Beneficio beneficio);

        Task RemoverEventoFromEventoBeneficio(Guid EventoId);

        Task RemoverBeneficioFromEventoBeneficio(Guid BeneficioId);




        // Colaborador
        Task inserirColaborador(Beneficiario colaborador); 

        Task EditarColaborador (int edv,Beneficiario colaborador); 

        Task DeletarColaborador (int edv); 

        Task<dynamic> GetAllColaboradores();

        Task<dynamic> GetColaboradorbyEdv(int edv);


        Task<dynamic> GetColaboradorbyId(Guid ColabId);


        //Colaborador - Beneficio

        Task inserirBeneficioColaborador(int edv,Beneficio beneficio, int quantidade);

        Task<dynamic> GetBeneficiosFromColaborador(int edv); 

        

        Task RemoverColaboradorfromBeneficiarioBeneficios(int edv); 

        // Colaborador - Evento        
        Task<dynamic> GetAllBeneficiariosEvento(Guid EventoId);

        

        Task<dynamic> GetUmColaboradorFromEventobyEdv(Guid EventoId, int edv);

                  

       Task RemoverUmBeneficiarioFromEvento(Guid EventoId, int edv); 
        Task inserirBeneficiarioEvento(Guid EventoId,int edv); // Arrumar
        

        //Evento -> Beneficiario -> Beneficio




       


        //GUSTAVO
        
        
        Task<dynamic> GetLogin(Login login);
        Task<dynamic> GetBeneficios(Guid cod);
        Task<dynamic> GetTerceiro(Guid cod);
        Task<dynamic> inserirTerceiro(TerceiroModel terceiro);
        Task<dynamic> removerTerceiro(Guid idBeneficiario, string identificacaoTerceiro);

           

        //VINICIUS
        
        




        Task<dynamic> GetBeneficiosParaEntregar(Guid idEvento, Guid idIlha, string identificacao);
        
        void entregarBeneficios(BeneficiarioBeneficioEntregar beneficioEntregue);

        Task carregarBeneficiarios(Guid idEvento, List<BeneficiarioPayload> Beneficiarios);

        Task carregarBeneficios(Guid idEvento, List<string> Beneficios);

        Task carregarBeneficiarioBeneficio(Guid idEvento, Dictionary<string, List<CpfQuantidade>> BeneficioBeneficiario);

    }
}