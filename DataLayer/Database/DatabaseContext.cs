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
    public class DatabaseContext : DbContext
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
                _fakNum = "000",
                Name = "John Doe",
                Password = "2468",
                Email = "",
                Role = UserRolesEnum.ADMIN,
                Expires = DateTime.Now.AddYears(10)
            };
            user.HashPassword(); 
            var user2 = new DatabaseUser
            {
                _id = 2,
                _fakNum = "112",
                Name = "Doey",
                Password = "1122",
                Email = "",
                Role = UserRolesEnum.INSPECTOR,
                Expires = DateTime.Now.AddYears(1)
            };
            user2.HashPassword();
            var user3 = new DatabaseUser
            {
                _id = 3,
                _fakNum = "221",
                Name = "Kris",
                Password = "parola",
                Email = "",
                Role = UserRolesEnum.STUDENT,
                Expires = DateTime.Now.AddYears(1)
            };
            user3.HashPassword();

            modelBuilder.Entity<DatabaseUser>().HasData(user);
            modelBuilder.Entity<DatabaseUser>().HasData(user2);
            modelBuilder.Entity<DatabaseUser>().HasData(user3);
        }
    }
}
