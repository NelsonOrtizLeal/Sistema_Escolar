using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
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

        public bool Add(Alumno alumno)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    contexto.Add(alumno);
                    contexto.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    // Preguntar si el id delalumno existe
                    Alumno alumno = contexto.Alumnos.Find(id);

                    if (alumno == null)
                        return false;

                    // Ingreso de datos
                    Console.WriteLine("Nombre:");
                    alumno.Nombre = Console.ReadLine();

                    Console.WriteLine("DNI:");
                    alumno.Dni = Console.ReadLine();

                    Console.WriteLine("Direccion:");
                    alumno.Direccion = Console.ReadLine();

                    Console.WriteLine("Edad:");
                    alumno.Edad = int.Parse(Console.ReadLine());

                    Console.WriteLine("Email:");
                    alumno.Email = Console.ReadLine();

                    // Le indicamos al contexto de EF que lo siguiente es una modificacion
                    contexto.Entry(alumno).State = EntityState.Modified;
                    contexto.SaveChanges();
                }

                return true;
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
                    // El alumno existe
                    Alumno alumno = contexto.Alumnos.Find(id);

                    if (alumno == null)
                        return false;

                    // Eliminar el alumno
                    contexto.Alumnos.Remove(alumno);
                    // Guardar los cambios
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
    }
}
