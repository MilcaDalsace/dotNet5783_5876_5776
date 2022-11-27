using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    /// <summary>
    /// Interface for displaying a basket
    /// </summary>
    public interface ICart
    {
        public Cart Add(Cart CurrCart,int product);
        public Cart UpdateAmount(Cart CurrCart, int product,int amount);
        public void SaveCart(Cart CurrCart);
    }
}
