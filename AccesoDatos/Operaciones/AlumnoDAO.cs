using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operaciones
{
    public class AlumnoDAO
    {
        public List<Alumno> SeleccionarTodos()
        {
            using (ProyectoContext contexto = new ProyectoContext())
            {
                var alumnos = contexto.Alumnos.ToList<Alumno>();
                return alumnos;
            }
        }

        public Alumno SeleccionarAlumno(int id)
        {
            using (ProyectoContext contexto = new ProyectoContext())
            {
                var alumno = contexto.Alumnos.Find(id);
                return alumno;
            }
        }
    }
}
