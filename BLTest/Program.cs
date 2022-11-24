using Dal;
using DalApi;
using BLApi;
using BlImplementation;
using System.Diagnostics;
using BL;
using BO;
using System.IO;

namespace BLTest
{
    internal class Program
    {
        static private IBl ibl = new Bl();
        static void Main(string[] args)
        {
            BO.Cart UserCart = new BO.Cart() { };
            Console.WriteLine("Hello, World!");
            Console.WriteLine(
                "enter 0 to exit \n" +
                "enter 1 to Products \n" +
                "enter 2 to  orders\n" +
                "enter 3 to cart \n"
                );
            int userChoice = int.Parse(Console.ReadLine());
            int chooseMethod;
            while (userChoice != 0)
            {
                if (userChoice == 0)
                    return;
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("enter 1 to get product list \n" +
                           "enter 2 to get catalog \n" +
                           "enter 3 to get product details \n" +
                           "enter 4 to add product \n" +
                           "enter 5 to delete product \n" +
                           "enter 6 to update product \n");

                        chooseMethod = int.Parse(Console.ReadLine());
                        Product(chooseMethod);
                        break;
                    case 2:
                        Console.WriteLine("enter 1 to get product list \n" +
                           "enter 2 to get order details \n" +
                           "enter 3 to update order sent \n" +
                           "enter 4 to update order supply \n");
                        chooseMethod = int.Parse(Console.ReadLine());
                        Order(chooseMethod);
                        break;
                    case 3:
                        Console.WriteLine("enter 1 to add \n" +
                            "enter 2 to update amount \n" +
                            "enter 3 to save cart \n");
                        chooseMethod = int.Parse(Console.ReadLine());
                        Cart(chooseMethod);
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
                    int idProduct = int.Parse(Console.ReadLine());
                    try
                    {
                        Product productDetails = ibl.Product.GetProductDetails(idProduct);
                        Console.WriteLine(productDetails);
                    }
                    catch (BO.OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    break;
                case 4:
                    // add
                    Console.WriteLine("enter name,price,inStock");
                    BO.Product ProductToAdd = new BO.Product()
                    {
                        Name = Console.ReadLine(),
                        Price = int.Parse(Console.ReadLine()),
                        InStock = int.Parse(Console.ReadLine())
                    };
                    Console.WriteLine("choose category: \n" +
                      "1 to babygrows \n" +
                      "2 to shirts \n" +
                      "3 toskirts \n" +
                      "4 to pants \n" +
                      "5 to dresses \n" +
                      "6 to tShirt \n");
                    ProductToAdd.Category = (BO.Categories)int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Product.AddProduct(ProductToAdd);
                    }
                    catch (TheArrayIsFull ex)
                    {
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 5:
                    Console.WriteLine("enter id of product to delete:");
                    int idOfProduct = int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Product.DeleteProduct(idOfProduct);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        Console.WriteLine(ex.Message, ex.InnerException.Message); ;
                    }

                    catch (BO.ProductInOrder ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 6:
                    Console.WriteLine("enter id of product new name,in stock,price");
                    BO.Product productToReturn = new BO.Product()
                    {
                        ID = int.Parse(Console.ReadLine()),
                        Name = Console.ReadLine(),
                        InStock = int.Parse(Console.ReadLine()),
                        Price = float.Parse(Console.ReadLine()),
                    };
                    Console.WriteLine("choose category:" +
                        "1 to babygrows" +
                        "2 to shirts" +
                        "3 to skirts" +
                        "4 to pants" +
                        "5 to dresses" +
                        "6 to tShirt");
                    productToReturn.Category = (BO.Categories)int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Product.UpdateProduct(productToReturn);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
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
                    int idorder = int.Parse(Console.ReadLine());
                    try
                    {
                        Order orderDetails = ibl.Order.GetOrderDetails(idorder);
                        Console.WriteLine(orderDetails);
                    }
                    catch (BO.OneFieldsInCorrect ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    break;
                case 3:
                    Console.WriteLine("enter id of order to update");
                    int idOrder = int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Order.UpdateOrderSent(idOrder);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (BO.OrderAlreadyUpdate ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    Console.WriteLine("enter id of order to update");
                    int idOrderSupply = int.Parse(Console.ReadLine());
                    try
                    {
                        ibl.Order.UpdateOrderSupply(idOrderSupply);
                    }
                    catch (BO.NoSuchObjectExcption ex)
                    {
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    catch (BO.OrderAlreadyUpdate ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }
        private static void Cart(int userChoiceMethod)
        {
            switch (userChoiceMethod)
            {
                case 1:
                    Console.WriteLine("enter ");

                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    break;
            }
        }
    }

}
//switch (userChoiceMethod)
//{
//            case 1:


//                break;
//            case 2:

//                break;
//            case 3:

//                break;
//            case 4:

//                break;
//            case 5:
//                 break;
//    default:
//                break;
//}