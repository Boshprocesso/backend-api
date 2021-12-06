using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("admin/[controller]")]
    public class AdminController : ControllerBase
    {        

        private readonly IRepository _repo;

        public AdminController(IRepository repo){
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var result = await _repo.GetBeneficiarios();
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }
    }

}