using Hotel_Management_System_v2.Services;
using Hotel_Management_System_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Management_System_v2.Types;
using Hotel_Management_System_v2.Other;
using System.Threading;

namespace Hotel_Management_System_v2.ConsolePages
{
    static partial class Pages
    {
        private static void BookingDetails()
        {
            UserService userService = new UserService();

            List<User> filteredUsers = userService.GetAll().FindAll((User user) => user.Room != null && user.Role == UserRole.User);

            if (filteredUsers.Count > 0)
            {
                while (true)
                {
                    Utils.ClearConsole();

                    Console.WriteLine("\n---------- USERS ----------");
                    for (int i = 0; i < filteredUsers.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {filteredUsers[i].Name} {filteredUsers[i].Surname} {filteredUsers[i].PassportNo}");
                    }
                    Console.WriteLine("\n---------------------------");

                    int userIndex = Utils.GetInputFromConsole<int>("\nSelect User: ") - 1;

                    try
                    {
                        User selectedUser = filteredUsers[userIndex];

                        Utils.ClearConsole();
                        Console.WriteLine($"\n{selectedUser.Name} {selectedUser.Surname}\n");
                        Console.WriteLine($"Passport/ID - {selectedUser.PassportNo} | Email: {selectedUser.Email}\n");
                        Console.WriteLine($"Room No - {selectedUser.Room.No} | From - {selectedUser.Room.LeasedDate} | To - {selectedUser.Room.LeaseExpirationDate}");
                        Console.WriteLine($"Last Check In - {(selectedUser.Room.LastCheckIn == null ? "N/A" : selectedUser.Room.LastCheckIn)} | Last Check Out - {(selectedUser.Room.LastCheckOut == null ? "N/A" : selectedUser.Room.LastCheckOut)}");


                        Console.WriteLine("\nPress any key to go back main menu...\n");
                        Console.ReadKey();
                        Pages.ModesPage();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nInvalid user selected!\n");
                        Thread.Sleep(2000);
                        continue;
                    }

                    break;
                }
            }
            else
            {
                Utils.ClearConsole();
                if (userService.GetAll().FindAll((User user) => user.Role == UserRole.User).Count == 0)
                {
                    Console.WriteLine("\nNo user founded!\n");
                }
                else
                {
                    Console.WriteLine("\nNo user have a room!\n");
                }
                Thread.Sleep(2000);
                Pages.ModesPage();
            }
        }
    }
}
