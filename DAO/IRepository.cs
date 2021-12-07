using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
         Task<Beneficiario[]> GetAllBeneficiarios();

         Task<Evento[]> GetAllEventos();

         Task<dynamic> GetBeneficosFromEvento(Guid EventoId);

         Task<dynamic> GetUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId);

         Task<dynamic> GetEventobyId(Guid EventoId);

         Task RemoverEvento(Guid EventoId);
         Task RemoverUmBeneficioFromEvento(Guid EventoId, Guid BeneficioId);

         Task<dynamic> GetEvento(Evento evento);

         Task inserirEvento(Evento evento);
    }
}