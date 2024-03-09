using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;

namespace DataLayer.Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Welcome;Trusted_Connection=True;");
            string solutionFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databaseFile = "Welcome.db";
            string databasePath = Path.Combine(solutionFolder, databaseFile);
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseUser>().Property(u => u._id).ValueGeneratedOnAdd();

            // Create a user
            var user = new DatabaseUser
            {
                _id = 1,
                _name = "John Doe",
                _password = "2468",
                _role = UserRolesEnum.ADMIN,
                _expires = DateTime.Now.AddYears(10)
            };
            var user2 = new DatabaseUser
            {
                _id = 2,
                _name = "Doey",
                _password = "1122",
                _role = UserRolesEnum.INSPECTOR,
                _expires = DateTime.Now.AddYears(1)
            };
            var user3 = new DatabaseUser
            {
                _id = 3,
                _name = "Kris",
                _password = "parola",
                _role = UserRolesEnum.STUDENT,
                _expires = DateTime.Now.AddYears(1)
            };

            modelBuilder.Entity<DatabaseUser>().HasData(user);
            modelBuilder.Entity<DatabaseUser>().HasData(user2);
            modelBuilder.Entity<DatabaseUser>().HasData(user3);


        }
    }
}
