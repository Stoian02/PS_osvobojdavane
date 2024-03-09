using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using static WelcomeExtended.Others.Delegates;
using Microsoft.Extensions.Logging;
using WelcomeExtended.Loggers;
using WelcomeExtended.Data;
using WelcomeExtended.Helpers;

namespace WelcomeExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test if the logger works -------------------
            // Configure logger
            LoggerProvider loggerProvider = new LoggerProvider();
            ILogger logger = loggerProvider.CreateLogger("TestLogger");
            var hashLogger = logger as HashLogger;

            /*
            // Log some messages
            logger.Log(LogLevel.Information, new EventId(1), "Test message 1", null, (state, exception) => state.ToString());
            logger.Log(LogLevel.Information, new EventId(2), "Test message 2", null, (state, exception) => state.ToString());
            logger.Log(LogLevel.Error, new EventId(3), "Test error", new Exception("Something went wrong"), (state, exception) => state.ToString());

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
            // ------------------- END -------------------
            */
            try
            {
                Console.WriteLine("Example 2");
                var user = new User
                {
                    _name = "John Smith",
                    _password = "",
                    _role = UserRolesEnum.STUDENT
                };

                var viewModel = new UserViewModel(user);

                var view = new UserView(viewModel);

                view.DisplayAll();

                // Throw error here
                // view.DisplayError();
                Console.WriteLine("Example 2 END --------------------");

                Console.WriteLine("Example UserData");
                var userData = new UserData();

                User studentUser = new User
                {
                    _name = "student",
                    _password = "123",
                    _role = UserRolesEnum.STUDENT
                };
                userData.AddUser(studentUser);

                User studentUser2 = new User
                {
                    _name = "Student2",
                    _password = "123",
                    _role = UserRolesEnum.STUDENT
                };
                userData.AddUser(studentUser2);

                User teacher = new User
                {
                    _name = "Teacher",
                    _password = "1234",
                    _role = UserRolesEnum.PROFFESOR
                };
                userData.AddUser(teacher);

                User admin = new User
                {
                    _name = "Admin",
                    _password = "12345",
                    _role = UserRolesEnum.ADMIN
                };
                userData.AddUser(admin);

                string username, password;

                Console.WriteLine("Enter a username: ");
                username = Console.ReadLine();

                Console.WriteLine("Enter a password: ");
                password = Console.ReadLine();

                if (UserHelper.ValidateCredentials(userData, username, password))
                {
                    Console.WriteLine(UserHelper.ToString(UserHelper.GetUser(userData, username, password)));
                    logger.Log(LogLevel.Information, new EventId(1000), $"User {username} logged in successfully.", null, (state, exception) => state.ToString());
                }
                else
                {
                    // Log failed login
                    logger.Log(LogLevel.Warning, new EventId(1001), $"Failed login attempt for user {username}.", null, (state, exception) => state.ToString());

                    throw new Exception("The user was not found!");
                }


            }
            catch (Exception e)
            {
                var log = new ActionOnError(Log);
                log(e.Message);
            }
            finally
            {
                if (hashLogger != null)
                {
                    // Save all logs to files after the try-catch block
                    hashLogger.SaveAllLogs();
                }
                Console.WriteLine("Executed in any case!");
            }
        }
    }
}
