using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        CalificacionDAO calificacionDAO = new CalificacionDAO();

        [HttpGet("calificaciones")]
        public List<Calificacion> CalificacionesMatricula(int id_matricula)
        {
            return calificacionDAO.CalificacionesMatricula(id_matricula);
        }

        [HttpPost("calificacion")]
        public bool Add([FromBody] Calificacion calif)
        {
            return calificacionDAO.Add(calif);
        }
    }
}
