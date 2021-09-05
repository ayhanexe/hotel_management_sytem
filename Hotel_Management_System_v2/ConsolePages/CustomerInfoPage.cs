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
        public static void CustomerInformation()
        {
            UserService _userService = new UserService();

            while (true)
            {
                Utils.ClearConsole();
                Console.WriteLine(
                    "1: Create Customer\n" +
                    "2: List Customer\n" +
                    "3: Update Customer\n" +
                    "4: Delete Customer\n" +
                    "5: Back\n"
                );

                int mode = Utils.GetInputFromConsole<int>("Select Mode: ");

                if (mode == 1)
                {

                    string name = Utils.GetInputFromConsole<string>("\nName: ", "\nInvalid Format!\n");
                    string surname = Utils.GetInputFromConsole<string>("Surname: ", "\nInvalid Format!\n");

                    string username = Utils.GetInputFromConsole<string>("Username: ", "\nInvalid Format!\n");

                    string email = Utils.GetInputFromConsole<string>("Email: ", "\nInvalid Format!\n");

                    foreach (User user in Database.Users)
                    {
                        if (user.Email == email)
                        {
                            Console.WriteLine("\nUser email already exists in database!\n");
                            Thread.Sleep(2000);
                            Pages.CustomerInformation();
                            return;
                        }

                    }

                    string password = Utils.GetInputFromConsole<string>("Password: ", "\nInvalid Format!\n");

                    string passportNo = Utils.GetInputFromConsole<string>("Passport No: ", "\nInvalid Format!\n");



                    foreach (User user in Database.Users)
                    {
                        if (user.PassportNo == passportNo)
                        {
                            Console.WriteLine("\nThis passport id already exists in database!\n");
                            Thread.Sleep(2000);
                            Pages.CustomerInformation();
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
                else if (mode == 2)
                {
                    ListUsers();
                    Console.WriteLine("\nPress any key for go back customer information page...\n");
                    Console.ReadKey();
                    Pages.CustomerInformation();
                }
                else if (mode == 3)
                {
                    while (true)
                    {
                        List<User> listUsers = ListUsers();

                        int selectedUserIndex = Utils.GetInputFromConsole<int>("Select User: ") - 1;

                        try
                        {
                            User selectedUser = listUsers[selectedUserIndex];

                            Utils.ClearConsole();
                            Console.WriteLine($"\nUpdating -> {selectedUser.Name} {selectedUser.Surname} {selectedUser.PassportNo} {selectedUser.Email} {selectedUser.Room} \n");

                            string name = Utils.GetInputFromConsole<string>("\nName: ", "\nInvalid Format!\n");
                            string surname = Utils.GetInputFromConsole<string>("Surname: ", "\nInvalid Format!\n");

                            string username = Utils.GetInputFromConsole<string>("Username: ", "\nInvalid Format!\n");

                            string email = Utils.GetInputFromConsole<string>("Email: ", "\nInvalid Format!\n");

                            foreach (User user in Database.Users)
                            {
                                if (user.Email == email)
                                {
                                    Console.WriteLine("\nUser email already exists in database!\n");
                                    Thread.Sleep(2000);
                                    Pages.CustomerInformation();
                                    return;
                                }

                            }

                            string password = Utils.GetInputFromConsole<string>("Password: ", "\nInvalid Format!\n");

                            string passportNo = Utils.GetInputFromConsole<string>("Passport No: ", "\nInvalid Format!\n");



                            foreach (User user in Database.Users)
                            {
                                if (selectedUser.PassportNo != passportNo && (user.Id != selectedUser.Id && user.PassportNo == passportNo))
                                {
                                    Console.WriteLine("\nThis passport id already exists in database!\n");
                                    Thread.Sleep(2000);
                                    Pages.CustomerInformation();
                                    return;
                                }
                            }

                            while (true)
                            {
                                int wantChangeRoom = Utils.GetInputFromConsole<int>("Do you want change guest's room? (1: Yes, 0: No): ");

                                if (wantChangeRoom == 1)
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
                                            Room selectedRoom = wantChangeRoom == 1 ? Database.Rooms[roomIndex] : selectedUser.Room;


                                            User updatedUser = new User(name, surname, username, email, password, passportNo, selectedRoom, selectedUser.Role);

                                            if (wantChangeRoom == 1) {
                                                selectedRoom.LeasedDate = DateTime.Now;
                                                selectedRoom.Guest = updatedUser;
                                            }

                                            Utils.ClearConsole();

                                            bool isSuccess = _userService.Update(selectedUser.Id, updatedUser);

                                            if (isSuccess)
                                            {
                                                Console.WriteLine("\nAccount Updated Successfully!\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nAn unknown error occurred while updating account!\n");
                                            }
                                            Thread.Sleep(2000);

                                            Console.WriteLine("\nPress any key to return customer information page...!\n");
                                            Console.ReadKey();
                                            Pages.CustomerInformation();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("\nInvalid Room Data\n");
                                            Thread.Sleep(2000);
                                            continue;
                                        }
                                    }

                                    break;
                                }
                                else
                                {
                                    try
                                    {
                                        Room selectedRoom = selectedUser.Room;

                                        User updatedUser = new User(name, surname, username, email, password, passportNo, selectedRoom, selectedUser.Role);
                                        Utils.ClearConsole();

                                        bool isSuccess = _userService.Update(selectedUser.Id, updatedUser);

                                        if (isSuccess)
                                        {
                                            Console.WriteLine("\nAccount Updated Successfully!\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nAn unknown error occurred while updating account!\n");
                                        }
                                        Thread.Sleep(2000);

                                        Console.WriteLine("\nPress any key to return customer information page...!\n");
                                        Console.ReadKey();
                                        Pages.CustomerInformation();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("\nInvalid Room Data\n");
                                        Thread.Sleep(2000);
                                        continue;
                                    }
                                }
                                break;
                            }
                            break;
                        }

                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                else if (mode == 4)
                {
                    while (true)
                    {
                        Utils.ClearConsole();

                        List<User> listUsers = ListUsers();

                        int selectedIndex = Utils.GetInputFromConsole<int>("Select User: ") - 1;

                        try
                        {
                            Utils.ClearConsole();
                            User user = listUsers[selectedIndex];

                            int areYouSure = Utils.GetInputFromConsole<int>("Are you sure about that? (1:Yes, 0:No): ");

                            if (areYouSure == 1)
                            {
                                bool isSuccessfull = _userService.Delete(user.Id);
                                if (isSuccessfull)
                                {
                                    Console.WriteLine("\nUser successfully deleted!\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nUnknown error occurred!\n");
                                }
                            }
                            else
                            {
                                Pages.CustomerInformation();
                                return;
                            }

                            Thread.Sleep(2000);
                            Pages.CustomerInformation();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nInvalid Index Selected!\n");
                            Thread.Sleep(2000);
                            continue;
                        }
                    }
                }
                else
                {
                    Pages.ModesPage();
                    break;
                }
            }

        }
        public static List<User> ListUsers()
        {
            Utils.ClearConsole();

            Console.WriteLine("---------- USERS ----------");

            List<User> filteredUsers = Database.Users.FindAll((User user) => user.Role == UserRole.User);

            for (int i = 0; i < filteredUsers.Count; i++)
            {
                User user = filteredUsers[i];
                Console.WriteLine($"{i + 1}: {user.Name} {user.Surname} {user.PassportNo} {user.Email}");
            }
            Console.WriteLine("---------------------------");
            return filteredUsers;
        }
    }
}
