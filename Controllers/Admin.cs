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

        // Evento
        
        [HttpGet("/eventos")]
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



        [HttpDelete("/deletarEvento/{EventoId}")]
        public async Task<IActionResult> DeleteEvento(Guid EventoId)
        {
            try{
                var result = await _repo.GetEventobyId(EventoId);
                await _repo.RemoverEventoFromEventoBeneficio(EventoId);
                await _repo.RemoverEvento(EventoId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }
        
        [HttpPost("/adicionarEvento")]
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
        [HttpPut("/editarEvento/evento/{EventoId}")]
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


        //Beneficio

        [HttpGet("/beneficios")]
        public async Task<IActionResult> GetBeneficios()
        {
            try{
                var result = await _repo.GetAllBeneficios();
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

        [HttpGet("/beneficiobyId")]
        public async Task<IActionResult> GetBeneficioporId(Guid BeneficioId)
        {
            try{
                var result = await _repo.GetBeneficiobyId(BeneficioId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }



        [HttpDelete("/deletarBeneficio/{BeneficioId}")]
        public async Task<IActionResult> DeleteBeneficio(Guid BeneficioId)
        {
            try{
                var result = await _repo.GetBeneficiobyId(BeneficioId);
                await _repo.RemoverBeneficioFromEventoBeneficio(BeneficioId);
                await _repo.DeletarBeneficio(BeneficioId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPost("/adicionarBeneficio")]
        public async Task<IActionResult> AddBeneficio(Beneficio beneficio)
        {
            try{

                await _repo.inserirBeneficio(beneficio);              
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPut("/editarBeneficio/{beneficioId}")]
        public async Task<IActionResult> EditarBeneficio(Guid beneficioId,Beneficio Descbeneficio)
        {
            try{

                await _repo.editarBenficio(beneficioId,Descbeneficio);              
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }
        
        //Beneficio -> Evento

        [HttpGet("/beneficiosEvento/{EventoId}")]
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


        
        [HttpDelete("/deletarBeneficioEvento/evento/{EventoId}/beneficio/{BeneficioId}")]
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
        
        [HttpPost("/adicionarBeneficioEvento/evento/{EventoId}")]
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
        
        [HttpPut("/EditarBeneficio/evento/{EventoId}/beneficio/{BeneficioId}")]
        public async Task<IActionResult> EditBeneficio(Guid EventoId, Guid BeneficioId,Beneficio Descbeneficio) 
        {
            try{

                await _repo.editarBenficio(BeneficioId,Descbeneficio);                             
                var result = await _repo.GetUmBeneficioFromEventobyId(EventoId,BeneficioId);
                
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }


        //Colaborador

        [HttpGet("/colaboradores")]

        public async Task<IActionResult> GetBeneficiarios()
        {
            try{
                var result = await _repo.GetAllColaboradores();
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }


        

        [HttpGet("/colaboradobyId/{ColabId}")]
        public async Task<IActionResult> GetBeneficiariobyId(Guid ColabId)
        {
            try{
                var result = await _repo.GetColaboradorbyId(ColabId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

        [HttpGet("/colaboradobyEdv/{edv}")]
        public async Task<IActionResult> GetBeneficiariobyEdv(int edv)
        {
            try{
                var result = await _repo.GetColaboradorbyEdv(edv);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

        [HttpPost("/adicionarColaborador")]
        public async Task<IActionResult> AddColaborador(Beneficiario colaborador)
        {
            try{

                await _repo.inserirColaborador(colaborador);              
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpPut("/editarColaborador/{edv}")]
        public async Task<IActionResult> EditarBeneficiario(int edv,Beneficiario colaborador)
        {
            try{

                await _repo.EditarColaborador(edv,colaborador);              
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpDelete("/deletarColaborador/edv/{edv}")]
        public async Task<IActionResult> DeleteColaborador(int edv)
        {
            try{
                var result = await _repo.GetColaboradorbyEdv(edv);
                await _repo.RemoverColaboradorfromBeneficiarioBeneficios(edv);
                await _repo.DeletarColaborador(edv);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        // COlaborador -> Beneficio

        [HttpPost("/adicionarBeneficioColaborador/edv/{edv}")]
        public async Task<IActionResult> AddBeneficioBeneficiario(int edv,Beneficio beneficio, int quantidade) 
        {
            try{

                
                await _repo.inserirBeneficioColaborador(edv,beneficio, quantidade);               
                
                
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpGet("/GetBeneficiosColaborador/edv/{edv}")]
        public async Task<IActionResult> ListarBeneficioBeneficiario(int edv) 
        {
            try{


                var result = await _repo.GetBeneficiosFromColaborador(edv);


                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        // Evento Beneficiario
        
        [HttpGet("/colaboradoresEvento/{EventoId}")]
        public async Task<IActionResult> GetEventBeneficiarios(Guid EventoId)
        {
            try{
                var result = await _repo.GetAllBeneficiariosEvento(EventoId);
                return Ok(result);
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            
        }

        [HttpPost("/adicionarColaboradorEvento/eventoid/{EventoId}")]

        public async Task<IActionResult> AddBeneficiarioEvento(Guid EventoId, BeneficiarioDoEvento beneficiarioDoEvento) 
        {
            if(beneficiarioDoEvento == null || beneficiarioDoEvento.colaborador == null || beneficiarioDoEvento.listaBeneficios == null) {
                return BadRequest("É necessário enviar as informações do Colaborador");
            }
            try{
                Beneficiario beneficiario = beneficiarioDoEvento.colaborador;
                List<BeneficioDoBeneficiario> beneficios = beneficiarioDoEvento.listaBeneficios;

                await _repo.inserirColaborador(beneficiario);

                beneficiario = (await _repo.GetColaboradorbyEdv(beneficiario.Edv.Value))[0];

                if(beneficios.Count > 0) {
                    foreach (var beneficio in beneficios)
                    {
                        Beneficio beneficioParaInserir = new Beneficio {
                            IdBeneficio = beneficio.IdBeneficio,
                            DescricaoBeneficio = beneficio.DescricaoBeneficio
                        };

                        await _repo.inserirBeneficioColaborador(beneficiario.Edv.Value, beneficioParaInserir, beneficio.quantidade);                        
                    }
                }

                await _repo.inserirBeneficiarioEvento(EventoId, beneficiario.Edv.Value);
                
                
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }

        [HttpDelete("/removerColaboradorEvento/eventoid/{EventoId}/edv/{edv}")]

        public async Task<IActionResult> DeleteBeneficiarioEvento(Guid EventoId,int edv) 
        {
            try{

                
                await _repo.RemoverUmBeneficiarioFromEvento(EventoId, edv);               
                
                
                return Ok();
            }
            catch (Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
                        
        }
        
        


        

        

        

        

        

        

        

        

        

        
    }

}