using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using static WelcomeExtended.Others.Delegates;

namespace WelcomeExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
