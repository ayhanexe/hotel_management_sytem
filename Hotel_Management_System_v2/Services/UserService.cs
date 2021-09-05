using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel_Management_System_v2.Types;
using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.DatabaseService;
using System.Reflection;

namespace Hotel_Management_System_v2.Services
{
    class UserService : ICrudService<User>
    {
        public static int lastId = 0;

        public bool Create(User model)
        {
            try
            {
                Database.Users.Add(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Database.Users.Remove(GetById(id));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<User> GetAll() => Database.Users;

        public User GetById(int id) {
            foreach(User user in Database.Users)
            {
                if(user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public bool Update(int id, User model)
        {
            try
            {
                for (int i = 0; i < Database.Users.Count; i++)
                {
                    if (Database.Users[i].Id == id)
                    {
                        Database.Users[i] = new User(model.Name, model.Surname, model.Username, model.Email, model.Password, model.PassportNo, Database.Users[i].Id, model.Room, model.Role);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
