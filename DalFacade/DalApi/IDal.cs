using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface IDal
    {
        public IDoOrder Order { get; }
        public IDoOrderItem orderItem { get; }
        public IDoProduct product { get; }
    }
}
