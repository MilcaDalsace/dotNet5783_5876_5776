using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        internal int Add(T item);
        internal IEnumerable<T> GetAll();
        internal T Read(int id);
        internal void Delete();
        internal void Update(T item);
    }
}
