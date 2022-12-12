using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//e.	נוסיף ב-DalApi.ICrud
//מתודת בקשה של אובייקט ע"פ תנאי שתקבל פרמטר כנ"ל (אך לא אופציונלי - ללא ערך ברירת מחדל),
//ונממש את המתודה עבור כל ישויות הנתונים בהתאם
namespace DalApi
{
    public interface ICrud<T>
    {
        public int Create(T item);
        //public  GetAll();
        public IEnumerable<T> GetAll(Func<T, bool>? func=null);
        public T Read(int id);
        public T ReadByFunc(Predicate<T> func);

        public void Delete(int id);
        public void Update(T item);
    }
}
