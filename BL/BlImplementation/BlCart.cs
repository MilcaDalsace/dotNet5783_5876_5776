using System;
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
        public BO.Cart Add(BO.Cart CurrCart, int product)
        {
            DO.Product ProductToAdd = CDal.product.Read(product);
            foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
            {
                if (Curr.ProductId == product)
                {
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
            if (ProductToAdd.InStock >= 1)
            {
                BO.OrderItem orderItemToAdd = new BO.OrderItem();
                orderItemToAdd.ProductId = product;
                orderItemToAdd.Price = ProductToAdd.Price;
                orderItemToAdd.FinalPrice = ProductToAdd.Price;
                orderItemToAdd.ProductName = ProductToAdd.Name;
                orderItemToAdd.Amount = 1;
                CurrCart.FinalPrice += orderItemToAdd.Price;
                CurrCart.ItemOrderList.Add(orderItemToAdd);
            }
            else
                throw new BO.OutOfStockExcption();
            return CurrCart;

        }
        public BO.Cart UpdateAmount(BO.Cart CurrCart, int product, int amount)
        {
            int difAmount;
            foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
            {
                if (Curr.ProductId == product)
                {
                    if (Curr.Amount > amount)
                    {
                        difAmount = Curr.Amount - amount;
                        Curr.FinalPrice -= difAmount * Curr.Price;
                        CurrCart.FinalPrice -= difAmount * Curr.Price;
                    }
                    if (Curr.Amount < amount)
                    {
                        difAmount = amount - Curr.Amount;
                        Curr.FinalPrice += difAmount * Curr.Price;
                        CurrCart.FinalPrice += difAmount * Curr.Price;
                    }
                    if (amount == 0)
                    {
                        CurrCart.FinalPrice -= Curr.Amount * Curr.Price;
                        CurrCart.ItemOrderList.Remove(Curr);
                    }
                }
            }
            throw new BO.NoSuchObjectExcption();
        }
        public void SaveCart(BO.Cart CurrCart)
        {
            DO.Order OrderToAdd = new DO.Order();
            OrderToAdd.OrderDate = DateTime.Now;
            OrderToAdd.CustomerName = CurrCart.Name;
            OrderToAdd.CustomerEmail = CurrCart.CustomerEmail;
            OrderToAdd.CustomerAdress = CurrCart.CustomerAdress;
            OrderToAdd.ShipDate = DateTime.MinValue;
            OrderToAdd.DeliveryDate = DateTime.MinValue;
            int orderId = CDal.Order.Create(OrderToAdd);
            DO.OrderItem OrderItemToAdd = new DO.OrderItem();
            DO.Product ProductToAdd;
            try
            {
                if (CurrCart.CustomerEmail != null) {
                    MailAddress m = new MailAddress(CurrCart.CustomerEmail);
                }

            }
            catch { throw new BO.OneFieldsInCorrect(); }
            if (CurrCart.Name == null || CurrCart.CustomerAdress == null)
                throw new BO.OneFieldsInCorrect();
            foreach (BO.OrderItem Curr in CurrCart.ItemOrderList) { 
                ProductToAdd = CDal.product.Read(Curr.ProductId);
                if (ProductToAdd.InStock - Curr.Amount<= 0 )
                     throw new BO.OutOfStock(); 
                if( Curr.Amount < 0)
                    throw new BO.OneFieldsInCorrect();
            }
           
            foreach (BO.OrderItem Curr in CurrCart.ItemOrderList)
            {
                    ProductToAdd = CDal.product.Read(Curr.ProductId);
                    OrderItemToAdd.Amount = Curr.Amount;
                    OrderItemToAdd.OrderId = orderId;
                    OrderItemToAdd.Price = ProductToAdd.Price;
                    OrderItemToAdd.ProductId = ProductToAdd.ID;
                    CDal.orderItem.Create(OrderItemToAdd);
                    ProductToAdd.InStock -= Curr.Amount;
                    CDal.product.Update(ProductToAdd);
            }

        }
    }
}
