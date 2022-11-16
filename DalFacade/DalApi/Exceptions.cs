using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal class ObjectNotFoundException : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "object not found";

    }
    internal class ObjectAlreadyExist : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "object already exist";

    }
}
