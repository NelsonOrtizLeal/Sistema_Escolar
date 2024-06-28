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

        public bool Add(Calificacion calificacion)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    contexto.Calificacions.Add(calificacion);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    var encontrado = contexto.Calificacions.Find(id);

                    if( encontrado == null)
                    {
                        return false;
                    }

                    contexto.Calificacions.Remove(encontrado);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
