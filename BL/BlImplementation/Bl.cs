using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
namespace BlImplementation
{
    public class Bl:IBl
    {
        public IOrder Order =>new BlOrder();
        public ICart Cart=>new BlCart();
        public IProduct Product=>new BlProduct();
    }
}
