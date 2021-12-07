using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
         Task<Beneficiario[]> GetAllBeneficiarios();

         Task<Evento[]> GetAllEventos();

         Task<dynamic> GetBeneficoFromEvento(Guid EventoId);

         //Task RemoverEvento(Guid EventoId);
    }
}