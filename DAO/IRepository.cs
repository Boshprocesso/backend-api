using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        
        Task<bool> SaveChangesAsync();
        

        

        

        // Evento 
        Task<Evento[]> GetAllEventos();

        Task inserirEvento(Evento evento);
        Task EditarEvento(Guid EventoId, Evento novoEvento);
        Task RemoverEvento(Guid EventoId);

        Task<dynamic> GetEvento(Evento evento);
        Task<dynamic> GetEventobyId(Guid EventoId);

        // Beneficio

        Task<Beneficio[]> GetAllBeneficios();

        Task<dynamic> GetBeneficios(Guid cod);
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

        Task inserirBeneficioColaborador(int edv,Beneficio beneficio);

        Task<dynamic> GetBeneficiosFromColaborador(int edv); 

        //Task<dynamic> GetUmBeneficioFromColaboradorbyId(Guid BeneficiarioId, Guid BeneficioId);//FALTA

        //Task<dynamic> GetUmBeneficioFromColaborador(Guid BeneficiarioId, Beneficio beneficio);//FALTA          

        //Task RemoverUmBeneficioFromColaborador(Guid BeneficiarioId, Guid BeneficioId);//FALTA

        Task RemoverColaboradorfromBeneficiarioBeneficios(int edv); 

        // Colaborador - Evento        
        Task<dynamic> GetAllBeneficiariosEvento(Guid EventoId);

        

        Task<dynamic> GetUmColaboradorFromEventobyEdv(Guid EventoId, int edv);

        //Task<dynamic> GetUmColaboradorFromEvento(Guid EventoId, Beneficiario colaborador);//FALTA          

       Task RemoverUmBeneficiarioFromEvento(Guid EventoId, int edv); 
        Task inserirBeneficiarioEvento(Guid EventoId,int edv);
        //Task RemoverEventoFromEventoColaborador(Guid EventoId);

        //Evento -> Beneficiario -> Beneficio



       // Task <dynamic> GetAllBeneficiariosBeneficiosFromEvento(Guid EventoId); 


        //GUSTAVO
        
        

        Task<dynamic> GetLogin(Login login);    

        //VINICIUS
        
        
        Task<BeneficiarioBeneficioResgatar[]> GetBeneficiosParaEntregar(string identificacao);
        void entregarBeneficios(List<BeneficiarioBeneficioEntregar> beneficiosEntregues);

        
    }
}