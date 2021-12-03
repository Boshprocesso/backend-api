using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class BeneficiarioController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {

        return Ok("Beneficiario Ok!");
    }
  } 
}
