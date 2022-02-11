using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoclubBaias
{
    internal class Menu
    {
        public static void MenuFilm(User user1)
        {

            bool exit = false;
            int res = 0;
            Film film = new Film();
            do
            {

                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\tMENU {User.Name}");
                    Console.ResetColor();
                    Console.WriteLine($" \n1.- Ver peliculas disponibles \n2.- Alquilar película \n3.- Mis alquileres\n4.-Logout");
                    try
                    {
                        res = Int32.Parse(Console.ReadLine());
                        if (res != 1 && res != 2 && res != 3 && res != 4)
                        {
                            Console.WriteLine("No has introducido un valor correcto");
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("No has introducido un valor correcto");
                        Console.WriteLine(ex.Message);
                    }
                } while (res != 1 && res != 2 && res != 3 && res != 4);



                switch (res)
                {

                    case 1://VER LAS PELICULAS QUE ENCAJAN CON NUESTRA EDAD
                        {
                            int age = User.AgeUser();
                            Film.AvaiableFilms(age);
                            Console.WriteLine("Pulsa cualquier tecla para volver al menú ");
                            Console.ReadKey();
                           
                        }
                        break;
                    case 2://VER LOS TITULOS QUE PODEMOS ALQUILAR Y SELECCIONAR
                        {
                            Booking.BookingFilm(user1);
                        }
                        break;
                    case 3:
                        {
                            Booking.MyBookings(user1);
                            break;
                        }
                    case 4:
                        {
                            exit = true;
                            //Login.EnterLogin();
                            break;
                        }

                }


            } while (!exit);
        }
    }
}
