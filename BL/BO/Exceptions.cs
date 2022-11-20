using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    
    public class OutOfStockExcption : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Out of stock";

    }
    public class NoSuchObjectExcption : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "No such object excption";

    }
    public class OneFieldsInCorrect : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "One fields inCorrect";

    }
    public class OutOfStock : Exception  // מחלקה שמתארת שגיאה
    {
        public override string Message => "Out of stock";

    }
}
