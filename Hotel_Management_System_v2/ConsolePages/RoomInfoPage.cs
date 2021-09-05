using Hotel_Management_System_v2.DatabaseService;
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
        public static void RoomInformationPage()
        {
            UserService userService = new UserService();
            RoomService roomService = new RoomService();

            while (true)
            {
                Utils.ClearConsole();
                Console.WriteLine(
                    "1: Create Room\n" +
                    "2: List Rooms\n" +
                    "3: Update Room\n" +
                    "4: Delete Customer\n" +
                    "5: Back\n"
                );

                int mode = Utils.GetInputFromConsole<int>("Select Mode: ");

                if (mode == 1)
                {
                    string no = Utils.GetInputFromConsole<string>("\nNo: ", "\nInvalid Format!\n");

                    int wantAddGuest = Utils.GetInputFromConsole<int>("\nDo you want add Guest to room? (1: Yes, 0: No): ");

                    try
                    {
                        User Guest = null;
                        if (wantAddGuest == 1)
                        {
                            while (true)
                            {
                                Utils.ClearConsole();
                                List<User> userList = ListUsers();

                                int userIndex = Utils.GetInputFromConsole<int>("\nSelect user: ") - 1;

                                User user = userList[userIndex];




                                if (user.Room != null)
                                {
                                    Utils.ClearConsole();
                                    Console.WriteLine("\nGuest already has a room!\n");
                                    Console.WriteLine("\nFor exit user selection type exit and enter.\n");
                                    int cont = Utils.GetInputFromConsole<int>("\nAre you want continue? (1: Yes, 0:No): \n");
                                    if (cont == 1)
                                    {
                                        return;
                                    }

                                }

                                else
                                {
                                    Guest = user;
                                }
                                break;
                            }
                        }

                        int roomCount = Utils.GetInputFromConsole<int>("Room Count: ");
                        int windowCount = Utils.GetInputFromConsole<int>("Window Count: ");
                        int hasLandscape = Utils.GetInputFromConsole<int>("Has Landscape? (1: Yes, 0:No): ");
                        DateTime? leaseExpirationDate = null;

                        if (Guest != null)
                        {
                            while (true)
                            {
                                Console.Clear();
                                DateTime expiration = Utils.GetInputFromConsole<DateTime>("Enter Lease Expiration Date: ");

                                if (expiration > (DateTime.Now.AddDays(-1)))
                                {
                                    leaseExpirationDate = expiration;
                                }
                                else
                                {
                                    Console.WriteLine("\nRoom Lease Expiration Date Can't be lower Than Current Time!\n");
                                    Thread.Sleep(2000);
                                    continue;
                                }
                                break;
                            }
                        }

                        Room room = new Room(no, Guest, roomCount, windowCount, hasLandscape == 1, leaseExpirationDate != null ? leaseExpirationDate : null);

                        bool isSuccess = roomService.Create(room);


                        if (isSuccess)
                        {
                            Utils.ClearConsole();
                            Console.WriteLine("\nRoom Successfully Created!\n");
                            Thread.Sleep(2000);
                            Pages.RoomInformationPage();
                        }
                        else
                        {
                            Utils.ClearConsole();
                            Console.WriteLine("\nUnknown Error Occurred While Creating Room!\n");
                            Thread.Sleep(2000);
                            Pages.RoomInformationPage();
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n{ex.Message}\n");
                        Thread.Sleep(000);
                        continue;
                    }
                    break;
                }
                else if (mode == 2)
                {
                    Utils.ClearConsole();
                    ListRooms();
                    Console.WriteLine("\nPress any key to go back Room Information Page...\n");
                    Console.ReadKey();
                    Pages.RoomInformationPage();

                }
                else if (mode == 3)
                {
                    while (true)
                    {
                        Utils.ClearConsole();
                        List<Room> rooms = ListRooms();

                        int selectIndex = Utils.GetInputFromConsole<int>("Select Room: ") - 1;

                        try
                        {
                            Room room = rooms[selectIndex];

                            Console.WriteLine($"Updatinng: Room No: {room.No} | Room Count: {room.RoomCount} | Window Count: {room.WindowCount}");

                            string no = Utils.GetInputFromConsole<string>("Room no: ");
                            int roomCount = Utils.GetInputFromConsole<int>("Room Count: ");
                            int windowCount = Utils.GetInputFromConsole<int>("WindowCount: ");
                            bool hasLandscape = Utils.GetInputFromConsole<int>("Has Landscape? (1: Yes, 0:No): ") == 1;
                            bool hasGuest = Utils.GetInputFromConsole<int>("Change Guest? (1: Yes, 0: No): ") == 1;
                            User guest = null;
                            DateTime? expirationDate = null;

                            if (hasGuest)
                            {
                                while (true)
                                {
                                    Utils.ClearConsole();
                                    List<User> listUsers = ListUsers();
                                    int selectedUserIndex = Utils.GetInputFromConsole<int>("Select User:") - 1;

                                    try
                                    {
                                        User user = listUsers[selectedUserIndex];
                                        guest = user;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Utils.ClearConsole();
                                        Console.WriteLine("\nInvalid Guest Selected!\n");
                                        Thread.Sleep(2000);
                                        continue;
                                    }
                                }
                            }

                            if (guest != null)
                            {
                                while (true)
                                {
                                    Utils.ClearConsole();
                                    DateTime expire = Utils.GetInputFromConsole<DateTime>("Lease Expiration Date: ");

                                    if (expire > DateTime.Now)
                                    {
                                        expirationDate = expire;
                                    }
                                    else
                                    {
                                        Utils.ClearConsole();
                                        Console.WriteLine("\nExpire date must be greater than current time!\n");
                                        Thread.Sleep(2000);
                                        continue;
                                    }

                                    break;
                                }
                            }

                            Room updatedRoom = new Room(no, roomCount, windowCount, hasLandscape);

                            if (guest != null)
                            {
                                updatedRoom.Guest = guest;
                                updatedRoom.LeasedDate = DateTime.Now;
                                updatedRoom.LeaseExpirationDate = expirationDate;
                                updatedRoom.Id = room.Id;
                                userService.Update(guest.Id, new User(guest.Name, guest.Surname, guest.Username,guest.Email, guest.Password, guest.PassportNo, updatedRoom));
                            }


                            bool isSuccessed = roomService.Update(room.Id, updatedRoom);

                            if (isSuccessed)
                            {
                                Utils.ClearConsole();
                                Console.WriteLine("\nRoom Updated Successfully!\n");
                                Thread.Sleep(2000);
                                Pages.RoomInformationPage();
                            }
                            else
                            {
                                Utils.ClearConsole();
                                Console.WriteLine("\nUnknown error occurred while updating room!\n");
                                Thread.Sleep(2000);
                                Pages.RoomInformationPage();
                            }


                        }
                        catch (Exception ex)
                        {
                            Utils.ClearConsole();
                            Console.WriteLine("\nInvalid Room Selected!\n");
                            continue;
                        }
                    }
                }
                else if (mode == 4)
                {
                    while (true)
                    {
                        Utils.ClearConsole();

                        List<Room> roomList = ListRooms();

                        int selectIndex = Utils.GetInputFromConsole<int>("Select Room: ") - 1;

                        try
                        {
                            Room selectedRoom = roomList[selectIndex];

                            bool isSuccessfull = roomService.Delete(selectedRoom.Id);


                            if (isSuccessfull)
                            {
                                Utils.ClearConsole();
                                Console.WriteLine("\nRoom Successfully Deleted!\n");
                                Thread.Sleep(2000);
                                Pages.RoomInformationPage();
                                return;
                            }
                            else
                            {
                                Utils.ClearConsole();
                                Console.WriteLine("\nUnknown Error Occurred!\n");
                                Thread.Sleep(2000);
                                Pages.RoomInformationPage();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            Utils.ClearConsole();
                            Console.WriteLine("\nInvalid Room Selected!\n");
                            Thread.Sleep(2000);
                            continue;
                        }
                        break;
                    }
                }
                else
                {
                    Pages.ModesPage();
                    return;
                }
                break;
            }
        }

        public static List<Room> ListRooms()
        {
            RoomService roomService = new RoomService();

            Utils.ClearConsole();
            List<Room> rooms = roomService.GetAll();


            Console.WriteLine("\n---------- Rooms ----------\n");
            for (int i = 0; i < rooms.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {rooms[i].No} Room Count: {rooms[i].RoomCount} Window Count: {rooms[i].WindowCount} | {(rooms[i].Guest != null ? "Room has a guest" : "Room is Empty")} | {(rooms[i].hasLandscape ? "Has Beautiful Landscape" : null)}");
            }
            Console.WriteLine("\n---------------------------\n");
            return rooms;
        }
    }
}
