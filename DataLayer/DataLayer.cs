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
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
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
                }
                else
                {
                    Console.WriteLine("Invalid data");
                }
            }
        }
    }
}
