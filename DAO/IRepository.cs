using System.Threading.Tasks;
using backend_api.Models;
using webAPI.Models;

namespace webAPI.DAO
{
    public interface IRepository
    {
         Task<Beneficiario[]> GetBeneficiarios();
         Task<dynamic> GetLogin(Login login);
    }
}