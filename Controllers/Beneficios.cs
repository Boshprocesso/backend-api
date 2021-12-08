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

        [HttpGet("/{codBeneficiario}")]
        public async Task <IActionResult> GetBeneficios(string codBeneficiario)
        {
            try{
                var result = await _repo.GetBeneficios(codBeneficiario);
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