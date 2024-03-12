using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Welcome.Others;
using WelcomeExtended.Data;

namespace DataLayer
{
    internal class DataLayer
    {
        static DatabaseLogger logger = new DatabaseLogger();
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(new DatabaseUser()
                {
                    _fakNum = "010",
                    Name = "user",
                    Password = BCrypt.Net.BCrypt.HashPassword("pass"),
                    Email = "user@email.com",
                    Expires = DateTime.Now,
                    Role = UserRolesEnum.STUDENT,
                });
                context.SaveChanges();
                var users = context.Users.ToList();
                Console.ReadKey();
            }

            string username, password;
            Console.WriteLine("Enter a username: ");
            username = Console.ReadLine();

            Console.WriteLine("Enter a password: ");
            password = Console.ReadLine();

            using (var context = new DatabaseContext())
            {
                // This filters at the database level, not in memory
                var user = context.Users
                    .FirstOrDefault(u => u.Name == username);

                // Verify the password in-memory
                if (user != null && user.VerifyPassword(password))
                {
                    Console.WriteLine("Valid user");
                    logger.LogInformation("The username and password are valid!");
                }
                else
                {
                    Console.WriteLine("Invalid data");
                    logger.LogError("Invalid data!");
                }
            }

            DisplayMenu();
        }
        static void DisplayMenu()
        {
            logger.Log("Information", "Displaying menu options.");

            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Get all users");
            Console.WriteLine("2. Add a new user");
            Console.WriteLine("3. Delete an existing user");
            Console.WriteLine("4. Exit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    GetAllUsers();
                    break;
                case "2":
                    AddNewUser();
                    break;
                case "3":
                    DeleteUser();
                    break;
                case "4":
                    logger.Log("Information", "Exiting the application.");
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }

            // Show the menu again
            DisplayMenu();
        }
        static void GetAllUsers()
        {
            logger.Log("Information", "Fetching all users.");
            using (var context = new DatabaseContext())
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"Name: {user.Name}");
                }
            }
        }
        static void AddNewUser()
        {
            Console.WriteLine("Enter a username: ");
            string username = Console.ReadLine();
            while (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username cannot be empty. Please enter a username: ");
                username = Console.ReadLine();
            }

            Console.WriteLine("Enter a password: ");
            string password = Console.ReadLine();
            while (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty. Please enter a password: ");
                password = Console.ReadLine();
            }

            var user = new DatabaseUser
            {
                Name = username,
                Password = password
            };

            using (var context = new DatabaseContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("User added successfully.");
            }
            logger.Log("Information", $"Adding new user: {username}.");
        }

        static void DeleteUser()
        {
            Console.WriteLine("Enter user name to delete:");
            string username = Console.ReadLine();
            logger.Log("Information", $"Attempting to delete user: {username}.");

            using (var context = new DatabaseContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == username);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    Console.WriteLine("User deleted successfully.");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
        }
    }
}
