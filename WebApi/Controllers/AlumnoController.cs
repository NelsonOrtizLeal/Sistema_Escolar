using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        // Acceso a nuestra capa de operaciones DAO
        private AlumnoDAO alumnoDAO = new AlumnoDAO();

        [HttpGet("alumnoProfesor")]
        public List<AlumnoProfesor> AlumnoProfesor(string usuario)
        {
            return alumnoDAO.AlumnosProfesor(usuario);
        }

        [HttpGet("alumno")]
        public Alumno? GetAlumno(int id)
        {
            return alumnoDAO.SeleccionarAlumno(id);
        }

        [HttpPut("alumno")]
        public bool UpdateAlumno([FromBody] Alumno alumno)
        {
            return alumnoDAO.UpdateAlumno(alumno);
        }
    }
}
