using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;

namespace WelcomeExtended.Data
{
    internal class UserData
    {
        private List<User> _users;
        private int _nextId;

        public UserData()
        {
            _users = new List<User>();
            _nextId = 0;
        }

        public User? GetUser(string username, string password)
        {
            foreach (var user in _users)
            {
                if (user._name == username && user._password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public void AddUser(User user)
        {
            user._id = _nextId++;
            _users.Add(user);
        }
        public void DeleteUser(int id)
        {
            _users.RemoveAll(u => u._id == id);
        }
        public bool ValidateUser(string name, string password)
        {
            foreach (var user in _users)
            {
                if (user._name == name && user._password == password)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ValidateUserLambda(string name, string password)
        {
            return _users.Where(x => x._name == name && x._password == password)
                         .FirstOrDefault() != null ? true : false;
        }
        public bool ValidateUserLinq(string name, string password)
        {
            var ret = from user in _users
                      where user._name == name && user._password == password
                      select user._id;
            return ret != null ? true : false;
        }
    }
}
