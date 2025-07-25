using System;

namespace ShipItApp
{
    public static class Menu
    {
        public static void DisplayLoggedOutMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("              MAIN MENU                ");
            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [1] Create User");
            Console.WriteLine(" [2] Login");
            Console.WriteLine(" [3] About");
            Console.WriteLine();
            Console.WriteLine(" [0] Exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write(" Select a Menu Option: ");
            Console.ResetColor();
        }

        public static void DisplayLoggedInMenu(User currentUser)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.WriteLine($"      Welcome {currentUser.Name}      ");
            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [1] About");
            Console.WriteLine(" [2] Show Profile");
            Console.WriteLine(" [3] Users");
            Console.WriteLine(" [4] Logout");
            Console.WriteLine();
            Console.WriteLine(" [0] Exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write(" Select a Menu Option: ");
            Console.ResetColor();
        }
    }
}
