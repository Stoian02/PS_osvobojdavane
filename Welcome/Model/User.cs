using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Welcome.Others;

namespace Welcome.Model
{
    public class User
    {
        public virtual int _id { get; set; }
        public string _fakNum { get; set; } = "";
        private string _name;
        private string _hashedPassword = string.Empty;
        private string _email = "";
        private UserRolesEnum _role;
        private DateTime? _expires;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Password
        {
            set { _hashedPassword = value; }
            get { return _hashedPassword; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public UserRolesEnum Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public DateTime? Expires
        {
            get { return _expires; }
            set { _expires = value; }
        }

        public User(){}
        public User(int id, string fakNum, string name, string password, string email, UserRolesEnum role)
        {
            _id = id;
            _fakNum = fakNum;
            _name = name;
            Password = password;
            _email = email;
            _role = role;
        }
        public void HashPassword()
        {
            if (!string.IsNullOrEmpty(_hashedPassword))
            {
                _hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            }
        }
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _hashedPassword);
        }

        public void SetActive(DateTime validDate)
        {
            _expires = validDate;
        }
    }
}
