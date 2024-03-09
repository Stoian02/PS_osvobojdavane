﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BCrypt.Net;

using Welcome.Others;

namespace Welcome.Model
{
    public class User
    {
        public virtual int _id { get; set; }
        public string _fakNum { get; set; }
        public string _name { set; get; }
        private string _hashedPassword = string.Empty; 
        public string _password 
        {
            set { _hashedPassword = BCrypt.Net.BCrypt.HashPassword(value); }
            get { return _hashedPassword; }
        }
        public string _email { get; set; }
        public UserRolesEnum _role { set; get; }

        public DateTime? _expires { get; set; }

        public void SetActive(DateTime validDate)
        {
            _expires = validDate;
        }

        public User(){}
        public User(int id, string fakNum, string name, string pass, string email, UserRolesEnum role)
        {
            _id = id;
            _fakNum = fakNum;
            _name = name;
            _password = pass;
            _email = email;
            _role = role;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _hashedPassword);
        }
    }
}
