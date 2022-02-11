using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoclubBaias
{
    public class Booking

    {
        static string connectionString = ConfigurationManager.ConnectionStrings["VideoclubBaias"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);
        static SqlCommand command;
        static SqlDataReader user;


        public int ID { get; set; }
        public string Email { get; set; }
        public int FilmID { get; set; }
        public DateTime RentalDay { get; set; }
        public DateTime ReturnDay { get; set; }


        public static void BookingFilm(User user1)
        {
            List<int> listID = new List<int>();
            //Mostramos titulos y cogemos la variable i de films para recoger la respuesta del usuario 
            Film.TitlesFilm(User.AgeUser(), listID);

            Console.WriteLine("\tEscribe el número de la pelicula que quieres alquilar");
            int selectedFilm = 0;
            try
            {
                selectedFilm = Int32.Parse(Console.ReadLine());

            }
            catch (Exception ex)
            {

                Console.WriteLine("No has introducido un valor correcto");
                Console.WriteLine(ex.Message);
            }


            connection.Open();
            if (listID.IndexOf(selectedFilm) != -1)
            {
                try
                {
                    command = new SqlCommand($"INSERT INTO bookings (filmsID, email, RentalDate)" +
                        $" VALUES ('{selectedFilm}', '{User.Email}', GETDATE()); UPDATE Films SET available = 'o' where id ={selectedFilm};", connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Pelicula aquilada con éxito");
                Console.ResetColor();
                Console.WriteLine("pulsa cualquier tecla para continuar.....");
                Console.ReadKey();

            }
            connection.Close();
            // Menu.MenuFilm(user1);
        }


        public static void MyBookings(User user1)
        {
            List<int> bookingID = new List<int>();
            List<int> listID = new List<int>();
            bool exit = false;
            connection.Open();
            command = new SqlCommand($"SELECT FilmsID, Title, RentalDate, B.ID FROM Films F INNER JOIN Bookings B ON F.Id = B.FilmsID Where Email = '{User.Email}' AND available = 'o' AND returnDate is null;", connection);
            user = command.ExecuteReader();
            Console.WriteLine($"\nEstas son las películas que tienes alquiladas\n\n\n");




            while (user.Read())
            {

                bookingID.Add(Convert.ToInt32(user["ID"]));
                listID.Add(Convert.ToInt32(user["filmsID"]));
                int filmID = Convert.ToInt32(user["filmsID"]);
                string title = Convert.ToString(user["title"]);

                DateTime ReturnDay = Convert.ToDateTime(user["RentalDate"]).AddDays(3);





                string available = string.Empty;

                Console.WriteLine($"\n\t\t -----{filmID}.-{title}");

                if ((ReturnDay - DateTime.Today).TotalDays < 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\t-------La fecha de devolución ha pasado. {ReturnDay.ToString("dd/M/yyyy")}");
                    Console.ResetColor();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"La fecha de devolución es {ReturnDay.ToString("dd/M/yyyy")}");
                    Console.ResetColor();
                }

            }
            connection.Close();

            if (bookingID.Count > 0)
            {
                int respuesta = 0;

                Console.WriteLine("Inserta el ID de la película que deseas devolver o cualquier otra tecla para volver al menú");
                try
                {
                    respuesta = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    exit = true;
                }



                if (listID.IndexOf(respuesta) != -1)
                {
                    //LLamada devolver pelicula
                    connection.Open();
                    try
                    {

                        command = new SqlCommand($"UPDATE Films SET available = 'l' where ID = '{respuesta}' ;" +
                            $"UPDATE Bookings SET ReturnDate = GETDATE() where filmsid = '{respuesta}';", connection);

                        command.ExecuteNonQuery();

                        connection.Close();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("La pelicula se ha devuelto");
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        Console.WriteLine(ex.Message);
                    }

                    connection.Close();

                }
                else
                {
                    exit = true;
                }
                //Menu.MenuFilm(user1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No tienes ninguna película alquilada");
                Console.ResetColor();
            }


        }

    }
}

