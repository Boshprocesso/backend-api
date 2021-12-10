using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
using webAPI.Models;


namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("beneficiario")]
    public class TerceirosController : ControllerBase
    {        

        private readonly IRepository _repo;

        public TerceirosController(IRepository repo){
            _repo = repo;
        }
        [HttpGet("{id}")]
        public async Task <IActionResult> GetTerceiro(Guid id)
        {
            try{
                var result = await _repo.GetTerceiro(id);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest(null);  
            }
            catch (Exception ex){
                return Ok (null);
            }
            
        }
        [HttpPost("Terceiro")]
        public async Task <IActionResult> inserirTerceiro(TerceiroModel terceiro)
        {
            try{
                var result = await _repo.inserirTerceiro(terceiro);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest(null);  
            }
            catch (Exception ex){
                return Ok ($"Erro: {ex.Message}");
            }
            
        }
        [HttpDelete("delete/{idBeneficiario}/{idTerceiro}")]
        public async Task <IActionResult> removerTerceiro(Guid idBeneficiario, string identificacaoTerceiro)
        {
            try{
                var result = await _repo.removerTerceiro(idBeneficiario,identificacaoTerceiro);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest("deu ruim");  
            }
            catch (Exception ex){
                return Ok ($"Erro: {ex.Message}");
            }
            
        }
    }

}