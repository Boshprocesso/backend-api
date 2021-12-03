using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class EventoController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {

      return Ok("Beneficio Ok!");
    }  
  }
}