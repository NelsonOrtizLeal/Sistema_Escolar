using AccesoDatos.Operaciones;
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
                Console.WriteLine("Elija una opción:");
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
                }
            } while (again);
        }

        public static void ShowMenu()
        {
            System.Console.WriteLine("\n---------MENU---------");
            System.Console.WriteLine("1.-   Mostrar alumnos");
            System.Console.WriteLine("2.-   Mostrar detalle alumno");
            System.Console.WriteLine("2.-   Agregar");
            System.Console.WriteLine("3.-   Editar");
            System.Console.WriteLine("4.-   Eliminar");
            System.Console.WriteLine("5.-   Salir");
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
        #endregion
    }
}
