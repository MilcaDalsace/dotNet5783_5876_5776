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
       static private  DalOrders order=new DalOrders();
        static private DalProduct product = new DalProduct();
        static private DalOrderItem orderItem = new DalOrderItem();
         public static void Main(String[] args)
        {
            Console.WriteLine(
                "enter 0 to exit \n" +
                "enter 1 to Products \n" +
                "enter 2 to  orders\n" +
                "enter 3 to orderItems \n"
                );
          int userChoice = int.Parse(Console.ReadLine());
            while(userChoice != 0) { 
            if (userChoice == 0)
                return;
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
                Console.WriteLine(
                   "enter 0 to exit \n" +
                   "enter 1 to Products \n" +
                   "enter 2 to  orders\n" +
                   "enter 3 to orderItems \n"
                   );
                 userChoice = int.Parse(Console.ReadLine());
            }
        }
       private static void Product(int userChoiceMethod)
        {
            DO.Product prudoctToCreate=new DO.Product();
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter nameProduct priceProduct and amountInStock");
                    prudoctToCreate.Name=Console.ReadLine();
                    prudoctToCreate.Price= double.Parse(Console.ReadLine());
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
                    DO.Product[] productToPrint =product.ReadAll(); 
                    //new DO.Product[];
                    for(int i=0;i< productToPrint.Length; i++)
                    {
                        if (productToPrint[i].ID == 0)
                            i= productToPrint.Length;
                        else
                            Console.WriteLine(productToPrint[i]);

                    }
                    
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
                    string tempName = Console.ReadLine();
                    double tempPrice=double.Parse(Console.ReadLine());
                    int tempInStock = int.Parse(Console.ReadLine());
                    prudoctToCreate.ID = idToUpdate;
                    prudoctToCreate.Name = tempName==""? prudoctToCreate.Name : tempName;
                    prudoctToCreate.Price = tempPrice ==null ? prudoctToCreate.Price : tempPrice;
                    prudoctToCreate.InStock = tempInStock == null? prudoctToCreate.InStock : tempInStock;
                    product.Update(prudoctToCreate);
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    product.Delete(int.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }
        private static void Order(int userChoiceMethod)
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
                    product.ReadAll();
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
                    string tempName = Console.ReadLine();
                    string tempEmail = Console.ReadLine();
                    string tempAdress = Console.ReadLine();
                    DateTime tempOrderDate = DateTime.Parse(Console.ReadLine());
                    DateTime tempShipDate = DateTime.Parse(Console.ReadLine());
                    DateTime tempDeliveryDate = DateTime.Parse(Console.ReadLine());
                    orderToCreate.CustomerName = tempName == "" ? orderToCreate.CustomerName : tempName;
                    orderToCreate.CustomerEmail = tempEmail == null ? orderToCreate.CustomerEmail : tempEmail;
                    orderToCreate.CustomerAdress = tempAdress == null ? orderToCreate.CustomerAdress : tempAdress;
                    orderToCreate.OrderDate = tempOrderDate == null ? orderToCreate.OrderDate : tempOrderDate;
                    orderToCreate.ShipDate = tempShipDate == null ? orderToCreate.ShipDate : tempShipDate;
                    orderToCreate.DeliveryDate = tempDeliveryDate == null ? orderToCreate.DeliveryDate : tempDeliveryDate;
                    order.Update(orderToCreate);
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    order.Delete(int.Parse(Console.ReadLine()));
                    break;
                default:
                    break;
            }
        }
        private static void OrderItem(int userChoiceMethod)
        {
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
                    product.ReadAll();
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
                    Console.WriteLine("enter order id, prudoct id, price and amount");
                    orderItemToCreate.ID= idToUpdate;
                    int tempOrderId = int.Parse(Console.ReadLine());
                    int tempProductId = int.Parse(Console.ReadLine());
                    double tempPrice = double.Parse(Console.ReadLine());
                    int tempAmount = int.Parse(Console.ReadLine());
                    orderItemToCreate.OrderId = tempOrderId == null ? orderItemToCreate.OrderId : tempOrderId;
                    orderItemToCreate.ProductId = tempProductId == null ? orderItemToCreate.ProductId : tempProductId;
                    orderItemToCreate.Price = tempPrice == null ? orderItemToCreate.Price : tempPrice;
                    orderItemToCreate.Amount = tempAmount == null ? orderItemToCreate.Amount : tempAmount;
                    orderItem.Update(orderItemToCreate);
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