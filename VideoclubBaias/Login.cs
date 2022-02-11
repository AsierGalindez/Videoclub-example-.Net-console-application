using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoclubBaias
{
    public class Login
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["VideoclubBaias"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);



        public static void EnterLogin()
        {
            bool exit = false;
            do
            {

                User user1 = new User();
                int res = 0;
                string email = string.Empty;

                
                do
                {

                    Console.WriteLine(@"                                                                                
                                                                                
                                         @@@@@@@@%                              
                                     &@@@@@@@@@@@@@@@.                          
                     &@@@@@@       *@@@@@@@    %@@@@@@@                         
                  @@@@@@@@@@@@@@  #@@@@@    @@    @@@@@@                        
                @@@@@&      @@@@@@@@@@@/  @    .   @@@@@@                       
               &@@@@  @   @  @@@@@@@@@@@   @@ @@   @@@@@@                       
               &@@@@   @  @  @@@@@@@@@@@@@       @@@@@@@                        
                @@@@@@(    @@@@@@   @@@@@@@@@@@@@@@@@@(                         
                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        @@@              
                     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@@@@@              
                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@@@@@              
                   @@ (@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@@@@@              
                      (@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@@@@@              
                      (@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        @@@              
                      (@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                         
                      (@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                         
                                                                                
                                                                                ");

                    Console.Write("\n\n \t\t Bienvenido a ----");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("VIDEOCLUB BAIAS");
                    Console.ResetColor();
                    Console.WriteLine("----\n\n\t\t1.- Login \n\t\t2.- Registrarte como usuario \n\t\t3.- Salir");

                    try
                    {
                        res = Int32.Parse(Console.ReadLine());
                        if (res != 1 && res != 2 && res != 3)
                        {
                            Console.WriteLine("No has introducido un valor correcto");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("No has introducido un valor correcto");
                        Console.WriteLine(ex.Message);
                    }
                } while (res != 1 && res != 2 && res != 3);

                switch (res)
                {
                    case 1://Introducir email y contraseña
                        {


                            do
                            {

                                Console.WriteLine("Introduce el email de usuario o escribe 'exit' para salir");
                                email = Console.ReadLine().ToLower();
                                if (email.ToLower() == "exit") { exit = true; }

                                //Controlamos que tenga una @ y . el email:

                                else if (email.IndexOf("@") == -1 || email.IndexOf(".") == -1)
                                {
                                    Console.WriteLine("El email introducido no es correcto");
                                }

                                else
                                {

                                    Console.WriteLine("\nIntroduce tu contraseña o escribe 'exit' para salir");

                                    string pass = Console.ReadLine();


                                    if (pass.ToLower() == "exit")
                                    {
                                        exit = true;
                                    }


                                    //Si La verificación es correcta nos manda al menu
                                    else if (User.UserVerification(email, pass))
                                    {

                                        user1.NewObject(email);

                                        Menu.MenuFilm(user1);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Usuario y contraseña no válidos");

                                    }


                                }


                            } while (!exit);

                        }
                        break;
                    case 2://Crear nuevo Usuario

                        User.NewUser();
                        Login.EnterLogin();

                        break;

                    case 3://Salir está bien así¿?

                        Console.Write("Gracias por visitar ----");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("VIEDOCLUB BAIAS");
                        Console.ResetColor();
                        Console.Write("----Nos vemos pronto.");
                        Console.WriteLine(@"  
                      _________________________________
                     |.--------_--_------------_--__--.|
                     ||    /\ |_)|_)|   /\ | |(_ |_   ||
                     ;;`,_/``\|__|__|__/``\|_| _)|__ ,:|
                    ((_(-,-----------.-.----------.-.)`)
                     \__ )        ,'     `.        \ _/
                     :  :        |_________|       :  :
                     |-'|       ,'-.-.--.-.`.      |`-|
                     |_.|      (( (*  )(*  )))     |._|
                     |  |       `.-`-'--`-'.'      |  |
                     |-'|        | ,-.-.-. |       |._|
                     |  |        |(|-|-|-|)|       |  |
                     :,':        |_`-'-'-'_|       ;`.;
                      \  \     ,'           `.    /._/
                       \/ `._ /_______________\_,'  /
                        \  / :   ___________   : \,'
                         `.| |  |           |  |,'
                           `.|  |           |  |
                             |  | SSt       |  |");
                        exit = true;
                        break;

                }


            } while (!exit);

        }
    }
}
