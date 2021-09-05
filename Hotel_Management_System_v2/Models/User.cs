using Hotel_Management_System_v2.Services;
using Hotel_Management_System_v2.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.Models
{
    class User
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _email;
        private string _password;
        private string _passportNo;
        private string _username;
        private UserRole _role = UserRole.User;
        private Room _room;

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }
        public string Surname
        {
            get { return _surname; }
        }
        public string Email
        {
            get { return _email; }
        }
        public string Password
        {
            get { return _password; }
        }
        public string PassportNo
        {
            get { return _passportNo; }
        }
        public string Username
        {
            get { return _username; }
        }

        public Room Room
        {
            get { return _room; }
            set { _room = value; } 
        }

        public UserRole Role
        {
            get { return _role; }
        }

        public User(string name, string surname, string username, string email, string password, string passportNo, Room? room, UserRole role = UserRole.User)
        {
            _id = UserService.lastId;
            _name = name;
            _surname = surname;
            _username = username;
            _email = email;
            _password = password;
            _passportNo = passportNo;
            _role = role;
            _room = room;

            UserService.lastId++;
        }

        public User(string name, string surname, string username, string email, string password, string passportNo, int id, Room? room, UserRole role = UserRole.User)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _username = username;
            _email = email;
            _password = password;
            _passportNo = passportNo;
            _role = role;
            _room = room;
        }
    }
}
