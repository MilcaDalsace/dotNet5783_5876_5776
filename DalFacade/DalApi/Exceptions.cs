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
    public class TheArrayIsFull : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "The array is full";

    }
    public class ObjectAlreadyExist : Exception  // execption of full list
    {
        public override string Message => "Object already exist";

    }
}
