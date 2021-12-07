using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;

namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class OperacionalController : ControllerBase
    {        

        private readonly IRepository _repo;

        public OperacionalController(IRepository repo){
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name="identificacao")] string? identificacao)
        {
            if(identificacao == null)
            {
                return StatusCode(400, "Informe a identificação");
            }
            
            try{
                var result = await _repo.getBeneficiarioBeneficios(identificacao);
                return Ok(result);
            }
            catch (Exception ex){
                return StatusCode(500 , $"Erro: {ex.Message}");
            }
            
        }
    }

}