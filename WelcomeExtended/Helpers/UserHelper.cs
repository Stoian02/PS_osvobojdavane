using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Welcome.Model;
using WelcomeExtended.Data;

namespace WelcomeExtended.Helpers
{
    internal static class UserHelper
    {
        public static string ToString(this User user)
        {
            return $"Id: {user._id}, Name: {user._name}, Email: {user._email}, Role: {user._role}";
        }

        public static bool ValidateCredentials(this UserData userData, string name, string password)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("This {name-field} cannot be empty!");    
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("This {pass-field} cannot be empty!");    
            }
            return userData.ValidateUser(name, password);
        }

        public static User? GetUser(this UserData userData, string name, string password)
        {
            return userData.GetUser(name, password);
        }
    }
}
