using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using System.Threading.Channels;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programa para probar la biblioteca de EF");

            // Instanceamiento de conexion a EF
            AlumnoDAO alumnoDAO = new AlumnoDAO();

            // Variables para controlar las opciones del menu
            int op = 0;
            bool again = true;

            //Menu
            do
            {
                ShowMenu();
                Console.WriteLine("\nElija una opción:");
                op = int.Parse(Console.ReadLine());

                // Opciones
                switch (op)
                {
                    case 1:
                        Show(alumnoDAO);
                        break;
                    case 2:
                        ShowOne(alumnoDAO);
                        break;
                    case 3:
                        Add(alumnoDAO);
                        break;
                    case 4:
                        Edit(alumnoDAO);
                        break;
                    case 5:
                        Delete(alumnoDAO);
                        break;
                    case 6:
                        again = false;
                        break;

                }
            } while (again);
        }

        public static void ShowMenu()
        {
            System.Console.WriteLine("\n---------MENU---------");
            System.Console.WriteLine("1.-   Mostrar alumnos.");
            System.Console.WriteLine("2.-   Mostrar ficha tecnica.");
            System.Console.WriteLine("3.-   Agregar alumno.");
            System.Console.WriteLine("4.-   Editar alumno.");
            System.Console.WriteLine("5.-   Eliminar alumno.");
            System.Console.WriteLine("6.-   Salir");
        }

        #region OPCIONES MENU
        public static void Show(AlumnoDAO alumnoDAO)
        {
            // Limpiar la consola
            Console.Clear();

            // Mensaje
            Console.WriteLine("Lista de alumnos:");

            // Consultar base de datos
            var alumnos = alumnoDAO.SeleccionarTodos();

            // Consultar numero de elementos
            if (alumnos.Count == 0)
            {
                Console.WriteLine("Sin alumnos para mostrar.");
                return;
            }

            Console.WriteLine(string.Join(Environment.NewLine, alumnos.Select(alumno => $"ID: {alumno.Id} Nombre: {alumno.Nombre}")));
        }

        public static void ShowOne(AlumnoDAO alumnoDAO)
        {
            // Limpiar la consola
            Console.Clear();

            // Mostrando lista de alumnos para seleccionar
            Show(alumnoDAO);

            // Mensaje
            Console.WriteLine("");
            Console.WriteLine("Ingrese el ID del alumno a mostrar:");
            int id = int.Parse(Console.ReadLine());

            // Limpiamos la consola
            Console.Clear();

            // Consultar base de datos
            var alumno = alumnoDAO.SeleccionarAlumno(id);

            // Consultar numero de elementos
            if (alumno == null)
            {
                Console.WriteLine("Alumno no encontrado.");
                return;
            }

            Console.WriteLine("Ficha de Alumno: ");
            Console.WriteLine($"ID: {alumno.Id} \nNombre: {alumno.Nombre} \nDNI: {alumno.Dni} \nDireccion: {alumno.Direccion} \nEdad: {alumno.Edad} \nEmail: {alumno.Email}");
        }

        public static void Add(AlumnoDAO alumnoDAO)
        {
            // Limpiar la consola
            Console.Clear();

            // Mensaje
            Console.WriteLine("\nIngrese los datos del alumno:");

            // Ingreso de datos
            Console.WriteLine("Nombre:");
            string nombre = Console.ReadLine();

            Console.WriteLine("DNI:");
            string dni = Console.ReadLine();

            Console.WriteLine("Direccion:");
            string direccion = Console.ReadLine();

            Console.WriteLine("Edad:");
            int edad = int.Parse(Console.ReadLine());

            Console.WriteLine("Email:");
            string email = Console.ReadLine();

            // Crear objeto
            Alumno alumno = new Alumno()
            {
                Nombre = nombre,
                Dni = dni,
                Direccion = direccion,
                Edad = edad,
                Email = email
            };

            // Guardar en base de datos
            bool result = alumnoDAO.Add(alumno);

            if (result)
            {
                // Mensaje
                Console.WriteLine("Alumno agregado correctamente.");
            }
        }

        public static void Edit(AlumnoDAO alumnoDAO)
        {
            // Limpiar la consola
            Console.Clear();

            // Mostrar la lista de alumnos
            Show(alumnoDAO);

            // Recibimos el id del alumno
            Console.WriteLine("\nElija un alumno a editar:");

            int id = int.Parse(Console.ReadLine());

            bool response = alumnoDAO.Edit(id);

            if (response)
            {
                Console.WriteLine("Alumno actualizado");
            }
            else
            {
                Console.WriteLine("El alumno no existe");
            }
        }

        public static void Delete(AlumnoDAO alumnoDAO)
        {
            Console.Clear();

            Show(alumnoDAO);

            Console.WriteLine("\nElija un alumno a eliminar");

            int id = int.Parse(Console.ReadLine());

            bool response = alumnoDAO.Delete(id);

            if (response)
            {
                Console.WriteLine("Alumno eliminado");
            }
            else
            {
                Console.WriteLine("El alumno no existe");
            }
        }
        #endregion
    }
}
