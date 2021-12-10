using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        
        Task<bool> SaveChangesAsync();
        Task<Beneficiario[]> GetAllBeneficiarios();

        Task<Evento[]> GetAllEventos();

        Task<EventoBeneficiario[]> GetAllEventoBeneficiarios();

        Task<dynamic> GetBeneficiosFromEvento(Guid EventoId);

        Task<dynamic> GetUmBeneficioFromEventobyId(Guid EventoId, Guid BeneficioId);

        Task<dynamic> GetUmBeneficioFromEvento(Guid EventoId, Beneficio beneficio);

        Task<dynamic> GetEventobyId(Guid EventoId);

        Task RemoverEvento(Guid EventoId);  

        Task RemoverUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId);       

        Task<dynamic> GetEvento(Evento evento);

        Task<dynamic> GetLogin(Login login);
        Task<dynamic> GetBeneficios(Guid cod);

        Task inserirEvento(Evento evento);

        Task EditarEvento(Guid EventoId, Evento novoEvento);

        Task inserirBeneficio(Beneficio beneficio);

        Task inserirBeneficioEvento(Guid EventoId,Beneficio beneficio);

        Task editarBenficioFromEvento(Guid EvendoId, Guid BeneficioId, Beneficio Descbeneficio);

        Task<dynamic> GetBeneficiosParaEntregar(Guid idEvento, Guid idIlha, string identificacao);
        
        void entregarBeneficios(BeneficiarioBeneficioEntregar beneficioEntregue);

        Task carregarBeneficiarios(Guid idEvento, List<BeneficiarioPayload> Beneficiarios);

        Task carregarBeneficios(Guid idEvento, List<string> Beneficios);

        Task carregarBeneficiarioBeneficio(Guid idEvento, Dictionary<string, List<CpfQuantidade>> BeneficioBeneficiario);
    }
}