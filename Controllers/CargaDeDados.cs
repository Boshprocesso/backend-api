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

            if(payload.Beneficios == null || payload.BeneficioBeneficiario == null)
            {
                return StatusCode(400, "Está faltando uma ou mais informações no payload");
            }
            
            try{
                Guid idEvento = payload.IdEvento;
                if(payload.Beneficiarios != null)
                {
                    await _repo.carregarBeneficiarios(payload.Beneficiarios);
                }

                List<Beneficio> beneficios = await _repo.carregarBeneficios(idEvento, payload.Beneficios);

                List<Beneficiario> beneficiarios = (await _repo.GetAllBeneficiarios()).ToList();

                _repo.carregarEventoBeneficio(payload.IdEvento, beneficios);
                 
                _repo.carregarBeneficiarioBeneficio(beneficios, beneficiarios, payload.BeneficioBeneficiario);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Dados Carregados");
                }
            }
            catch (Exception ex){
                return StatusCode(500 , $"Erro: {ex.Message}");
            }

            return BadRequest();      
        }
    }

}