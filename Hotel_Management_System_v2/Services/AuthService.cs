using Hotel_Management_System_v2.ConsolePages;
using Hotel_Management_System_v2.DatabaseService;
using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.Other;
using Hotel_Management_System_v2.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.Services
{
    static class AuthService
    {
        private static User _loggedInUser = null;
        private static bool _isLoggedIn = false;

        public static User GetLoggedInUser => _loggedInUser;
        public static bool IsLoggedIn => _isLoggedIn;
        public static void Logout()
        {
            _loggedInUser = null;
            _isLoggedIn = false;
        }
        public static User Login(Action<User> callback)
        {
            string email = Utils.GetInputFromConsole<string>("\nEmail: ", "\nInvalid Format!\n");
            string password = Utils.GetInputFromConsole<string>("Password: ", "\nInvalid Format!\n");

            foreach (User user in DatabaseService.Database.Users)
            {
                if(user.Email == email && user.Password == password)
                {
                    if (user.Role == UserRole.Admin)
                    {
                        _loggedInUser = user;

                        callback(user);

                        return user;
                    }
                    else
                    {
                        Console.WriteLine("\nUser does not admin!\n");
                        Thread.Sleep(2000);
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("\nEmail or password is incorrect!\n");
                    Thread.Sleep(2000);
                    return null;
                }
            }
            return null;
        }

        public static void Register()
        {
            Utils.ClearConsole();

            bool isCorrectPassword = Utils.GetInputFromConsole<string>("Please enter secret password for access register page: ") == "ayxan123";

            if(isCorrectPassword)
            {
                Utils.ClearConsole();
                Console.WriteLine("\nPassword Correct!\n");

                UserService _userService = new UserService();

                string name = Utils.GetInputFromConsole<string>("\nName: ", "\nInvalid Format!\n");
                string surname = Utils.GetInputFromConsole<string>("Surname: ", "\nInvalid Format!\n");

                string username = Utils.GetInputFromConsole<string>("Username: ", "\nInvalid Format!\n");

                string email = Utils.GetInputFromConsole<string>("Email: ", "\nInvalid Format!\n");
                string password = Utils.GetInputFromConsole<string>("Password: ", "\nInvalid Format!\n");

                string passportNo = Utils.GetInputFromConsole<string>("Passport No: ", "\nInvalid Format!\n");

                foreach (User user in Database.Users)
                {
                    if (user.Email == email)
                    {
                        Console.WriteLine("\nUser email already exists in database!\n");
                        Thread.Sleep(2000);
                        return;
                    }
                    else if (user.PassportNo == passportNo)
                    {
                        Console.WriteLine("\nThis passport id already exists in database!\n");
                        Thread.Sleep(2000);
                        return;
                    }
                }

                bool isSuccess = _userService.Create(new User(name, surname, username, email, password, passportNo, null));

                Utils.ClearConsole();

                if (isSuccess)
                {
                    Console.WriteLine("\nYour Account Created Successfully!\n");
                }
                else
                {
                    Console.WriteLine("\nAn Unknown Error Occurred!\n");
                }
                Thread.Sleep(2000);
            }
            else
            {
                Utils.ClearConsole();
                Console.WriteLine("\nInvalid Password!\n");
                Thread.Sleep(2000);
                Pages.Init();
            }
        }
    }
}
