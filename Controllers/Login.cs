using Microsoft.AspNetCore.Mvc;
using webAPI.DAO;
using System.Threading.Tasks;
using webAPI.Models;


namespace webAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {        

        private readonly IRepository _repo;

        public LoginController(IRepository repo){
            _repo = repo;
        }
        [HttpPost]
        public async Task <IActionResult> GetLogin(Login login)
        {
            try{
                var result = await _repo.GetLogin(login);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest(null);  
            }
            catch (Exception ex){
                return Ok(null);
            }
            
        }
    }

}