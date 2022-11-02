using DO;
using System;
using System.IO;



namespace Dal;
static internal class DataSource
{

    //static readonly System.Random _random;
    internal static class Config
    {
        static internal int _orderId = 0;
        static internal int _orderItemId = 0;
        static internal int _orderNum = 0;
        static internal int _orderItemNum = 0;
        static internal int _productNum = 0;
        static public int RandProductId()
        {
            Random r = new Random();
            int tempRand= r.Next(100000, 1000000);
            for (int i = 0; i < _productNum; i++)
                if (tempRand == _products[i].ID)
                {
                    tempRand =r.Next(100000, 1000000);
                    i = 0;
                }
            //Console.WriteLine(tempRand);
            return tempRand;
        }
        static public int ProductId
        {
            get { return RandProductId(); }
        }
        static public int OrderId
        {
            get { return _orderId++; }
        }
        static public int OrderItemId
        {
            get { return _orderItemId++; }
        }
        //static public int OrderNum
        //{
        //    get { return _orderNum++; }
        //}
        //static public int OrderItemNum
        //{
        //    get { return _orderItemNum++; }
        //}
        //static public int ProductNum
        //{
        //    get { return _productNum++; }
        //}
    }

    static DataSource()
    {
        s_Initialize();
    }
    static private void s_Initialize()
    {
        //מערך מוצרים הכולל:שם מוצר,קטגוריית מוצר,כמות במלאי
        Console.WriteLine("h");
        (DO.Categories, string, int)[] namesOfProducts = {
            ( DO.Categories.babygrows, "babygrow Polo", 0 ),
            ( DO.Categories.shirts,"shirt Polo",88),
            ( DO.Categories.skirts, "skirt Polo",87),
            ( DO.Categories.pants,"pant Polo",24),
            ( DO.Categories.dresses,"dress Polo",24),
            ( DO.Categories.tShirt, "tShirt Polo",30),
            ( DO.Categories.babygrows,"babygrow Ralph",60),
            ( DO.Categories.shirts, "shirt Ralph",90),
            ( DO.Categories.skirts, "skirt Ralph",50),
            ( DO.Categories.pants,"pant Ralph",40)
        };
        (int, int)[] numOfItemPerOrder = {
            (0, 0 ),
            (1, 0 ),
            (2, 0 ),
            (3, 0 ),
            (4, 0 ),
            (5, 0 ),
            (6, 0 ),
            (7, 0 ),
            (8, 0 ),
            (9, 0 ),
            (10, 0 ),
            (11, 0 ),
            (12, 0 ),
            (13, 0 ),
            (14, 0 ),
            (15, 0 ),
            (16, 0 ),
            (17, 0 ),
            (18, 0 ),
            (19, 0 ),
            (20, 0 )
        };
        (string, string, string)[] Orders = {
            ( "orit", "orit@gmail.com", "brand"),
            ( "orit1", "orit1@gmail.com", "brand1"),
            ( "orit2", "orit2@gmail.com", "brand2"),
            ( "orit3", "orit3@gmail.com", "brand3"),
            ( "orit4", "orit4@gmail.com", "brand4"),
            ( "orit5", "orit5@gmail.com", "brand5"),
            ( "orit6", "orit6@gmail.com", "brand6"),
            ( "orit7", "orit7@gmail.com", "brand7"),
            ( "orit8", "orit8@gmail.com", "brand8"),
            ( "orit9", "orit9@gmail.com", "brand9"),
            ( "orit10", "orit10@gmail.com", "brand10"),
            ( "orit11", "orit11@gmail.com", "brand11"),
            ( "orit12", "orit12@gmail.com", "brand12"),
            ( "orit13", "orit13@gmail.com", "brand13"),
            ( "orit14", "orit14@gmail.com", "brand14"),
            ( "orit15", "orit15@gmail.com", "brand15"),
            ( "orit16", "orit16@gmail.com", "brand16"),
            ( "orit17", "orit17@gmail.com", "brand17"),
            ( "orit18", "orit18@gmail.com", "brand18"),
            ( "orit19", "orit19@gmail.com", "brand19")
        };

        Random r = new Random();
        DO.Product tempProduct = new DO.Product();
        for (int i = 0; i < 10; i++)
        {
            //לקיחת פרטי המוצר
            tempProduct.ID = Config.ProductId;
            Console.WriteLine(tempProduct.ID);
            int tempRand= r.Next(0, 10);
            tempProduct.Category = namesOfProducts[tempRand].Item1;
            tempProduct.Name = namesOfProducts[tempRand].Item2;
            tempProduct.InStock = namesOfProducts[tempRand].Item3;
            tempRand= r.Next(20, 150);
            tempProduct.Price = tempRand;
            AddProduct(tempProduct);
        }


        DO.Order tempOrder = new DO.Order();
        for (int i = 0; i < 20; i++)
        {
            int tempRand= r.Next(1, 20);
            tempOrder.ID = Config.OrderId;
            tempOrder.CustomerName = Orders[tempRand].Item1;
            tempOrder.CustomerEmail = Orders[tempRand].Item2;
            tempOrder.CustomerAdress = Orders[tempRand].Item3;
            tempOrder.OrderDate = DateTime.Now;
            if (i < 8)
            {
                tempRand= r.Next(2, 5);
                TimeSpan ShippingDate = new TimeSpan(tempRand, 0, 0, 0);
                tempOrder.ShipDate = tempOrder.OrderDate.Add(ShippingDate);
            }
            else
                tempOrder.ShipDate = DateTime.MinValue;
            if (i < 5)
            {
                tempRand= r.Next(4, 10);
                TimeSpan DeliveryDate = new TimeSpan(tempRand, 0, 0, 0);
                tempOrder.DeliveryDate = tempOrder.OrderDate.Add(DeliveryDate);
            }
            else
                tempOrder.DeliveryDate = DateTime.MinValue;
            AddOrder(tempOrder);
        }

        DO.OrderItem tempOrderItem = new DO.OrderItem();
        for (int i = 0; i < 40; i++)
        {
            tempOrderItem.ID = Config.OrderItemId;
            int tempRand= r.Next(0, 20);
            tempOrderItem.OrderId = tempRand;
            if (numOfItemPerOrder[tempRand].Item2 < 4)
            {
                numOfItemPerOrder[tempRand].Item2++;
            }
            else
            {
                throw new Exception("The number of order details is limited to 4.");
            }
            tempRand= r.Next(0, 10);
            tempOrderItem.ProductId = _products[tempRand].ID;
            tempOrderItem.Price = _products[tempRand].Price;
            tempRand= r.Next(1, 5);
            tempOrderItem.Amount = tempRand;

            AddOrederItem(tempOrderItem);
        }
    }
    const int SIZEOFARRAYPRUDOCT = 50;
    const int SIZEOFARRAYORDER = 100;
    const int SIZEOFARRAYORDERITEM = 200;
    static internal DO.Order[] _orders = new DO.Order[SIZEOFARRAYORDER];
    static internal DO.OrderItem[] _orderItems = new DO.OrderItem[SIZEOFARRAYORDERITEM];
    static internal DO.Product[] _products = new DO.Product[SIZEOFARRAYPRUDOCT];

    static void AddOrder(DO.Order newOrder)
    {
        _orders[Config._orderNum++] = newOrder;
    }
    static void AddOrederItem(DO.OrderItem newOrderItem)
    {
        _orderItems[Config._orderItemNum++] = newOrderItem;
    }
    static void AddProduct(DO.Product newProduct)
    {
        _products[Config._productNum++] = newProduct;
    }
}
