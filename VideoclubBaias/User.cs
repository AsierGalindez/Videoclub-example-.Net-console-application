using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoclubBaias
{
    public class User
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["VideoclubBaias"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static SqlCommand command;
        static SqlDataReader user;

        public static string Email { get; set; }
        public static string Pass { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static DateTime BirthDate { get; set; }

        public User()
        {

        }


        public static bool UserVerification(string email, string pass)
        {
            bool var = false;
            connection.Open();
            command = new SqlCommand("SELECT * FROM USERS", connection);
            user = command.ExecuteReader();

            while (user.Read())
            {
                
                if (Convert.ToString(user["email"]) == email && Convert.ToString(user["userpass"]) == pass)
                {

                    var = true;
                    connection.Close();
                    return var;

                }
               

            }
            connection.Close();
            return var;

        }
        public static void NewUser()
        {

            do
            {
                Console.WriteLine("Introduce tu email");
                Email = Console.ReadLine();
                if (Email.IndexOf("@") == -1 || Email.IndexOf(".") == -1)
                {
                    Console.WriteLine("El email introducido no es correcto vuelve a introducir un email");
                }

            } while (Email.IndexOf("@") == -1 || Email.IndexOf(".") == -1);

            string pas2;
            do
            {

                Console.WriteLine("Introduce una contraseña");
                Pass = Console.ReadLine();


                Console.WriteLine("Repite la contraseña");
                pas2 = Console.ReadLine();
                if (Pass != pas2) { Console.WriteLine("Las contraseñas no coinciden"); }

            } while (Pass != pas2);


            do
            {

                Console.WriteLine("Introduce tu Nombre");
                Name = Console.ReadLine();
                if (Name.Length < 2)
                {
                    Console.WriteLine("El nombre tiene que tener al menos dos caracteres");
                }

            } while (Name.Length < 2);

            do
            {

                Console.WriteLine("Introduce tu Apellido");
                LastName = Console.ReadLine();

                if (LastName.Length < 2)
                {
                    Console.WriteLine("El apellido tiene que tener al menos dos caracteres");
                }

            } while (LastName.Length < 2);


            //Faltaría hacer el bucle aquí
            Console.WriteLine("Introduce tu fecha de nacimiento en formato 'DD/MM/AAAA'");

            try
            {

                BirthDate = Convert.ToDateTime(Console.ReadLine());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }



            connection.Open();
            try
            {
                command = new SqlCommand($"INSERT INTO Users (email, userpass, name, lastname, birthdate)" +
                    $" VALUES ('{Email}', '{Pass}', '{Name}', '{LastName}', '{BirthDate}');", connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            connection.Close();
        }

        public void NewObject(string email)
        {
            
            
                connection.Open();
                command = new SqlCommand($"SELECT * FROM USERS WHERE EMAIL = '{email}';", connection);
                user = command.ExecuteReader();

                while (user.Read())
                {

                    Email = email;
                    Name = Convert.ToString(user["name"]);
                    Pass = Convert.ToString(user["userpass"]);
                    LastName = Convert.ToString(user["lastname"]);
                    BirthDate = Convert.ToDateTime(user["birthdate"]);


                }
                connection.Close();


            
        }

        public static int AgeUser()
        {
            int age = 0;
            age = DateTime.Now.Subtract(BirthDate).Days;
            age = age / 365;
            return age;

        }
    }
}
