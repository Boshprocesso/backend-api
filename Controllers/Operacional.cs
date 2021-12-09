using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
using webAPI.Models;

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
        [HttpGet("{idEvento}/{idIlha}")]
        public async Task<IActionResult> GetBeneficiosParaEntregar(Guid idEvento, Guid idIlha, [FromQuery(Name="identificacao")] string? identificacao)
        {
            if(identificacao == null)
            {
                return StatusCode(400, "Informe a identificação");
            }

            try{
                var result = await _repo.GetBeneficiosParaEntregar(idEvento, idIlha, identificacao);
                return Ok(result);
            }
            catch (Exception ex){
                return StatusCode(500 , $"Erro: {ex.Message}");
            }
            
        }
        
        [HttpPost("/BeneficioEntregue")]
        public async Task<IActionResult> entregarBeneficios(List<BeneficiarioBeneficioEntregar> beneficiosEntregues)
        {
            if(beneficiosEntregues.Count == 0)
            {
                return StatusCode(400, "Informe ao menos um benefício para entregua");
            }
            
            try{
                _repo.entregarBeneficios(beneficiosEntregues);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Beneficios entregues");
                }
            }
            catch (Exception ex){
                return StatusCode(500 , $"Erro: {ex.Message}");
            }

            return BadRequest("Esses benefícios já foram entregues");            
        }
    }

}