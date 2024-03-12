using DataLayer.Others;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public class DatabaseLogger
    {
        public void LogInformation(string message)
        {
            Log("Information", message);
        }

        public void LogError(string message)
        {
            Log("Error", message);
        }

        public void Log(string level, string message)
        {
            using (var context = new DatabaseContext())
            {
                var logEntry = new LogEntry
                {
                    Level = level,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };
                context.LogEntries.Add(logEntry);
                context.SaveChanges();
            }
        }
    }
}
