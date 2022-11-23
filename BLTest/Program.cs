using Dal;
using DalApi;
using BLApi;
using BlImplementation;
using System.Diagnostics;
using BL;
using BO;
namespace BLTest
{
    internal class Program
    {
        static private IBl ibl = new Bl();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine(
                "enter 0 to exit \n" +
                "enter 1 to Products \n" +
                "enter 2 to  orders\n" +
                "enter 3 to cart \n"
                );
            int userChoice = int.Parse(Console.ReadLine());
            while (userChoice != 0)
            {
                if (userChoice == 0)
                    return;
                //string userChoiceClass = userChoice == 1 ? "Product" : userChoice == 2 ? "Order" : userChoice == 3 ? "Cart" : "Exit";
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("enter 1 to get product list" +
                            "enter 2 to get catalog" +
                            "enter 3 to get product details" +
                            "enter 4 to add product" +
                            "enter 5 to delete product" +
                            "enter 6 to update product");
                        int chooseMethod = int.Parse(Console.ReadLine());
                        Product(chooseMethod);
                        break;
                    case 2:
                        Order(chooseMethod);
                        break;
                    case 3:
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
                    catch (BO.NoSuchObjectExcption ex) {
                        Console.WriteLine(ex.Message, ex.InnerException.Message);
                    }
                    break;
                case 4:
                   // add
                   Console.WriteLine("");
                    break;
                case 5:
                    //delete
                    break;
                case 6:
                    //update
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