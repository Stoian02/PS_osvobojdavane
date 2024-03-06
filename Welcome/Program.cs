using System;

using Welcome.Model;
using Welcome.Others;
using Welcome.View;
using Welcome.ViewModel;

namespace Welcome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            User user = new User("111111011", "Stoyo", "parola", "email@test.com", UserRolesEnum.STUDENT);

            UserViewModel userViewModel = new UserViewModel(user);

            UserView userView = new UserView(userViewModel);

            userView.Display();
            userView.DisplayAll();
        }
    }
}
