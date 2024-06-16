using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        // Creamos nuestros endpoints 
        [HttpGet(Name = "prueba")]
        public string pruebaApi()
        {
            return "Esto es una prueba de mi API.";
        }
    }
}
