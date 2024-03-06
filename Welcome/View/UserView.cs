using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Welcome.ViewModel;

namespace Welcome.View
{
    class UserView
    {
        private UserViewModel _viewModel;

        public UserView(UserViewModel viewModel)
        {
            _viewModel = viewModel;
        }
            
        public void Display()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine($"User: {_viewModel.Name}");
            Console.WriteLine($"Role: {_viewModel.Role}");
        }

        public void DisplayAll()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine($"Fak number: {_viewModel.FakNumber}");
            Console.WriteLine($"User: {_viewModel.Name}");
            Console.WriteLine($"Pass: {_viewModel.Password}");
            Console.WriteLine($"Role: {_viewModel.Role}");
            Console.WriteLine($"Email: {_viewModel.Email}");

        }

    }
}
