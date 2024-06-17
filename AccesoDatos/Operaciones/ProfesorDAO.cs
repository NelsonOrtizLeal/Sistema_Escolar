using AccesoDatos.Context;
using AccesoDatos.Models;

namespace AccesoDatos.Operaciones
{
    public class ProfesorDAO
    {
        public Profesor? login(string username, string password)
        {
            using (ProyectoContext contexto = new ProyectoContext())
            {
                var profesor = contexto.Profesors.Where(p => p.Usuario.Equals(username) && p.Pass.Equals(password)).FirstOrDefault();

                return profesor;
            }
        }
    }
}
