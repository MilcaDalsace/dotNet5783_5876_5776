using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class Program
    {
       private DalOrders order=new DalOrders();
       private DalProduct product = new DalProduct();
       private DalOrderItem orderItem = new DalOrderItem();
         public void Main(String[] args)
        {
            Console.WriteLine(
                "enter 0 to exit \n" +
                "enter 1 to Products \n" +
                "enter 2 to  orders\n" +
                "enter 3 to orderItems \n"
                );
          int userChoice = int.Parse(Console.ReadLine());
            string userChoiceClass = userChoice == 1 ? "Product" : userChoice == 2 ? "Orders" : userChoice == 3 ? "OrderItem" : "Exit";
            Console.WriteLine("enter 1 to add a " + userChoiceClass + "\n" +
                      "enter 2 to  show  " + userChoiceClass + " by id \n" +
                      "enter 3 to show all " + userChoiceClass + "\n" +
                      "enter 4 to update " + userChoiceClass + " \n" +
                      "enter 5 to delete a " + userChoiceClass + " \n");

            int chooseMethod = int.Parse(Console.ReadLine());
            switch (userChoice)
            {
                case 1:
                    Product(chooseMethod);
                    break;
                case 2:
                    Order(chooseMethod);
                    break;
                case 3:
                    OrderItem(chooseMethod);
                    break;
                default:
                    break;
            }
            

        }
       private void Product(int userChoiceMethod)
        {
            DO.Product prudoctToCreate=new DO.Product();
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter nameProduct priceProduct and amountInStock");
                    prudoctToCreate.Name=Console.ReadLine();
                    prudoctToCreate. Price= int.Parse(Console.ReadLine());
                    prudoctToCreate.InStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter 1 to babygrows\n" +
                        "enter 2 to shirts\n" +
                        "enter 3 to skirts\n" +
                        "enter 4 to pants\n" +
                        "enter 5 to dresses\n" +
                        "enter 6 to tShirt\n");
                   // int CategoryChoose=int.Parse(Console.ReadLine());
                    prudoctToCreate.Category=(DO.Categories) int.Parse(Console.ReadLine());
                    try
                    {
                     product.Create(prudoctToCreate);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                    break;
                case 2:
                    Console.WriteLine("enter id of product to show");
                    try
                    {
                    Console.WriteLine( product.Read(int.Parse(Console.ReadLine())));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    //product.ReadAll();
                    break;
                case 4:
                    Console.WriteLine("enter id of prudoct to update");
                    int idToUpdate=int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(product.Read(idToUpdate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    Console.WriteLine("enter new name price and amount inStock");
                    prudoctToCreate.ID= idToUpdate;
                    prudoctToCreate.Name = Console.ReadLine();
                    prudoctToCreate.Price= int.Parse(Console.ReadLine());   
                    prudoctToCreate.InStock= int.Parse(Console.ReadLine());
                    product.UpDate(prudoctToCreate);
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    product.Delete(int.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }
        private void Order(int userChoiceMethod)
        {
            DO.Order orderToCreate = new DO.Order();
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter customerName CustomerEmail CustomerAdress OrderDate ShipDate DeliveryDate");
                    orderToCreate.CustomerName = Console.ReadLine();
                    orderToCreate.CustomerEmail = Console.ReadLine();
                    orderToCreate.CustomerAdress = Console.ReadLine();
                    orderToCreate.OrderDate = DateTime.Parse( Console.ReadLine());
                    orderToCreate.ShipDate = DateTime.Parse(Console.ReadLine());
                    orderToCreate.DeliveryDate = DateTime.Parse(Console.ReadLine());
                    try
                    {
                        order.Create(orderToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case 2:
                    Console.WriteLine("enter id of order to show");
                    try
                    {
                        Console.WriteLine(order.Read(int.Parse(Console.ReadLine())));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    //product.ReadAll();
                    break;
                case 4:
                    Console.WriteLine("enter id of prudoct to update");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(order.Read(idToUpdate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    Console.WriteLine("enter new customer name customer email customer adress order date ship date and delivery date");
                    orderToCreate.ID = idToUpdate;
                    orderToCreate.CustomerName = Console.ReadLine();
                    orderToCreate.CustomerEmail = Console.ReadLine();
                    orderToCreate.CustomerAdress = Console.ReadLine();
                    orderToCreate.OrderDate = DateTime.Parse(Console.ReadLine());
                    orderToCreate.ShipDate = DateTime.Parse(Console.ReadLine());
                    orderToCreate.DeliveryDate = DateTime.Parse(Console.ReadLine());
                    order.UpDate(orderToCreate);
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    order.Delete(int.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }
        private void OrderItem(int userChoiceMethod)
        {
        //    public int ID { get; set; }
        //public int OrderId { get; set; }

        //public int ProductId { get; set; }
        //public double Price { get; set; }
        //public int Amount { get; set; }
        DO.OrderItem orderItemToCreate = new DO.OrderItem();
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter order id prudoct id price and amount");
                    orderItemToCreate.OrderId = int.Parse( Console.ReadLine());
                    orderItemToCreate.ProductId = int.Parse(Console.ReadLine());
                    orderItemToCreate.Price = int.Parse(Console.ReadLine());
                    orderItemToCreate.Amount = int.Parse(Console.ReadLine());
                    
                    try
                    {
                        orderItem.Create(orderItemToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case 2:
                    Console.WriteLine("enter id of orderitem to show");
                    try
                    {
                        Console.WriteLine(orderItem.Read(int.Parse(Console.ReadLine())));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    //product.ReadAll();
                    break;
                case 4:
                    Console.WriteLine("enter id of orderItem to update");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(orderItem.Read(idToUpdate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    Console.WriteLine("enter order id prudoct id price and amount");
                    orderItemToCreate.ID= idToUpdate;
                    orderItemToCreate.OrderId = int.Parse(Console.ReadLine());
                    orderItemToCreate.ProductId = int.Parse(Console.ReadLine());
                    orderItemToCreate.Price = int.Parse(Console.ReadLine());
                    orderItemToCreate.Amount = int.Parse(Console.ReadLine());
                    orderItem.UpDate(orderItemToCreate);
                    break;
                case 5:
                    Console.WriteLine("enter id of oorderItem to delete");
                    orderItem.Delete(int.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }

    }
}