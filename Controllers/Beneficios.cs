using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
using webAPI.Models;


namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("beneficio")]
    public class BeneficiosController : ControllerBase
    {        

        private readonly IRepository _repo;

        public BeneficiosController(IRepository repo){
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetBeneficios(Guid id)
        {
            try{
                var result = await _repo.GetBeneficios(id);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest(null);  
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

}
}