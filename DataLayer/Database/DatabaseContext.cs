using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;
using DataLayer.Others;


namespace DataLayer.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string solutionFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databaseFile = "Welcome.db";
            string databasePath = Path.Combine(solutionFolder, databaseFile);
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseUser>().Property(u => u._id).ValueGeneratedOnAdd();

            // Makes the relationships between the tables (one => many)
            modelBuilder.Entity<LogEntry>()
                .HasOne(le => le.User) // LogEntry has one User
                .WithMany(u => u.LogEntries) // User has many LogEntries
                .HasForeignKey(le => le.UserId); // The foreign key is UserId in LogEntry

            // Create users
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
