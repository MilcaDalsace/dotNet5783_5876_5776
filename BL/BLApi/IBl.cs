using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IBl
    {
        public IOrder Order { get; }
        public ICart  Cart{ get; }
        public IProduct Product { get; }
    }
}
