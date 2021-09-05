using Hotel_Management_System_v2.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.ConsolePages
{
    static partial class Pages
    {
        private static void ModesPage()
        {

            while (true)
            {
                Utils.ClearConsole();
                Console.WriteLine(
                    "1: Search Available Rooms\n" +
                    "2: Booking\n" +
                    "3: Booking Details\n" +
                    "4: Check-in\n" +
                    "5: Check-out\n" +
                    "6: Room Information\n" +
                    "7: Customer Information\n" +
                    "8: Booking Reports\n" +
                    "9: Logout\n\n"
                );

                int mode = Utils.GetInputFromConsole<int>("Enter mode: ");

                if (mode == 1)
                {
                    Pages.SearchAvailableRoomsPage();
                    break;
                }
                else if (mode == 2)
                {
                    Pages.Booking();
                    break;
                }
                else if (mode == 3)
                {
                    Pages.BookingDetails();
                    break;
                }
                else if (mode == 4)
                {
                    Pages.CheckInPage();
                    break;
                }
                else if (mode == 5)
                {
                    Pages.CheckOutPage();
                    break;
                }
                else if (mode == 6)
                {
                    Pages.RoomInformationPage();
                    break;
                }
                else if (mode == 7)
                {
                    Pages.CustomerInformation();
                    break;
                }
                else if (mode == 8)
                {
                    Pages.BookingReports();
                    break;
                }
                else if (mode == 9)
                {
                    Pages.Init();
                }

                break;
            }
        }
    }
}
