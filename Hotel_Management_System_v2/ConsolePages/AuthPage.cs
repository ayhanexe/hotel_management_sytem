using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.Other;
using Hotel_Management_System_v2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.ConsolePages
{
    static partial class Pages
    {
        private static void AuthPage()
        {
            while (true)
            {
                Utils.ClearConsole();

                Console.WriteLine(
                    "1: Login\n" +
                    "2: Register\n"
                );

                int id = Utils.GetInputFromConsole<int>("Select Mode: ", "Invalid Input!");

                if (id == 1)
                {
                    User user = AuthService.Login((user) => Pages.ModesPage());

                    if(user != null)
                    {
                        Pages.ModesPage();
                    }
                    else
                    {
                        Pages.Init();
                    }

                    break;
                }
                else if (id == 2)
                {
                    AuthService.Register();
                }
                else
                {
                    Console.WriteLine("\nInvalid mode number!\n");
                }
            }
        }
    }
}
