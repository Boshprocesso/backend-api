using Microsoft.AspNetCore.Mvc;
namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class Beneficiario_beneficioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Beneficiario_beneficio Ok!");
        }
    }

}
