using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.Other
{
    static class Utils
    {
        public static T GetInputFromConsole<T>(string message = "Please enter text: ", string error = "\nError -> Invalid input!\n", bool isPassword = false, bool allowEmpty = false, int rightOffset = 0, ConsoleColor messageColor = ConsoleColor.Blue, ConsoleColor inputColor = ConsoleColor.Yellow)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (input != String.Empty && !allowEmpty)
                {
                    try
                    {
                        if (Convert.ChangeType(input, typeof(T)) is T)
                        {
                            return (T)Convert.ChangeType(input, typeof(T));
                        }
                        else
                        {
                            Console.WriteLine("Invalid Format!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid Format!");
                    }
                }
                else
                {
                    Console.WriteLine("\nError -> Input is empty!\n");
                }
            }
        }
        public static void ClearConsole() => Console.Clear();

        public static string RepeatString(this string str, int repeat = 0)
        {
            string final = $"{str}";
            for(int i = 0; i < repeat; i++)
            {
                final += str;
            }
            return final;
        }
    }
}
