using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ShipItApp
{
    class Program
    {
        private const string DataFilePath = "users.csv";
        private static User _currentUser = null;

        static void Main(string[] args)
        {
            EnsureDataFileExists();

            while (true)
            {
                Console.Clear();
                if (_currentUser == null)
                    Menu.DisplayLoggedOutMenu();
                else
                    Menu.DisplayLoggedInMenu(_currentUser);

                var choice = Console.ReadLine();
                HandleMenuChoice(choice);
            }
        }

        private static void EnsureDataFileExists()
        {
            if (!File.Exists(DataFilePath))
            {
                var sample = new[]
                {
                    "Jean Fajardo,j@example.com,Jean123!,Fajardo,PR",
                    "Zimone Williams,Z@example.com,Zimone123!,Orlando,FL",
                    "Juan Carlos,JC@example.com,Juan123!,San Juan,PR"
                };
                File.WriteAllLines(DataFilePath, sample);
            }
        }

        private static void HandleMenuChoice(string choice)
        {
            if (_currentUser == null)
            {
                switch (choice)
                {
                    case "1": CreateUser(); break;
                    case "2": Login();      break;
                    case "3": ShowAbout();  break;
                    case "0": Environment.Exit(0); break;
                }
            }
            else
            {
                switch (choice)
                {
                    case "1": ShowAbout();    break;
                    case "2": ShowProfile();  break;
                    case "3": ShowAllUsers(); break;
                    case "4": _currentUser = null; break;
                    case "0": Environment.Exit(0); break;
                }
            }
        }

        private static void CreateUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Create New User ===");
            Console.ResetColor();

            // Name
            Console.Write("Enter your name: ");
            var name = Console.ReadLine().Trim();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Name cannot be empty. Enter your name: ");
                name = Console.ReadLine().Trim();
            }

            // Email
            Console.Write("Enter your email: ");
            var email = Console.ReadLine().Trim();
            while (!Validation.ValidateEmail(email))
            {
                Console.Write("Invalid email format. Enter a valid email: ");
                email = Console.ReadLine().Trim();
            }

            // Password
            Console.Write("Enter your password (min 6 chars): ");
            var password = Console.ReadLine();
            while (!Validation.ValidatePassword(password))
            {
                Console.Write("Password too short. Enter a valid password: ");
                password = Console.ReadLine();
            }

            // City
            Console.Write("Enter your city: ");
            var city = Console.ReadLine().Trim();
            while (string.IsNullOrWhiteSpace(city))
            {
                Console.Write("City cannot be empty. Enter your city: ");
                city = Console.ReadLine().Trim();
            }

            // State
            Console.Write("Enter your state (2‑letter code): ");
            var state = Console.ReadLine().Trim();
            while (!Validation.ValidateStateAbbreviation(state))
            {
                Console.Write("Invalid state. Enter a valid 2‑letter state code: ");
                state = Console.ReadLine().Trim();
            }

            var user = new User
            {
                Name     = name,
                Email    = email,
                Password = password,
                City     = city,
                State    = state.ToUpper()
            };
            File.AppendAllLines(DataFilePath, new[]
            {
                $"{user.Name},{user.Email},{user.Password},{user.City},{user.State}"
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User created successfully.");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Login ===");
            Console.ResetColor();

            Console.Write("Enter your name: ");
            var nameInput = Console.ReadLine().Trim();
            var found = LoadUsers()
                .FirstOrDefault(u => u.Name.Equals(nameInput, StringComparison.OrdinalIgnoreCase));

            if (found != null)
            {
                Console.Write("Enter your password: ");
                var passwordInput = Console.ReadLine();
                if (found.Password == passwordInput)
                {
                    _currentUser = found;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Login successful. Welcome, {_currentUser.Name}!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid password.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found. Please create an account first.");
            }

            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static List<User> LoadUsers() =>
            File.ReadAllLines(DataFilePath)
                .Select(line => line.Split(','))
                .Where(parts => parts.Length >= 5)
                .Select(parts => new User
                {
                    Name     = parts[0],
                    Email    = parts[1],
                    Password = parts[2],
                    City     = parts[3],
                    State    = parts[4]
                })
                .ToList();

        private static void ShowProfile()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================");
            Console.WriteLine($"       Profile: {_currentUser.Name}       ");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(_currentUser.Name);
            Console.WriteLine(_currentUser.Email);
            Console.WriteLine($"{_currentUser.City}, {_currentUser.State}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write("Press any key to return to menu...");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void ShowAllUsers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("========================================");
            Console.WriteLine("                Users                  ");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            foreach (var u in LoadUsers())
            {
                Console.WriteLine(u.Name);
                Console.WriteLine(u.Email);
                Console.WriteLine($"{u.City}, {u.State}");
                Console.WriteLine();
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Press any key to return to menu...");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void ShowAbout()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ShipItApp–3‑User Sample with Password");
            Console.WriteLine("This console application tracks users (including email & password) and allows basic account management.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write("Press any key to return to menu...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
