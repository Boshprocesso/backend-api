using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
         Task<Beneficiario[]> GetAllBeneficiarios();

         Task<Evento[]> GetAllEventos();

         Task<EventoBeneficio[]> GetBeneficoFromEvento(Guid EventoId);

    }
}