using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.DatabaseService
{
    static partial class Database
    {
        public static List<User> Users = new List<User>() {
            new User("Ayxan", "Abdullayev", "0x21106.js", "a", "a", "AZE001", null, UserRole.Admin),
            new User("Alixandro", "Delluci", "ali", "aa", "aa", "AZE002", null, UserRole.User),
            new User("Alixandro", "Delluci", "ali", "ab", "ab", "AZE005", null, UserRole.User),
        };
    }
}
