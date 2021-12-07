using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
        Task<Beneficiario[]> GetBeneficiarios();
        Task<BeneficiarioBeneficioResgatar[]> GetBeneficiosParaEntregar(string identificacao);
        void entregarBeneficios(List<BeneficiarioBeneficioEntregar> beneficiosEntregues);
    }
}