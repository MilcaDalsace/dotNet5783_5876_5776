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
        static private DalList dalList=new DalList();
        //static private DalOrders order=new  DalOrders();
        //static private DalProduct product = new DalProduct();
        //static private DalOrderItem orderItem = new DalOrderItem();
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
            string userChoiceClass = userChoice == 1 ? "Product" : userChoice == 2 ? "Order" : userChoice == 3 ? "OrderItem" : "Exit";
            Console.WriteLine("enter 1 to add a " + userChoiceClass + "\n" +
                      "enter 2 to  show  " + userChoiceClass + " by id \n" +
                      "enter 3 to show all " + userChoiceClass + "\n" +
                      "enter 4 to update " + userChoiceClass + " \n" +
                      "enter 5 to delete a " + userChoiceClass + " \n");
            if(userChoiceClass == "OrderItem")
                    Console.WriteLine("enter 6 to show all order item by order id \n" +
                 "enter 7 to  show order item by id of orderItem n ");
                
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
                    prudoctToCreate.Category=(DO.Categories) int.Parse(Console.ReadLine());
                    try
                    {
                     Console.WriteLine(" Id of the new product is:"+ dalList.product.Create(prudoctToCreate));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    break;
                case 2:
                    Console.WriteLine("enter id of product to show");
                    try
                    {
                    Console.WriteLine( dalList.product.Read(int.Parse(Console.ReadLine())));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    IEnumerable<DO.Product> productToPrint =dalList.product.GetAll();
                    foreach (DO.Product i in productToPrint)

                    {
                        if (i.ID == 0)
                           return;
                        else
                            Console.WriteLine(i);

                    }
                    break;
                case 4:
                    Console.WriteLine("enter id of prudoct to update");
                    int idToUpdate=int.Parse(Console.ReadLine());
                    try
                    {
                        DO.Product orderTemp = dalList.product.Read(idToUpdate);
                        Console.WriteLine(orderTemp);
                        Console.WriteLine("enter new name price and amount inStock");
                    string tempName = Console.ReadLine();
                    double tempPrice=double.Parse(Console.ReadLine());
                    int tempInStock = int.Parse(Console.ReadLine());
                        prudoctToCreate.Category = orderTemp.Category;
                    prudoctToCreate.ID = idToUpdate;
                    prudoctToCreate.Name = tempName==""? prudoctToCreate.Name : tempName;
                    prudoctToCreate.Price = tempPrice ==null ? prudoctToCreate.Price : tempPrice;
                    prudoctToCreate.InStock = tempInStock == null? prudoctToCreate.InStock : tempInStock;
                    dalList.product.Update(prudoctToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    dalList.product.Delete(int.Parse(Console.ReadLine()));
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
                    Console.WriteLine("The new order id is:"+dalList.Order.Create(orderToCreate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case 2:
                    Console.WriteLine("enter id of order to show");
                    try
                    {
                        Console.WriteLine( dalList.Order.Read(int.Parse(Console.ReadLine())));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                   IEnumerable<DO.Order> orderToPrint = dalList.Order.GetAll();
                    foreach (DO.Order i in orderToPrint)

                    {
                        if (i.ID == 0)
                            return;
                        else
                            Console.WriteLine(i);
                    }
                    break;
                case 4:
                    Console.WriteLine("enter id of order to update");
                    int idToUpdate = int.Parse(Console.ReadLine());
                    try
                    {
                        DO.Order lastOrder =dalList.Order.Read(idToUpdate);
                        Console.WriteLine(lastOrder);
                        Console.WriteLine("enter new customer name customer email customer adress order date ship date and delivery date");
                    orderToCreate.ID = idToUpdate;
                    string tempName = Console.ReadLine();
                    string tempEmail = Console.ReadLine();
                    string tempAdress = Console.ReadLine();
                    DateTime tempOrderDate = DateTime.Parse(Console.ReadLine());
                    DateTime tempShipDate = DateTime.Parse(Console.ReadLine());
                    DateTime tempDeliveryDate = DateTime.Parse(Console.ReadLine());
                    orderToCreate.CustomerName =( (tempName == null) ? lastOrder.CustomerName : tempName);
                    orderToCreate.CustomerEmail = ((tempEmail == null) ? lastOrder.CustomerEmail : tempEmail);
                    orderToCreate.CustomerAdress = ((tempAdress == null) ? lastOrder.CustomerAdress : tempAdress);
                    orderToCreate.OrderDate =tempOrderDate;
                    orderToCreate.ShipDate = tempShipDate  ;
                    orderToCreate.DeliveryDate =tempDeliveryDate ;
                    dalList.Order.Update(orderToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    dalList.Order.Delete(int.Parse(Console.ReadLine()));
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
                    orderItemToCreate.OrderId = int.Parse(Console.ReadLine());
                    orderItemToCreate.ProductId = int.Parse(Console.ReadLine());
                    orderItemToCreate.Price = int.Parse(Console.ReadLine());
                    orderItemToCreate.Amount = int.Parse(Console.ReadLine());
                    try
                    {
                       Console.WriteLine(dalList.orderItem.Create(orderItemToCreate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case 2:
                    Console.WriteLine("enter id of order and id of item to show");
                    try
                    {
                        Console.WriteLine(dalList.orderItem.ReadByOrderitemId(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    IEnumerable<DO.OrderItem> orderItemToPrint = dalList.orderItem.GetAll();
                    foreach (DO.OrderItem i in orderItemToPrint)
                    {
                        if (i.ID == 0)
                            return;
                        else
                            Console.WriteLine(i);

                    }
                    break;
                case 4:
                    Console.WriteLine("enter id of order and id of product to update");
                    int idOderToUpdate = int.Parse(Console.ReadLine());
                    int productIdToUpdate = int.Parse(Console.ReadLine());
                    try
                    {
                        DO.OrderItem orderIdToUpdate = new DO.OrderItem();
                        orderIdToUpdate= dalList.orderItem.ReadByOrderitemId(idOderToUpdate, productIdToUpdate);    
                        Console.WriteLine(orderIdToUpdate);
                        Console.WriteLine("enter order id, prudoct id, price and amount");
                        orderItemToCreate.ID= orderIdToUpdate.ID;
                        double tempPrice = double.Parse(Console.ReadLine());
                        int tempAmount = int.Parse(Console.ReadLine());
                        orderItemToCreate.OrderId = idOderToUpdate;
                        orderItemToCreate.ProductId = productIdToUpdate;
                        orderItemToCreate.Price = tempPrice == null ? orderIdToUpdate.Price : tempPrice;
                        orderItemToCreate.Amount = tempAmount == null ? orderIdToUpdate.Amount : tempAmount;
                        dalList.orderItem.Update(orderItemToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
break;
                    }
                   
                    break;
                case 5:
                    Console.WriteLine("enter id of oorderItem to delete");
                    dalList.orderItem.Delete(int.Parse(Console.ReadLine()));
                    break;
                case 6:
                    Console.WriteLine("enter order id");
                    IEnumerable<DO.OrderItem> orderToShow= dalList.orderItem.ReadByOrderId(int.Parse(Console.ReadLine()));
                    if (orderToShow == null)
                        Console.WriteLine("order not found or your order is empty");
                    foreach (DO.OrderItem item in orderToShow) { 
                        if(item.OrderId==0)
                            return;
                        Console.Write(item); }
                       
                      
                    break;
                case 7:
                    Console.WriteLine("enter orderItem id");
                    try 
                    { 
                    Console.WriteLine(dalList.orderItem.Read(int.Parse(Console.ReadLine())));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

    }
}