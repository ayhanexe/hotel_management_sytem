using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Hotel_Management_System_v2.Services;

namespace Hotel_Management_System_v2.ConsolePages
{
    static partial class Pages
    {
        public static void Init()
        {
            Thread.Sleep(2000);
            if (!AuthService.IsLoggedIn)
            {
                AuthPage();
            }
            else
            {
                ModesPage();
            }
        }
    }
}
