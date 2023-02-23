using System;

namespace Simulator
{
    public delegate void stopEvent();
    public delegate void StateChange(BO.Status prevStatus, BO.Status newStatus);
    public delegate void OrderSelected(BO.Order order, int second);

    public static class Simulator
    {
        private static BO.Status _prevStatus;
        private static BO.Status _nextStatus;
        private static volatile bool _toStop = false;
        public static event stopEvent? stopEvent = null;
        public static event StateChange? stateChange = null;
        public static event OrderSelected? orderSelected = null;
        public static void start()
        {
            _toStop = false;
            BO.Order allOrder;
            BLApi.IBl bl = BLApi.Factory.Get();
            int? orderId = bl.Order.OrderSelection();
            Random rand = new Random();
            Thread? t = new Thread(() =>
                {
                    orderId = bl.Order.OrderSelection();
                    int curRand = rand.Next(1000, 5000);
                    if (orderId == null)//?
                        return;
                    allOrder = bl.Order.GetOrderDetails((int)orderId);
                    _prevStatus = allOrder.OrderStatus;
                    if (orderSelected != null)
                        orderSelected(allOrder, curRand);
                    Thread.Sleep(curRand);
                    if (allOrder?.ShipDate == null)
                        bl.Order.UpdateOrderSent((int)orderId);
                    else
                    if (allOrder?.DeliveryDate == null)
                        bl.Order.UpdateOrderSupply((int)orderId);
                    _nextStatus = bl.Order.GetOrderDetails((int)orderId).OrderStatus;
                    if(stateChange!=null)
                        stateChange(_prevStatus, _nextStatus);
                })
            {

            };
        }
    }
}