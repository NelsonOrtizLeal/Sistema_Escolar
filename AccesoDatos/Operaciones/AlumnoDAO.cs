using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public Alumno? SeleccionarAlumno(int id)
        {
            using (ProyectoContext contexto = new ProyectoContext())
            {
                var alumno = contexto.Alumnos.Find(id);
                return alumno;
            }
        }

        public Alumno? SeleccionarAlumnoDNI(string dni)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    var alumno = contexto.Alumnos.Where(a => a.Dni.Equals(dni)).FirstOrDefault();
                    return alumno;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
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

        public bool UpdateAlumno(Alumno alumno)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    contexto.Entry(alumno).State = EntityState.Modified;
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<AlumnoAsignatura> AlumnosAsignaturas()
        {
            using (ProyectoContext contexto = new ProyectoContext())
            {
                var query = from a in contexto.Alumnos
                            join m in contexto.Matriculas on a.Id equals m.AlumnoId
                            join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                            select new AlumnoAsignatura
                            {
                                NombreAlumno = a.Nombre,
                                NombreAsignatura = asig.Nombre,
                            };

                // Convertimos la consulta LINQ a la lista
                return query.ToList();
            }
        }

        public List<AlumnoProfesor> AlumnosProfesor(string usuario)
        {
            using (ProyectoContext contexto = new ProyectoContext())
            {
                var query = from a in contexto.Alumnos
                            join m in contexto.Matriculas on a.Id equals m.AlumnoId
                            join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                            where asig.Profesor == usuario
                            select new AlumnoProfesor
                            {
                                Id = a.Id,
                                Dni = a.Dni,
                                Nombre = a.Nombre,
                                Direccion = a.Direccion,
                                Edad = a.Edad,
                                Email = a.Email,
                                AsignaturaNombre = asig.Nombre,
                                MatriculaId = m.Id,
                            };

                return query.ToList();
            }
        }

        public bool InsertarMatricula(Alumno alumno, string dni, int id_asignatura)
        {
            try
            {
                //Revisar que el alumno exista
                var existe = SeleccionarAlumnoDNI(dni);

                if (existe == null)
                {
                    // Creamos el alumno
                    Add(alumno);

                    // Consultamos el Id del alumno
                    var insertado = SeleccionarAlumnoDNI(dni);

                    // Registramos la matricula
                    Matricula m = new Matricula();
                    m.AsignaturaId = id_asignatura;
                    m.AlumnoId = insertado.Id;

                    using (ProyectoContext contexto = new ProyectoContext())
                    {
                        contexto.Matriculas.Add(m);
                        contexto.SaveChanges();
                    }
                }
                else
                {
                    // Solo asignamos a el alumno
                    Matricula m = new Matricula();
                    m.AsignaturaId = id_asignatura;
                    m.AlumnoId = existe.Id;

                    using (ProyectoContext contexto = new ProyectoContext())
                    {
                        contexto.Matriculas.Add(m);
                        contexto.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Matricula> MatriculasAlumno(int id_alumno)
        {
            try
            {
                using (ProyectoContext contexto = new ProyectoContext())
                {
                    List<Matricula> matriculas = contexto.Matriculas.Where(m => m.AlumnoId.Equals(id_alumno)).ToList();
                    return matriculas;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Matricula>();
            }
        }

        public bool EliminarAlumno(int id)
        {
            try
            {
                //Obtener los datos del alumno
                var alumno = SeleccionarAlumno(id);

                // Si el alumno existe
                if (alumno != null)
                {
                    // Obtener todas las asignaturas mastriculadas al alumno
                    var matriculas = MatriculasAlumno(id);

                    // Iterar la lista de matriculas para eliminar las calificaciones
                    foreach (Matricula matricula in matriculas)
                    {
                        // Usar el contexto listar las calificaciones y luego eliminar todas las calificaciones de una matricula
                        using (ProyectoContext contexto = new ProyectoContext())
                        {
                            var calificaciones = contexto.Calificacions.Where(c => c.MatriculaId.Equals(matricula.Id));

                            // Eliminar las calificaciones
                            contexto.Calificacions.RemoveRange(calificaciones);
                            contexto.SaveChanges();
                        }
                    }

                    // De la misma forma eliminar todas las matriculas del alumno
                    using (ProyectoContext contexto = new ProyectoContext())
                    {
                        contexto.Matriculas.RemoveRange(matriculas);

                        // Eliminar el alumno
                        contexto.Alumnos.Remove(alumno);

                        // Confirmar los cambios en la base de datos
                        contexto.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
