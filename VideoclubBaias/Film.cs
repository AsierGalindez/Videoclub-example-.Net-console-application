using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoclubBaias
{
    public class Film
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["VideoclubBaias"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static SqlCommand command;
        static SqlDataReader user;

        public static int ID { get; set; }
        public static string Title { get; set; }
        public static string Synopsis { get; set; }
        public static int Pegi { get; set; }
        public static string Available { get; set; }



        public static void TitlesFilm(int age, List <int> listID)
        {
            connection.Open();
            command = new SqlCommand($"SELECT * FROM FILMS WHERE pegi <= '{age}'AND available = 'l';", connection);
            user = command.ExecuteReader();

            while (user.Read())
            {

                Film peli = new Film();

                ID = Convert.ToInt32(user["ID"]);

                listID.Add(ID);

                Title = Convert.ToString(user["title"]);

                Pegi = Convert.ToInt32(user["pegi"]);

                string available = string.Empty;

                Console.WriteLine($"\nSelecciona el número de la película que quieres alquilar\n\t\t -----{ID}.-{Title}\n\t Recomendación de edad: {Pegi}");

            }
            connection.Close();


        }

        public static void AvaiableFilms(int age)
        {
            int i = 1;
            connection.Open();
            command = new SqlCommand($"SELECT * FROM FILMS WHERE pegi < '{age}';", connection);
            user = command.ExecuteReader();

            while (user.Read())
            {



                Title = Convert.ToString(user["title"]);
                Synopsis = Convert.ToString(user["synapsis"]);
                Pegi = Convert.ToInt32(user["pegi"]);
                Available = Convert.ToString(user["available"]);
                string available = string.Empty;



                Console.WriteLine($"\n\n\t\t -----{i}.-{Title}-----\n\n\tSinopsis:\t{Synopsis}\n\t Recomendación de edad: {Pegi}");
                
                if (Available == "l")
                {
                    available = "La pelicula está disponible";
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(available);
                    Console.ResetColor();  
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    available = "La pelicula no está disponible";
                    Console.WriteLine(available);
                    Console.ResetColor();

                }

                i++;


            }
            connection.Close();
        }
    }
}
