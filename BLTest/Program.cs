using Dal;
using DalApi;
using BLApi;
using BlImplementation;
using System.Diagnostics;
using BL;
using BO;
using System.IO;
using DO;

namespace BLTest
{
    internal class Program
    {
        static private IBl ibl = new Bl();
       
        static void Main(string[] args)
        { 
            BO.Cart UserCart = new BO.Cart();
            UserCart.ItemOrderList = new List<BO.OrderItem>();

            Console.WriteLine("Hello, World!");
            Console.WriteLine(
                "enter 0 to exit \n" +
                "enter 1 to Products \n" +
                "enter 2 to  orders\n" +
                "enter 3 to cart \n"
                );
            int userChoice;
            int.TryParse(Console.ReadLine(), out userChoice);
            int chooseMethod;
            ///<summary>
            ///A loop that works as long as the user's choice is not an exit
            /// If the user chooses 1 sends it to the product
            ///If 2 sends it to orders
            ///and 3 sends to the basket
            ///</summary>
            while (userChoice != 0)
            {
                if (userChoice == 0)
                    return;
                switch (userChoice)
                {
                    ///<summary>
                    ///read the customer ch
                    ///</summary>
                    case 1:
                        Console.WriteLine("enter 1 to get product list \n" +
                           "enter 2 to get catalog \n" +
                           "enter 3 to get product details \n" +
                           "enter 4 to add product \n" +
                           "enter 5 to delete product \n" +
                           "enter 6 to update product \n");
                        int.TryParse(Console.ReadLine(), out chooseMethod);
                        Product(chooseMethod);
                        break;
                    case 2:
                        Console.WriteLine("enter 1 to get order list \n" +
                           "enter 2 to get order details \n" +
                           "enter 3 to update order sent \n" +
                           "enter 4 to update order supply \n"+
                            "enter 5 to update order  \n");
                        int.TryParse(Console.ReadLine(), out chooseMethod);
                        Order(chooseMethod);
                        break;
                    case 3:
                        Console.WriteLine("enter 1 to add \n" +
                            "enter 2 to update amount \n" +
                            "enter 3 to save cart \n");
                        int.TryParse(Console.ReadLine(), out chooseMethod);
                        UserCart = Cart(chooseMethod, UserCart);
                        break;
                    default:
                        break;
                }
                Console.WriteLine(
                   "enter 0 to exit \n" +
                   "enter 1 to Products \n" +
                   "enter 2 to  orders\n" +
                   "enter 3 to cart \n"
                   );
                int.TryParse(Console.ReadLine(), out userChoice);
            }
        }
        private static void Product(int userChoiceMethod)
        {
            switch (userChoiceMethod)
            {
                case 1:
                    IEnumerable<ProductForList> ListOfProduct = ibl.Product.GetProductList();
                    foreach (ProductForList productForList in ListOfProduct)
                        Console.WriteLine(productForList);
                    break;
                case 2:
                    IEnumerable<ProductItem> catalog = ibl.Product.GetCatalog();
                    foreach (ProductItem catalogItem in catalog)
                        Console.WriteLine(catalogItem);
                    break;
                case 3:
                    Console.WriteLine("enter id of product");
                    int idProduct;
                    int.TryParse(Console.ReadLine(), out idProduct);
                    try
                    {
                        BO.Product productDetails = ibl.Product.GetProductDetails(idProduct);
                        Console.WriteLine(productDetails);
                    }
                    catch (BO.OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    break;
                case 4:
                    // add
                    Console.WriteLine("enter name,price,inStock");
                    int TPrice, TInstock ;
                    int.TryParse(Console.ReadLine(), out TPrice);
                    int.TryParse(Console.ReadLine(), out TInstock); ;

                    BO.Product ProductToAdd = new BO.Product()
                    {
                        Name = Console.ReadLine(),
                        Price = TPrice,
                        InStock =TInstock
                    };
                    Console.WriteLine("choose category: \n" +
                      "1 to babygrows \n" +
                      "2 to shirts \n" +
                      "3 toskirts \n" +
                      "4 to pants \n" +
                      "5 to dresses \n" +
                      "6 to tShirt \n");
                    int category;
                    int.TryParse(Console.ReadLine(), out category);
                    ProductToAdd.Category = (BO.Categories)category;
                    //ProductToAdd.Category = (BO.Categories)int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Product.AddProduct(ProductToAdd);
                    }
                    catch (TheArrayIsFull ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete:");
                    int idOfProduct;
                    int.TryParse(Console.ReadLine(), out idOfProduct);
                   // int idOfProduct = int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Product.DeleteProduct(idOfProduct);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message); ;
                    }

                    catch (BO.ProductInOrder ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 6:
                    Console.WriteLine("enter id of product new name,in stock,price");
                    int TId, TInstck;
                    float TPrice1;
                    int.TryParse(Console.ReadLine(), out TId);
                    int.TryParse(Console.ReadLine(), out TInstck);
                    float.TryParse(Console.ReadLine(), out TPrice1);
                    BO.Product productToReturn = new BO.Product()
                    {
                        ID =TId,
                        Name = Console.ReadLine(),
                        InStock = TInstck,
                        Price = TPrice1,
                    };
                    Console.WriteLine("choose category:" +
                        "1 to babygrows" +
                        "2 to shirts" +
                        "3 to skirts" +
                        "4 to pants" +
                        "5 to dresses" +
                        "6 to tShirt");
                    int category1;
                    int.TryParse(Console.ReadLine(), out category1);
                    productToReturn.Category = (BO.Categories)category1;
                    try
                    {
                        ibl.Product.UpdateProduct(productToReturn);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    break;
                default:
                    break;
            }
        }
        private static void Order(int userChoiceMethod)
        {
            switch (userChoiceMethod)
            {
                case 1:
                    IEnumerable<OrderForList> ListOfOrder = ibl.Order.GetListOrder();
                    foreach (OrderForList orderForList in ListOfOrder)
                        Console.WriteLine(orderForList);
                    break;
                case 2:
                    Console.WriteLine("enter id of order");
                    int idorder;
                    int.TryParse(Console.ReadLine(), out idorder);
                    try
                    {
                        BO.Order orderDetails = ibl.Order.GetOrderDetails(idorder);
                        Console.WriteLine(orderDetails);
                    }
                    catch (BO.OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    break;
                case 3:
                    Console.WriteLine("enter id of order to update");
                    int idOrder;
                    int.TryParse(Console.ReadLine(), out idOrder);
                    try
                    {
                        ibl.Order.UpdateOrderSent(idOrder);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (BO.OrderAlreadyUpdate ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    Console.WriteLine("enter id of order to update");
                    int idOrderSupply;
                    int.TryParse(Console.ReadLine(), out idOrderSupply);
                    try
                    {
                        ibl.Order.UpdateOrderSupply(idOrderSupply);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (BO.OrderAlreadyUpdate ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (BO.OrderDidnotsentAlready ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break ;
                    case 5:
                    Console.WriteLine("enter 1 to delete item" +
                        "2 to add item" +
                        "3 to update item");
                    int action;
                    int.TryParse(Console.ReadLine(), out action);
                    Console.WriteLine("enter order id ,product id");
                    int idOrderToUpdate;
                    int.TryParse(Console.ReadLine(), out idOrderToUpdate);
                    int itemId;
                    int.TryParse(Console.ReadLine(), out itemId);
                    if (action!=1)
                    {
                        Console.WriteLine("enter new amount");
                        int newAmount;
                        int.TryParse(Console.ReadLine(), out newAmount);
                        ibl.Order.UpdateOrder(idOrderToUpdate, itemId, newAmount,action );
                    }
                    else
                    {
                        ibl.Order.UpdateOrder(idOrderToUpdate, itemId, 0, action);
                    }
                    break;
                default:
                    break;
            }
        }
        private static BO.Cart Cart(int userChoiceMethod,BO.Cart userCart)
        {
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter id of product to add");
                    try {
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        userCart = ibl.Cart.Add(userCart, id);
                        Console.WriteLine(userCart);
                        return userCart;
                    }
                    catch(BO.OutOfStockExcption ex)
                    {
                        Console.WriteLine(ex.Message);
                        return userCart;
                    }
                   catch (BO.NoSuchObjectExcption ex){
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                        return userCart;
                    }
                case 2:
                    Console.WriteLine("enter id of product and update amount");
                    try
                    {
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        int amount;
                        int.TryParse(Console.ReadLine(), out amount);
                        userCart = ibl.Cart.UpdateAmount(userCart,id,amount);
                        Console.WriteLine(userCart);
                        return userCart;
                    }
                    catch (BO.OutOfStockExcption ex)
                    {
                        Console.WriteLine(ex.Message);
                        return userCart;
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        Console.WriteLine(ex.Message);
                        return userCart;
                    }
                case 3:
                    Console.WriteLine("enter user name,adress,email");
                    userCart.Name = Console.ReadLine();
                    userCart.CustomerAdress = Console.ReadLine();
                    userCart.CustomerEmail = Console.ReadLine();
                    try
                    {
                        ibl.Cart.SaveCart(userCart);
                        Console.WriteLine(userCart);
                        userCart = new Cart();
                        userCart.ItemOrderList = new List<BO.OrderItem>();
                    }
                    catch(BO.OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (BO.TheArrayIsFullException ex)
                    {
                        if (ex.InnerException != null)
                            Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        if(ex.InnerException != null)
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (BO.OutOfStockExcption ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return userCart;
                default:
                    break;
            }
            return userCart;
        }
    }

}
