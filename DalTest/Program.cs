using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal
{
    internal class Program
    {
        static private IDal? dalList = Factory.Get();
        //new DalList();

        public static void Main(String[] args)
        {
            Console.WriteLine(
                "enter 0 to exit \n" +
                "enter 1 to Products \n" +
                "enter 2 to  orders\n" +
                "enter 3 to orderItems \n"
                );
            int userChoice;
            int.TryParse(Console.ReadLine(),out userChoice);
            while (userChoice != 0)
            {
                if (userChoice == 0)
                    return;
                string userChoiceClass = userChoice == 1 ? "Product" : userChoice == 2 ? "Order" : userChoice == 3 ? "OrderItem" : "Exit";
                Console.WriteLine("enter 1 to add a " + userChoiceClass + "\n" +
                          "enter 2 to  show  " + userChoiceClass + " by id \n" +
                          "enter 3 to show all " + userChoiceClass + "\n" +
                          "enter 4 to update " + userChoiceClass + " \n" +
                          "enter 5 to delete a " + userChoiceClass + " \n");
                if (userChoiceClass == "OrderItem")
                    Console.WriteLine("enter 6 to show all order item by order id \n" +
                 "enter 7 to  show order item by id of orderItem n ");

                int chooseMethod;
                int.TryParse(Console.ReadLine(), out chooseMethod);

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
                int.TryParse(Console.ReadLine(), out userChoice);
            }
        }
        private static void Product(int userChoiceMethod)
        {
            DO.Product prudoctToCreate = new DO.Product();
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter nameProduct priceProduct and amountInStock");
                    prudoctToCreate.Name = Console.ReadLine();
                    float TPrice;
                    int TInStock;
                    float.TryParse(Console.ReadLine(), out TPrice);
                    prudoctToCreate.Price =TPrice;
                    int.TryParse(Console.ReadLine(), out TInStock);
                    prudoctToCreate.InStock = TInStock;
                    Console.WriteLine("enter 1 to babygrows\n" +
                        "enter 2 to shirts\n" +
                        "enter 3 to skirts\n" +
                        "enter 4 to pants\n" +
                        "enter 5 to dresses\n" +
                        "enter 6 to tShirt\n");
                    int category;
                    int.TryParse(Console.ReadLine(), out category);
                    prudoctToCreate.Category = (DO.Categories)category;
                    try
                    {
                        Console.WriteLine(" Id of the new product is:" + dalList?.product.Create(prudoctToCreate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case 2:
                    Console.WriteLine("enter id of product to show");
                    try
                    {
                        int id1;
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine(dalList?.product.Read(id1));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    IEnumerable<DO.Product> productToPrint = dalList?.product.GetAll()?? throw new NullException();
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
                    int idToUpdate;
                    int.TryParse(Console.ReadLine(),out idToUpdate);
                    try
                    {
                        DO.Product orderTemp = dalList?.product.Read(idToUpdate)?? throw new NullException();
                        Console.WriteLine(orderTemp);
                        Console.WriteLine("enter new name price and amount inStock");
                        string ? tempName = Console.ReadLine();
                        float tempPrice;
                        float.TryParse(Console.ReadLine(),out tempPrice);
                        int tempInStock;
                        int.TryParse(Console.ReadLine(),out tempInStock);
                        prudoctToCreate.Category = orderTemp.Category;
                        prudoctToCreate.ID = idToUpdate;
                        prudoctToCreate.Name = tempName == "" ? prudoctToCreate.Name : tempName;
                        prudoctToCreate.Price =tempPrice;
                        prudoctToCreate.InStock =tempInStock;
                        //prudoctToCreate.Price = tempPrice == null ? prudoctToCreate.Price : tempPrice;
                        //prudoctToCreate.InStock = tempInStock == null ? prudoctToCreate.InStock : tempInStock;
                        dalList.product.Update(prudoctToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    dalList?.product.Delete(id);
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

                    //orderToCreate.OrderDate = DateTime.Parse( Console.ReadLine());
                    string? orderDate = Console.ReadLine();
                    DateTime.TryParse(orderDate, out DateTime orderDateResult);
                    orderToCreate.OrderDate = orderDateResult;

                    //orderToCreate.ShipDate = DateTime.Parse(Console.ReadLine());
                    string? shipDate = Console.ReadLine();
                    DateTime.TryParse(shipDate, out DateTime shipDateResult);
                    orderToCreate.ShipDate = shipDateResult;

                    //orderToCreate.DeliveryDate = DateTime.Parse(Console.ReadLine());
                    string? deliveryDate = Console.ReadLine();
                    DateTime.TryParse(deliveryDate, out DateTime deliveryDateResult);
                    orderToCreate.DeliveryDate = deliveryDateResult;
                    try
                    {
                        Console.WriteLine("The new order id is:" + dalList?.Order.Create(orderToCreate));
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
                        int id2;
                        int.TryParse(Console.ReadLine(), out id2);
                        Console.WriteLine(dalList?.Order.Read(id2));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    IEnumerable<DO.Order> orderToPrint = dalList?.Order.GetAll()?? throw new NullException();
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
                    int idToUpdate;
                    int.TryParse(Console.ReadLine(),out idToUpdate);
                    try
                    {
                        DO.Order lastOrder = dalList?.Order.Read(idToUpdate)?? throw new NullException();
                        Console.WriteLine(lastOrder);
                        Console.WriteLine("enter new customer name customer email customer adress order date ship date and delivery date");
                        orderToCreate.ID = idToUpdate;
                        string ? tempName = Console.ReadLine();
                        string ? tempEmail = Console.ReadLine();
                        string ? tempAdress = Console.ReadLine();

                        //DateTime tempOrderDate = DateTime.Parse(Console.ReadLine());
                        string? tempOrderDate = Console.ReadLine();
                        DateTime.TryParse(tempOrderDate, out DateTime tempOrderDateResult);
                        //DateTime tempShipDate = DateTime.Parse(Console.ReadLine());
                        string? tempShipDate = Console.ReadLine();
                        DateTime.TryParse(tempShipDate, out DateTime tempShipDateResult);
                        //DateTime tempDeliveryDate = DateTime.Parse(Console.ReadLine());
                        string? tempDeliveryDate = Console.ReadLine();
                        DateTime.TryParse(tempDeliveryDate, out DateTime tempDeliveryDateResult);
                        orderToCreate.CustomerName = ((tempName == null) ? lastOrder.CustomerName : tempName);
                        orderToCreate.CustomerEmail = ((tempEmail == null) ? lastOrder.CustomerEmail : tempEmail);
                        orderToCreate.CustomerAdress = ((tempAdress == null) ? lastOrder.CustomerAdress : tempAdress);
                        orderToCreate.OrderDate = tempOrderDateResult;
                        orderToCreate.ShipDate = tempShipDateResult;
                        orderToCreate.DeliveryDate = tempDeliveryDateResult;
                        dalList.Order.Update(orderToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    dalList?.Order.Delete(id);
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
                    int oid, pid, price, amount;
                    int.TryParse(Console.ReadLine(), out oid);
                    orderItemToCreate.OrderId =oid ;
                    int.TryParse(Console.ReadLine(), out pid);
                    orderItemToCreate.ProductId =pid;
                    int.TryParse(Console.ReadLine(),out price);
                    orderItemToCreate.Price =price ;
                    int.TryParse(Console.ReadLine(), out amount);
                    orderItemToCreate.Amount =amount;
                    try
                    {
                       Console.WriteLine(dalList?.orderItem.Create(orderItemToCreate));
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
                        int oid1, pid1;
                        int.TryParse(Console.ReadLine(), out oid1);
                        int.TryParse(Console.ReadLine(), out pid1);
                        Console.WriteLine(dalList?.orderItem.ReadByOrderitemId(oid1, pid1)?? throw new NullException());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    IEnumerable<DO.OrderItem> orderItemToPrint = dalList?.orderItem.GetAll()??throw new NullException();
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
                    int idOderToUpdate;
                    int.TryParse(Console.ReadLine(),out idOderToUpdate);
                    int productIdToUpdate;
                    int.TryParse(Console.ReadLine(),out productIdToUpdate);
                    try
                    {
                        DO.OrderItem orderIdToUpdate = new DO.OrderItem();
                        orderIdToUpdate= dalList?.orderItem.ReadByOrderitemId(idOderToUpdate, productIdToUpdate);    
                        Console.WriteLine(orderIdToUpdate);
                        Console.WriteLine("enter order id, prudoct id, price and amount");
                        orderItemToCreate.ID= orderIdToUpdate.ID;
                        float tempPrice;
                        float.TryParse(Console.ReadLine(),out tempPrice);
                        int tempAmount;
                        int.TryParse(Console.ReadLine(),out tempAmount);
                        orderItemToCreate.OrderId = idOderToUpdate;
                        orderItemToCreate.ProductId = productIdToUpdate;
                        orderItemToCreate.Price =tempPrice;
                        orderItemToCreate.Amount =tempAmount;
                        dalList?.orderItem.Update(orderItemToCreate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
break;
                    }
                   
                    break;
                case 5:
                    Console.WriteLine("enter id of orderItem to delete");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    dalList?.orderItem.Delete(id);
                    break;
                case 6:
                    Console.WriteLine("enter order id");
                    int id1;
                    int.TryParse(Console.ReadLine(),out id1);
                    IEnumerable<DO.OrderItem> orderToShow= dalList?.orderItem.GetAll(item=>item.ID==id1)?? throw new NullException();
                    if (orderToShow == null)
                        Console.WriteLine("order not found or your order is empty");
                    else
                    foreach (DO.OrderItem item in orderToShow) { 
                        if(item.OrderId==0)
                            return;
                        Console.Write(item); }
                       
                      
                    break;
                case 7:
                    Console.WriteLine("enter orderItem id");
                    try 
                    {
                        int oiid;
                        int.TryParse(Console.ReadLine(),out oiid);
                    Console.WriteLine(dalList?.orderItem.Read(oiid));
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