using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers
{
    internal class HashLogger : ILogger
    {
        private readonly ConcurrentDictionary<int, (LogLevel logLevel, string message)> _logMessages;
        private readonly string _name;

        public HashLogger(string name)
        {
            _name = name;
            _logMessages = new ConcurrentDictionary<int, (LogLevel logLevel, string message)>();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // throw new NotImplementedException();
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            // Store the message with the eventId as the key
            _logMessages[eventId.Id] = (logLevel, message);
            // Here we actually print the log message
            PrintLog(logLevel, message);
        }

        private void PrintLog(LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("-- LOGGER --");
            Console.WriteLine($"[{logLevel}] [{_name}] {message}");
            Console.WriteLine("-- LOGGER --");
            Console.ResetColor();
        }

        // Print all stored log messages
        public void PrintAllLogs()
        {
            foreach (var logEntry in _logMessages)
            {
                Console.WriteLine($"EventId: {logEntry.Key}, Message: {logEntry.Value}");
            }
        }
        // Save all stored log messages to file
        public void SaveAllLogs()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string successFilePath = Path.Combine(docPath, "SuccessfulLogins.txt");
            string failedFilePath = Path.Combine(docPath, "FailedLogins.txt");

            try
            {
                // Using separate files for successful and failed logins for clarity
                using (StreamWriter successFile = new StreamWriter(successFilePath, true),
                       failedFile = new StreamWriter(failedFilePath, true))
                {
                    foreach (var logEntry in _logMessages)
                    {
                        var (logLevel, message) = logEntry.Value;
                        string formattedMessage = $"Timestamp: {DateTime.UtcNow}, EventId: {logEntry.Key}, Message: {message}";

                        // Determine where to log based on the log level
                        if (logLevel == LogLevel.Information) // Assuming successful login attempts are logged at Information level
                        {
                            successFile.WriteLine(formattedMessage);
                        }
                        else if (logLevel == LogLevel.Warning || logLevel == LogLevel.Error) // Assuming failed attempts are logged at Warning or Error level
                        {
                            failedFile.WriteLine(formattedMessage);
                        }
                    }
                }
                Console.WriteLine($"Successfully saved logs to {successFilePath} and {failedFilePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving logs: {ex.Message}");
            }
        }

        // Method to print a specific log by eventId
        public void PrintLogByEventId(int eventId)
        {
            if (_logMessages.TryGetValue(eventId, out (LogLevel logLevel, string message) logEntry))
            {
                Console.WriteLine($"EventId: {eventId}, LogLevel: {logEntry.logLevel}, Message: {logEntry.message}");
            }
            else
            {
                Console.WriteLine($"Log message with EventId: {eventId} not found.");
            }
        }

        // Method to delete a specific log by eventId
        public void DeleteLogByEventId(int eventId)
        {
            if (_logMessages.TryRemove(eventId, out (LogLevel logLevel, string message) logEntry))
            {
                Console.WriteLine($"Log with EventId: {eventId}, LogLevel: {logEntry.logLevel}, Message: {logEntry.message} has been deleted.");
            }
            else
            {
                Console.WriteLine($"Log message with EventId: {eventId} not found.");
            }
        }
    }
}
