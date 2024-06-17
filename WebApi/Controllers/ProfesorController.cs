using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        // ProfesorDAO de capa DAL
        public ProfesorDAO profesorDAO = new ProfesorDAO();

        [HttpPost("autenticacion")]
        public string login([FromBody] Profesor prof)
        {
            var profesor = profesorDAO.login(prof.Usuario, prof.Pass);

            if (profesor == null)
            {
                return "";
            }

            return profesor.Usuario;
        }
    }
}
