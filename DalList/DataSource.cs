using DalApi;
using DO;
using System;
using System.IO;



namespace Dal;
static internal class DataSource
{

    //static readonly System.Random _random;
    internal static class Config
    {
        static internal int _orderId = 1;
        static internal int _orderItemId = 1;
        static public int RandProductId()
        {
            Random r = new Random();
            int tempRand= r.Next(100000, 1000000);
            for (int i = 0; i < _products.Count; i++)
                if (tempRand == _products[i].ID)
                {
                    tempRand =r.Next(100000, 1000000);
                    i = 0;
                }
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
    }

    static DataSource()
    {
        s_Initialize();
    }
    static private void s_Initialize()
    {
        //מערך מוצרים הכולל:שם מוצר,קטגוריית מוצר,כמות במלאי
        List<(DO.Categories, string, int)> namesOfProducts =
                new List<(DO.Categories, string, int)> {
                    ( DO.Categories.babygrows, "babygrow Polo", 0 ),
            ( DO.Categories.shirts,"shirt Polo",88),
            ( DO.Categories.skirts, "skirt Polo",87),
            ( DO.Categories.pants,"pant Polo",24),
            ( DO.Categories.dresses,"dress Polo",24),
            ( DO.Categories.tShirt, "tShirt Polo",30),
            ( DO.Categories.babygrows,"babygrow Ralph",60),
            ( DO.Categories.shirts, "shirt Ralph",90),
            ( DO.Categories.skirts, "skirt Ralph",50),
            ( DO.Categories.dresses,"dresses Ralph",40)
                };
        List< (int, int)> numOfItemPerOrder =
               new List<(int, int)> {
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
            (20, 0 ),
            (21, 0 ),
            (22, 0 ),
            (23, 0 ),
            (24, 0 ),
            (25, 0 ),
            (26, 0 ),
            (27, 0 ),
            (28, 0 ),
            (29, 0 ),
            (30, 0 ),
            (31, 0 ),
            (32, 0 ),
            (33, 0 ),
            (34, 0 ),
            (35, 0 ),
            (36, 0 ),
            (37, 0 ),
            (38, 0 ),
            (39, 0 )
               };
        List<(string, string, string)> Orders =
                new List<(string, string, string)> {
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
        //לקיחת פרטי המוצר
        for (int i = 0; i < 10; i++)
        {
            tempProduct.ID = Config.ProductId;
            int tempRand= r.Next(0, 10);
            tempProduct.Category = namesOfProducts[tempRand].Item1;
            tempProduct.Name = namesOfProducts[tempRand].Item2;
            tempProduct.InStock = namesOfProducts[tempRand].Item3;
            tempRand= r.Next(20, 150);
            tempProduct.Price = tempRand;
            AddProduct(tempProduct);
        }

        //לקיחת פרטי הזמנה
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

        //לקיחת פרטי מוצר להזמנה
        DO.OrderItem tempOrderItem = new DO.OrderItem();
        for (int i = 0; i < 40; i++)
        {
            tempOrderItem.ID = Config.OrderItemId;
            int tempRand= r.Next(0, 40);//20
            tempOrderItem.OrderId = tempRand;
            if (numOfItemPerOrder[tempRand].Item2 < 4)//i
            {
                (int, int) TempNumOfItemPerOrder = numOfItemPerOrder[tempRand];//i
                TempNumOfItemPerOrder.Item2++;
                numOfItemPerOrder[tempRand] = TempNumOfItemPerOrder;
            }
            else
            {
                throw new LimitTo4Items();//
            }
            tempRand = r.Next(0, 10);
            tempOrderItem.ProductId = _products[tempRand].ID;
            tempOrderItem.Price = _products[tempRand].Price;
            tempRand= r.Next(1, 5);
            tempOrderItem.Amount = tempRand;
            AddOrederItem(tempOrderItem);
        }
    }
    internal const int SIZEOFARRAYPRUDOCT = 50;
    internal const int SIZEOFARRAYORDER = 100;
    internal const int SIZEOFARRAYORDERITEM = 200;

    static internal List<DO.Order> _orders =new List<DO.Order> { };
    static internal List<DO.OrderItem> _orderItems = new List<DO.OrderItem> { };
    static internal List<DO.Product> _products = new List<DO.Product> { };
    static void AddOrder(DO.Order newOrder)
    {
        _orders.Add(newOrder);
    }
    static void AddOrederItem(DO.OrderItem newOrderItem)
    {
        _orderItems.Add(newOrderItem);
    }
    static void AddProduct(DO.Product newProduct)
    {
        _products.Add(newProduct);
    }
}
