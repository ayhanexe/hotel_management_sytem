using Hotel_Management_System_v2.Models;
using Hotel_Management_System_v2.Types;
using Hotel_Management_System_v2.DatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.Services
{
    class RoomService : ICrudService<Room>
    {
        public static int NextRoomId = 0;

        public bool Create(Room model)
        {
            try
            {
                Database.Rooms.Add(model);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Room room = GetById(id);

                Database.Rooms.Remove(room);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<Room> GetAll() => Database.Rooms;

        public Room GetById(int id)
        {
            return Database.Rooms.Find((Room room) => room.Id == id);
        }

        public bool Update(int id, Room model)
        {
            try
            {
                Room room = GetById(id);
                List<Room> rooms = GetAll();
                Delete(id);
                Create(new Room(model.No, model.Guest== null ? room.Guest : model.Guest, model.RoomCount, model.WindowCount, model.hasLandscape, model.LeaseExpirationDate == null ? default(DateTime) : model.LeaseExpirationDate, model.Id));

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
