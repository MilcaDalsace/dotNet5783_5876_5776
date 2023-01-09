using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class  ObjectNotFoundException : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Object not found";

    }
    public class TheArrayIsFull : Exception  // execption of full list
    {
        public override string Message => "The array is full";

    }
    public class ObjectAlreadyExist : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Object already exist";

    }
    public class LimitTo4Items : Exception  // Limit to 4 items of the same type per order
    {
        public override string Message => "The number of order details is limited to 4";

    }
    public class NullException : Exception  // Limit to 4 items of the same type per order
    {
        public override string Message => "null exception";

    }
}
