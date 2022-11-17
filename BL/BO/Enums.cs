using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Status
    {
       received,
       sent,
       arrived
    }
    public enum Categories
    {
        babygrows = 1,
        shirts,
        skirts,
        pants,
        dresses,
        tShirt
    }
    public enum TypesOfActions
    {
        Create,
        Read,
        ReadAll,
        Update,
        Delete
    }
}
