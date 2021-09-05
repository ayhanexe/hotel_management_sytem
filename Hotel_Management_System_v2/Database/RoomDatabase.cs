using Hotel_Management_System_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.DatabaseService
{
    static partial class Database
    {
        public static List<Room> Rooms = new List<Room>()
        {
            new Room("001", 3, 4, false),
            new Room("002", 1, 2, false),
            //new Room("003", 1, 1, false),
            //new Room("004", 2, 2, false),
            //new Room("005", 4, 3, true),
            //new Room("006", 3, 3, true),
            //new Room("007", 5, 4, true),
            //new Room("008", 2, 2, false),
            //new Room("009", 1, 2, false),
            //new Room("0010", 4, 2, false),


        };
    }
}
