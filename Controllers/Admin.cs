using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {        

        private readonly IRepository _repo;

        public AdminController(IRepository repo){
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetBeneficiarios()
        {
            try{
                var result = await _repo.GetAllBeneficiarios();
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }
        [HttpGet("/evento")]
        public async Task<IActionResult> GetEvento()
        {
            try{
                var result = await _repo.GetAllEventos();
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

        [HttpGet("byEventoId/{EventoId}")]
        public async Task<IActionResult> GetBeneficiosEvento(Guid EventoId)
        {
            try{
                var result = await _repo.GetBeneficoFromEvento(EventoId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }
    }

}