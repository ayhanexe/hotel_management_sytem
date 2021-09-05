using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.Other;
using Hotel_Management_System_v2.DatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel_Management_System_v2.Services;
using System.Threading;
using Hotel_Management_System_v2.Types;

namespace Hotel_Management_System_v2.ConsolePages
{
    static partial class Pages
    {
        private static void SearchAvailableRoomsPage()
        {
            RoomService roomService = new RoomService();
            UserService userService = new UserService();
            List<User> filteredUsers = userService.GetAll().FindAll((User user) => user.Role != Types.UserRole.Admin && user.Room == null);

            while (true)
            {
                Utils.ClearConsole();

                Console.WriteLine("\n---------- SEARCH ROOM ----------\n");

                DateTime from = Utils.GetInputFromConsole<DateTime>("From (Date YYYY:MM:DD): ");

                if (from < DateTime.Now.AddDays(-1))
                {
                    Console.WriteLine("\n'From Date' Must Be Greater Than Yesterday!\n");
                    Thread.Sleep(2000);
                    continue;
                }

                DateTime to = Utils.GetInputFromConsole<DateTime>("To (Date YYYY:MM:DD): ");

                if (to < from)
                {
                    Console.WriteLine("\n'To Date' Must Be Greater Than 'From Date'!\n");
                    Thread.Sleep(2000);
                    continue;
                }

                while (true)
                {
                    Utils.ClearConsole();

                    Console.WriteLine($"\n---------- AVAILABLE ROOMS From: {from} to: {to}  ----------\n");
                    for (int i = 0; i < DatabaseService.Database.Rooms.Count; i++)
                    {
                        Room currentRoom = DatabaseService.Database.Rooms[i];
                        string printContent = $"{i + 1}: Room no: {currentRoom.No} | Room Count: {currentRoom.RoomCount} | Window Count: {currentRoom.WindowCount}| {(currentRoom.Guest != null ? $"Currently Leased (From - {currentRoom.LeasedDate} ) - (To - {currentRoom.LeaseExpirationDate}))" : null)}";

                        if (currentRoom.Guest != null)
                        {
                            if ((to <= currentRoom.LeasedDate) || (from >= currentRoom.LeaseExpirationDate))
                            {
                                Console.WriteLine(printContent);
                            }
                        }
                        else
                        {
                            Console.WriteLine(printContent);
                        }
                    }
                    Console.WriteLine("\n--------------------------------------\n");

                    Console.WriteLine("\nPlease press any key for go back main menu...\n");
                    Console.ReadKey();
                    Pages.ModesPage();
                }
                break;
            }
        }
    }
}
