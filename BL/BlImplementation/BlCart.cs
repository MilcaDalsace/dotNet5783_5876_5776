using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;
namespace BlImplementation
{
    internal class BlCart : ICart
    {
        IDal CDal = new Dal.DalList();
        /// <summary>
        /// A function that adds an item to the cart
        ///provided that: the item exists
        ///There is enough in stock
        ///You get a previous basket and the item code to add
        /// </summary>
        public BO.Cart Add(BO.Cart CurrCart, int product)
        {
            bool flag = false;
            try
            {
                //get the product from the list
                DO.Product ProductToAdd = CDal.product.Read(product);
                //Checking if the product is already in the basket and if
                //so just updating the quantity
                foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
                {
                    if (Curr.ProductId == product)
                    {
                        flag = true;
                        if (ProductToAdd.InStock >= 1)
                        {
                            Curr.Amount++;
                            Curr.FinalPrice += ProductToAdd.Price;
                            CurrCart.FinalPrice += ProductToAdd.Price;
                        }
                        else
                            throw new BO.OutOfStockExcption();
                    }
                }
                //If the product does not exist, create a product
                //in a new basket and add it to the basket
                if (!flag)
                {
                    if (ProductToAdd.InStock >= 1)
                    {
                        BO.OrderItem orderItemToAdd = new BO.OrderItem()
                        {
                            Id = 0,
                            ProductId = product,
                            Price = ProductToAdd.Price,
                            FinalPrice = ProductToAdd.Price,
                            ProductName = ProductToAdd.Name,
                            Amount = 1
                        };
                        CurrCart.FinalPrice += orderItemToAdd.Price;
                        CurrCart.ItemOrderList.Add(orderItemToAdd);

                    }
                    else
                        throw new BO.OutOfStockExcption();
                }

            }
            catch (ObjectNotFoundException ex)
            {
                //Throws an error if the data layer did not find such a product
                throw new BO.NoSuchObjectExcption(ex);
            }
            return CurrCart;

        }
        /// <summary>
        /// A function that updates the quantity of an item in the basket
        /// Provided that the product is indeed in the basket
        ///There is a quantity in stock
        ///I get a basket first
        ///item code
        ///and the updated quantity
        /// </summary>
        public BO.Cart UpdateAmount(BO.Cart CurrCart, int product, int amount)
        {
            int difAmount;
            //Searches for the product you want to install and updates it
            foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
            {
                if (Curr.ProductId == product)
                {
                    if (Curr.Amount > amount)
                    {
                        difAmount = Curr.Amount - amount;
                        Curr.Amount = amount;
                        Curr.FinalPrice -= difAmount * Curr.Price;
                        CurrCart.FinalPrice -= difAmount * Curr.Price;
                    }
                    if (Curr.Amount < amount)
                    {
                        if (amount - Curr.Amount > CDal.product.Read(product).InStock)
                            throw new BO.OutOfStockExcption();
                        difAmount = amount - Curr.Amount;
                        Curr.Amount = amount;
                        Curr.FinalPrice += difAmount * Curr.Price;
                        CurrCart.FinalPrice += difAmount * Curr.Price;
                    }
                    if (amount == 0)
                    {
                        CurrCart.FinalPrice -= Curr.Amount * Curr.Price;
                        CurrCart.ItemOrderList.Remove(Curr);
                    }
                    return CurrCart;
                }
            }
            // If it doesn't find such a product, it throws an error
            throw new BO.NoSuchObjectExcption();
        }
        /// <summary>
        /// A function that confirms an order
        ///Provided that:
        ///All fields are correct
        ///The products are found
        ///and an incomplete order set
        ///Receiving basket for confirmation
        /// </summary>
        public void SaveCart(BO.Cart CurrCart)
        {
            //chek name
            try
            {
                if (CurrCart.CustomerEmail != null)
                {
                    MailAddress m = new MailAddress(CurrCart.CustomerEmail);
                }
            }
            catch { throw new BO.OneFieldsInCorrect(); }
            if (CurrCart.Name == null || CurrCart.CustomerAdress == null)
                throw new BO.OneFieldsInCorrect();

            //try to add
            try
            {
                DO.Product ProductToAdd;
                foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
                {
                    ProductToAdd = CDal.product.Read(Curr.ProductId);
                    if (ProductToAdd.InStock - Curr.Amount < 0)
                        throw new BO.OutOfStockExcption();
                    if (Curr.Amount < 0)
                        throw new BO.OneFieldsInCorrect();
                }
                DO.Order OrderToAdd = new DO.Order()
                {
                    OrderDate = DateTime.Now,
                    CustomerName = CurrCart.Name,
                    CustomerEmail = CurrCart.CustomerEmail,
                    CustomerAdress = CurrCart.CustomerAdress,
                    ShipDate = DateTime.MinValue,
                    DeliveryDate = DateTime.MinValue
                };
                float finalPriceCart = 0;
                int orderId = CDal.Order.Create(OrderToAdd);
                foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
                {
                    
                    ProductToAdd = CDal.product.Read(Curr.ProductId);
                    DO.OrderItem OrderItemToAdd = new DO.OrderItem()
                    {
                        Amount = Curr.Amount,
                        OrderId = orderId,
                        Price = ProductToAdd.Price,
                        ProductId = ProductToAdd.ID
                    };
                    CDal.orderItem.Create(OrderItemToAdd);
                    ProductToAdd.InStock -= Curr.Amount;
                    CDal.product.Update(ProductToAdd);
                    finalPriceCart += Curr.Amount * ProductToAdd.Price;
                }

            }
            catch (TheArrayIsFull ex) { throw new BO.TheArrayIsFullException(); }

            catch (ObjectNotFoundException ex) { throw new BO.NoSuchObjectExcption(ex); };

        }
    }
}
