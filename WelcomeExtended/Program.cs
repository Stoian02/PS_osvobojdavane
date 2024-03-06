using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using static WelcomeExtended.Others.Delegates;
using Microsoft.Extensions.Logging;
using WelcomeExtended.Loggers;

namespace WelcomeExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configure logger
            LoggerProvider loggerProvider = new LoggerProvider();
            ILogger logger = loggerProvider.CreateLogger("TestLogger");

            // Log some messages
            logger.Log(LogLevel.Information, new EventId(1), "Test message 1", null, (state, exception) => state.ToString());
            logger.Log(LogLevel.Information, new EventId(2), "Test message 2", null, (state, exception) => state.ToString());
            logger.Log(LogLevel.Error, new EventId(3), "Test error", new Exception("Something went wrong"), (state, exception) => state.ToString());

            var hashLogger = logger as HashLogger;
            if (hashLogger != null)
            {
                hashLogger.SaveAllLogs();
                hashLogger.DeleteLogByEventId(1);

                // Print a specific log message by its eventId
                Console.WriteLine("Printing a specific log message by eventId:");
                hashLogger.PrintLogByEventId(2);

                // Print all messages for testing
                Console.WriteLine("Printing all log messages:");
                hashLogger.PrintAllLogs();
            }
            else
            {
                Console.WriteLine("Logger is not of type HashLogger. Cannot print log by eventId.");
            }

            try
            {
                // Example 2
                var user = new User("112233", "John Smith", "",
                    "smith.j@email.com", UserRolesEnum.STUDENT);

                var viewModel = new UserViewModel(user);

                var view = new UserView(viewModel);

                view.DisplayAll();

                // Throw error here
                view.DisplayError();
            }
            catch (Exception e)
            {
                var log = new ActionOnError(Log);
                log(e.Message);
            }
            finally
            {
                Console.WriteLine("Executed in any case!");
            }

        }
    }
}
