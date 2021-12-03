using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class BeneficioController : ControllerBase

  {
   [HttpGet]
    public IActionResult Get()
    {

      return Ok("Beneficio Ok!");
    } 
  }
}