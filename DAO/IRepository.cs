using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
        Task<Beneficiario[]> GetAllBeneficiarios();

        Task<Evento[]> GetAllEventos();

        Task<Beneficio[]> GetBeneficosFromEvento(Guid EventoId);

        Task<dynamic> GetUmBeneficioFromEventobyId(Guid EventoId, Guid BeneficioId);

        Task<dynamic> GetUmBeneficioFromEvento(Guid EventoId, Beneficio beneficio);

        Task<dynamic> GetEventobyId(Guid EventoId);

        Task RemoverEvento(Guid EventoId);  

        Task RemoverUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId);       

        Task<dynamic> GetEvento(Evento evento);


         Task<dynamic> GetLogin(Login login);
         Task<dynamic> GetBeneficios(string cpf);

        Task inserirEvento(Evento evento);

        Task inserirBeneficio(Beneficio beneficio);

        Task inserirBeneficioEvento(Guid EventoId,Beneficio beneficio);

        Task<dynamic> GetBeneficiosParaEntregar(string identificacao);
        
        void entregarBeneficios(List<BeneficiarioBeneficioEntregar> beneficiosEntregues);

        Task carregarBeneficiarios(List<BeneficiarioPayload> Beneficiarios);

        Task carregarBeneficios(List<string> Beneficios);

        void carregarRelacoes(List<Beneficio> Beneficios, List<Beneficiario> Beneficiarios, Dictionary<string, List<CpfQuantidade>> BeneficioBeneficiario, Guid idEvento);
    }
}