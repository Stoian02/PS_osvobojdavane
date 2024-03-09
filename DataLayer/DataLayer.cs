using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Welcome.Others;

namespace DataLayer
{
    internal class DataLayer
    {
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();
                context.Add<DatabaseUser>(new DatabaseUser()
                {
                    _name = "user",
                    _password = "pass",
                    _email = "user@email.com",
                    _expires = DateTime.Now,
                    _role = UserRolesEnum.STUDENT,
                });
                context.SaveChanges();
                var users = context.Users.ToList();
                Console.ReadKey();
            }

            string username, password;

            // Checking for a valid user
            using (var context = new DatabaseContext())
            {
                Console.WriteLine("Enter a username: ");
                username = Console.ReadLine();

                Console.WriteLine("Enter a password: ");
                password = Console.ReadLine();

                // Use LINQ to check if the user exists and the password matches
                bool isValidUser = context.Users.Any(u => u._name == username && u._password == password);

                if (isValidUser)
                {
                    Console.WriteLine("Валиден потребител"); // "Valid user"
                }
                else
                {
                    Console.WriteLine("Невалидни данни"); // "Invalid data"
                }
            }
        }
    }
}
