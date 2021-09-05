using Hotel_Management_System_v2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.Models
{
    class Room
    {
        private int _id;
        private string _no;
        private User _guest = null;
        private int _roomCount;
        private int _windowCount;
        private bool _hasLandscape;
        private DateTime? _leasedDate = null;
        private DateTime? _leaseExpirationDate = null;
        private DateTime? _lastCheckIn;
        private DateTime? _lastCheckOut;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string No
        {
            get { return _no; }
            set { _no = value; }
        }

        public User Guest
        {
            get { return _guest; }
            set { _guest = value; }
        }


        public int RoomCount
        {
            get { return _roomCount; }
            set { _roomCount = value; }
        }
        public int WindowCount
        {
            get { return _windowCount; }
            set { _windowCount = value; }
        }

        public bool hasLandscape
        {
            get { return _hasLandscape; }
            set { _hasLandscape = value; }
        }

        public DateTime? LeasedDate
        {
            get { return _leasedDate; }
            set { _leasedDate = value; }
        }

        public DateTime? LeaseExpirationDate
        {
            get { return _leaseExpirationDate; }
            set { _leaseExpirationDate = value; }
        }

        public DateTime? LastCheckIn
        {
            get { return _lastCheckIn; }
            set { _lastCheckIn = value; }
        }
        public DateTime? LastCheckOut
        {
            get { return _lastCheckOut; }
            set { _lastCheckOut = value; }
        }

        public Room(string no, int roomCount, int windowCount, bool hasLandscape)
        {
            _id = RoomService.NextRoomId;
            _no = no;
            _roomCount = roomCount;
            _windowCount = windowCount;
            _hasLandscape = hasLandscape;

            RoomService.NextRoomId++;
        }
        public Room(string no, int roomCount, int windowCount, bool hasLandscape, int id)
        {
            _id = id;
            _no = no;
            _roomCount = roomCount;
            _windowCount = windowCount;
            _hasLandscape = hasLandscape;
        }

        public Room(string no, User guest, int roomCount, int windowCount, bool hasLandscape, DateTime? leaseExpirationDate)
        {
            _id = RoomService.NextRoomId;
            _no = no;
            _guest = guest;
            _roomCount = roomCount;
            _windowCount = windowCount;
            _hasLandscape = hasLandscape;
            _leasedDate = DateTime.Now;
            _leaseExpirationDate = leaseExpirationDate;

            RoomService.NextRoomId++;
        }

        public Room(string no, User guest, int roomCount, int windowCount, bool hasLandscape, DateTime? leaseExpirationDate, int id)
        {
                _id = id;
                _no = no;
                _guest = guest;
                _roomCount = roomCount;
                _windowCount = windowCount;
                _hasLandscape = hasLandscape;
                _leasedDate = DateTime.Now;
                _leaseExpirationDate = leaseExpirationDate;
        }
    }
}
