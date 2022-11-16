using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        public int Create(T item);
        public IEnumerable<T> GetAll();
        public T Read(int id);
        public void Delete(int id);
        public void Update(T item);
    }
}
