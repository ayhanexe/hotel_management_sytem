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
        public static UserRole USerRole { get; private set; }

        public static void BookingReports()
        {
            RoomService roomService = new RoomService();

            Utils.ClearConsole();
            Console.WriteLine(
                "1: Report by Customer\n" +
                "2: Report by Date\n" +
                "3: Report by Room \n" +
                "4: Back\n\n"
            );

            int mode = Utils.GetInputFromConsole<int>("Select report type: ");

            if (mode == 1)
            {
                while (true)
                {
                    Utils.ClearConsole();
                    List<User> userList = DatabaseService.Database.Users.FindAll((User u) => u.Room != null && u.Role == UserRole.User);

                    if (userList.Count > 0)
                    {
                        Console.WriteLine("\n---------- Booking Reports By Customer ----------");
                        
                        for (int i = 0; i < userList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {userList[i].Name} {userList[i].Surname} {userList[i].Email} {userList[i].PassportNo}\n");
                        }

                        int userIndex = Utils.GetInputFromConsole<int>("Select User: ") - 1;

                        try
                        {
                            Utils.ClearConsole();
                            User user = userList[userIndex];


                            Console.WriteLine($"\n{user.Name} {user.Surname} {user.PassportNo} Bookings\n");
                            Console.WriteLine($"- Last Check In {user.Room.LastCheckIn}\n");
                            Console.WriteLine($"- Last Check Out {user.Room.LastCheckOut}\n");

                            Console.WriteLine($"- Lease Date {user.Room.LeasedDate}\n");
                            Console.WriteLine($"- Lease Date Expiration {user.Room.LeaseExpirationDate}\n");

                            Console.WriteLine("\nPress any key to go back booking details page...\n");
                            Console.ReadKey();
                            Pages.BookingReports();
                        }
                        catch (Exception ex)
                        {
                            Utils.ClearConsole();
                            Console.WriteLine("\nInvalid User Selected!\n");
                            Thread.Sleep(2000);
                            Pages.BookingReports();
                        }

                        break;
                    }
                    else
                    {
                        Utils.ClearConsole();
                        Console.WriteLine("\nNo Customer has a room!\n");
                        Thread.Sleep(2000);
                        Pages.ModesPage();
                    }
                }
            }
            else if (mode == 2)
            {
                DateTime from = Utils.GetInputFromConsole<DateTime>("\nFrom Date: ");
                DateTime to = Utils.GetInputFromConsole<DateTime>("\nTo Date: ");

                List<Room> roomList = roomService.GetAll().FindAll((Room room) => room.Guest != null && room.LeasedDate >= from && room.LeaseExpirationDate <= to);

                if (roomList.Count > 0)
                {
                    Console.WriteLine("\n---------- Booking Reports By Date ----------\n");
                    for (int i = 0; i < roomList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: Room No: {roomList[i].No} | Lease Date: {roomList[i].LeasedDate} | Lease Expiration Date: {roomList[i].LeaseExpirationDate}");
                        if (roomList[i].LastCheckIn != null)
                        {
                            Console.WriteLine($" - Last Check In: { roomList[i].LastCheckIn }");
                        }
                        if (roomList[i].LastCheckOut != null)
                        {
                            Console.WriteLine($" - Last Check Out: { roomList[i].LastCheckOut }");
                        }
                    }


                    Console.WriteLine("\nPress any key to go back booking details page...\n");
                    Console.ReadKey();
                    Pages.BookingReports();
                }
                else
                {
                    Utils.ClearConsole();
                    Console.WriteLine("\n---------- Not Found! ---------\n");
                    Thread.Sleep(2000);
                    Pages.ModesPage();
                }
            }
            else if (mode == 3)
            {
                List<Room> roomList = DatabaseService.Database.Rooms.FindAll((Room room) => room.Guest != null);

                if(roomList.Count > 0)
                {
                    while (true)
                    {
                        Utils.ClearConsole();

                        Console.WriteLine("\n---------- Rooms ----------");
                        for (int i = 0; i < roomList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: Room no: {roomList[i].No}");
                        }
                        Console.WriteLine("---------------------------\n");

                        int selectedRoomIndex = Utils.GetInputFromConsole<int>("Select Room: ") - 1;

                        try
                        {
                            Room room = roomList[selectedRoomIndex];

                            Utils.ClearConsole();
                            Console.WriteLine($"\n---------- Room No: {room.No} ----------\n");
                            Console.WriteLine($"Room Count: {room.RoomCount}\n");
                            Console.WriteLine($"Window Count: {room.WindowCount}\n");
                            Console.WriteLine($"Lease Date: {room.LeasedDate}");
                            Console.WriteLine($"Lease Date Expiration: {room.LeaseExpirationDate}");


                            if (room.LastCheckIn != null)
                            {
                                Console.WriteLine($"Last Check In: {room.LastCheckIn}\n");
                            }
                            if (room.LastCheckOut != null)
                            {
                                Console.WriteLine($"Last Check Out: {room.LastCheckOut}\n");
                            }

                            Console.WriteLine("\nPress any key to go back booking details page...\n");
                            Console.ReadKey();
                            Pages.BookingReports();

                        }
                        catch (Exception ex)
                        {
                            Utils.ClearConsole();
                            Console.WriteLine("\nInvalid Room Selected!\n");
                            Thread.Sleep(2000);
                            Pages.ModesPage();
                            continue;
                        }
                    }
                }
                else
                {
                    Utils.ClearConsole();
                    Console.WriteLine("\nNo Booking Data!\n");
                    Thread.Sleep(2000);
                    Pages.BookingReports();
                    return;
                }
            }
            else if (mode == 4)
            {
                Pages.ModesPage();
            }
        }
    }
}
