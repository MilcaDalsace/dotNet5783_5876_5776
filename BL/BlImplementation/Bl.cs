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
        //לא ידענו אם לשנות לפי הוראוות שלב 4 4 עמוד 8
        public IOrder Order => new BlOrder();
        public ICart Cart=> new BlCart();
        public IProduct Product=> new BlProduct();
    }
}
