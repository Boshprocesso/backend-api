using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class CargaDeDados : ControllerBase
    {        

        private readonly IRepository _repo;

        public CargaDeDados(IRepository repo){
            _repo = repo;
        }
        
        [HttpPost]
        public async Task<IActionResult> entregarBeneficios(Payload payload)
        {
            if(payload == null)
            {
                return StatusCode(400, "Informe o payload de dados");
            }

            if(payload.Beneficiarios == null || payload.Beneficios == null || payload.BeneficioBeneficiario == null)
            {
                return StatusCode(400, "Está faltando uma ou mais informações no payload");
            }
            
            try{
                Guid idEvento = payload.IdEvento;
                
                await _repo.carregarBeneficios(idEvento, payload.Beneficios);

                await _repo.carregarBeneficiarios(idEvento, payload.Beneficiarios);
                 
                await _repo.carregarBeneficiarioBeneficio(idEvento, payload.BeneficioBeneficiario);

                return Ok(new { status = "ok" });
            }
            catch (Exception ex){
                return StatusCode(500 , $"Erro: {ex.Message}");
            }
        }
    }

}