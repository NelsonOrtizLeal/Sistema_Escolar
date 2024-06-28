using AccesoDatos.Context;
using AccesoDatos.Models;

namespace AccesoDatos.Operaciones
{
    public class CalificacionDAO
    {
        public List<Calificacion> CalificacionesMatricula(int id_matricula)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    var calificaciones = contexto.Calificacions.Where(c => c.MatriculaId.Equals(id_matricula)).ToList();
                    return calificaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new List<Calificacion>();
            }
        }
    }
}
