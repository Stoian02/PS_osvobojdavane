using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Welcome.Others;
using Welcome.Model;
using System.Security;


namespace Welcome.ViewModel
{
    public class UserViewModel
    {
        public User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }

        public string FakNumber
        {
            get { return _user._fakNum; }
            set { _user._fakNum = value; }
        }
        public string Name 
        {
            get { return _user._name; }
            set { _user._name = value; }
        }
        public string Password
        {
            get { return _user._password; }
            set { _user._password = value; }
        }

        public string Email
        {
            get { return _user._email; }
            set { _user._email = value; }
        }

        public UserRolesEnum Role
        {
            get { return _user._role; }
            set { _user._role = value; }
        }
    }
}
