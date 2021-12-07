using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
         Task<Beneficiario[]> GetBeneficiarios();
         Task<Object[]> getBeneficiarioBeneficios(string identificacao);
    }
}