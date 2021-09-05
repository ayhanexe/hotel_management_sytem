using Hotel_Management_System_v2.ConsolePages;
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
        public static void CheckInPage()
        {
            UserService userService = new UserService();
            RoomService roomService = new RoomService();


            List<User> filteredUsers = userService.GetAll().FindAll((User user) => user.Room != null && user.Role == UserRole.User);

            if (filteredUsers.Count > 0)
            {
                while (true)
                {
                    Utils.ClearConsole();

                    Console.WriteLine("\n---------- SELECT USER FOR CHECK IN REPORT  ----------\n");
                    for (int i = 0; i < filteredUsers.Count; i++)
                    {
                        User user = filteredUsers[i];

                        string printContent = $"{i + 1}: {user.Name} {user.Surname} {user.Email} {user.PassportNo} {user.Username}";
                        Console.WriteLine(printContent);
                    }
                    Console.WriteLine("\n-----------------------------------\n");

                    int selectedUserId = Utils.GetInputFromConsole<int>("Select user: ") - 1 ;

                    try
                    {
                        User selectedUser = filteredUsers[selectedUserId];
                        List<Room> rooms = roomService.GetAll();
                        Room toUpdatedRoom = selectedUser.Room;

                        Room userRoom = new Room(toUpdatedRoom.No, toUpdatedRoom.RoomCount, toUpdatedRoom.WindowCount, toUpdatedRoom.hasLandscape) { LastCheckIn = DateTime.Now, LastCheckOut = toUpdatedRoom.LastCheckOut };

                        roomService.Update(toUpdatedRoom.Id, userRoom);

                        Utils.ClearConsole();

                        selectedUser.Room = userRoom;
                        Console.WriteLine($"\n{selectedUser.Name} {selectedUser.Surname}'s Check In Saved to database\n");
                        Thread.Sleep(2000);
                        Pages.ModesPage();
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
            else
            {
                Console.WriteLine("\nUser Booking Data Not Found!\n");
                Thread.Sleep(2000);
                Pages.ModesPage();
            }
        }
    }
}
