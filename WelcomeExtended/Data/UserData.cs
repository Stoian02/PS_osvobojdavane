﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Welcome.Model;
using Welcome.Others;

namespace WelcomeExtended.Data
{
    class UserData
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
                if (user.Name == username && user.VerifyPassword(password))
                {
                    return user;
                }
            }
            return null;
        }
        // SetActive ??

        public void AssignUserRole(string username, UserRolesEnum role)
        {
            var user = _users.FirstOrDefault(x => x.Name == username);
            if (user != null)
            {
                user.Role = role;
            }
            else
            {
                throw new InvalidOperationException($"User with name '{username}' not found.");
            }
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
                if (user.Name == name && user.VerifyPassword(password))
                {
                    return true;
                }
            }
            return false;
        }
        public bool ValidateUserLambda(string name, string password)
        {
            return _users.Where(x => x.Name == name && x.Password == password)
                         .FirstOrDefault() != null ? true : false;
        }
        public bool ValidateUserLinq(string name, string password)
        {
            var ret = from user in _users
                      where user.Name == name && user.Password == password
                      select user._id;
            return ret != null ? true : false;
        }
    }
}
