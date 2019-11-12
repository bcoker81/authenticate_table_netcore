using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testmysql.Utilities;

namespace testmysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController: ControllerBase
    {
        [Route("hash"),HttpPost]
        public  IActionResult HashPassword([FromBody] string password){
            try
            {
                return Ok(AuthUtilities.HashPassword(password));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }
    }
}