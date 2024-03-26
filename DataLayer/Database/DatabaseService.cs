using DataLayer.Others;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;

namespace DataLayer.Database
{
    public static class DatabaseService
    {
        public static List<T> GetAll<T>() where T : class
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public static void Add<T>(T entity) where T : class
        {
            using (var context = new DatabaseContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public static List<LogEntry> GetAllLogs()
        {
            return GetAll<LogEntry>();
        }
    }
}
