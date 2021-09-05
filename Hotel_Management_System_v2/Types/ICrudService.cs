using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_v2.Types
{
    interface ICrudService<T>
    {
        public bool Create(T model);
        public T GetById(int id);
        public List<T> GetAll();
        public bool Update(int id, T model);
        public bool Delete(int id);
    }
}
