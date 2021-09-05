using Hotel_Management_System_v2.DatabaseService;
using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.Other;
using Hotel_Management_System_v2.Services;
using Hotel_Management_System_v2.Types;
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
        static void Booking()
        {
            RoomService roomService = new RoomService();
            UserService userService = new UserService();
            List<User> filteredUsers = userService.GetAll().FindAll((User user) => user.Role != Types.UserRole.Admin && user.Room == null);

            if (filteredUsers.Count > 0)
            {
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

                        int roomIndex = Utils.GetInputFromConsole<int>("Select Room: ") - 1;

                        try
                        {
                            Room toUpdatedRoom = Database.Rooms[roomIndex];

                            while (true)
                            {
                                Utils.ClearConsole();

                                Console.WriteLine("\n---------- SELECT USERS  ----------\n");
                                for (int i = 0; i < filteredUsers.Count; i++)
                                {
                                    User user = filteredUsers[i];

                                    string printContent = $"{i + 1}: {user.Name} {user.Surname} {user.Email} {user.PassportNo} {user.Username}";
                                    Console.WriteLine(printContent);
                                }
                                Console.WriteLine("\n-----------------------------------\n");

                                int selectedUserId = Utils.GetInputFromConsole<int>("Select user: ");

                                try
                                {
                                    User selectedUser = userService.GetAll()[selectedUserId];

                                    if (selectedUser.Room == null)
                                    {
                                        Room userRoom = new Room(toUpdatedRoom.No, selectedUser, toUpdatedRoom.RoomCount, toUpdatedRoom.WindowCount, toUpdatedRoom.hasLandscape, to);

                                        roomService.Update(toUpdatedRoom.Id, userRoom);

                                        Utils.ClearConsole();

                                        selectedUser.Room = userRoom;
                                        Console.WriteLine($"\n{selectedUser.Name} {selectedUser.Surname} Added To Room No {userRoom.No}\n");
                                        Thread.Sleep(2000);
                                        Pages.ModesPage();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{selectedUser.Name} {selectedUser.Surname} already has a room! -> Room no: {selectedUser.Room.No}");
                                        Thread.Sleep(2000);
                                        Pages.ModesPage();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("\nInvalid User Data\n");
                                    Thread.Sleep(2000);
                                    continue;
                                }
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nInvalid Room Data\n");
                            Thread.Sleep(2000);
                            continue;
                        }
                        break;
                    }
                    break;
                }
            }
            else
            {
                if (userService.GetAll().FindAll(u => u.Role == UserRole.User).Count == 0)
                {
                    Console.WriteLine("No User Finded!");
                }
                else
                {
                    Console.WriteLine("All Guests Have a Room!");
                }
                Thread.Sleep(2000);
                Pages.ModesPage();
            }
        }
    }
}
