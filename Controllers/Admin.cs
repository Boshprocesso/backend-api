using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
using webAPI.Models;

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
        [HttpGet("BeneficiarioEventos")]
        public async Task<IActionResult> GetEventBeneficiarios()
        {
            try{
                var result = await _repo.GetAllEventoBeneficiarios();
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }
        [HttpGet("eventos")]
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

        [HttpGet("beneficiosEvento/{EventoId}")]
        public async Task<IActionResult> GetBeneficiosEvento(Guid EventoId)
        {
            try{
                var result = await _repo.GetBeneficiosFromEvento(EventoId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

        [HttpDelete("deleteEvento/{EventoId}")]
        public async Task<IActionResult> DeleteEvento(Guid EventoId)
        {
            try{
                var result = await _repo.GetEventobyId(EventoId);
                await _repo.RemoverEvento(EventoId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPost("adicionarEvento")]
        public async Task<IActionResult> AddEvento(Evento evento)
        {
            try{

                await _repo.inserirEvento(evento);
                var result = await _repo.GetEvento(evento);
                
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPut("EditarEvento/evento/{EventoId}")]
        public async Task<IActionResult> EditEvento(Guid EventoId, Evento novoEvento)
        {
            try{

                await _repo.EditarEvento(EventoId,novoEvento);                               
                var result = await _repo.GetEventobyId(EventoId);
                
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpDelete("deleteBeneficioEvento/evento/{EventoId}/beneficio/{BeneficioId}")]
        public async Task<IActionResult> DeletarBeneficioFromEvento(Guid EventoId, Guid BeneficioId)
        {
            try{

                
                var result = await _repo.GetUmBeneficioFromEventobyId(EventoId,BeneficioId);
                await _repo.RemoverUmBeneficioFromEvento(EventoId,BeneficioId);
                
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPost("adicionarBeneficioEvento/evento/{EventoId}")]
        public async Task<IActionResult> AddBeneficioEvento(Guid EventoId,Beneficio beneficio) 
        {
            try{

                await _repo.inserirBeneficio(beneficio);
                await _repo.inserirBeneficioEvento(EventoId,beneficio);               
                var result = await _repo.GetUmBeneficioFromEvento(EventoId,beneficio);
                
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPut("EditarBeneficio/evento/{EventoId}/beneficio/{BeneficioId}")]
        public async Task<IActionResult> EditBeneficio(Guid EventoId, Guid BeneficioId,Beneficio Descbeneficio) 
        {
            try{

                await _repo.editarBenficioFromEvento(EventoId,BeneficioId,Descbeneficio);                               
                var result = await _repo.GetUmBeneficioFromEventobyId(EventoId,BeneficioId);
                
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }
    }

}