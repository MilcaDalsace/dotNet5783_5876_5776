using BLApi;
using BO;
using System;

namespace Simulator
{
    public static class SimulatorClass
    {
        public delegate void StatusChanged(BO.Order order, string newStatus, DateTime prev, DateTime next);
        public static event StatusChanged? StatusChangedEvent = null;
        public delegate void FinishSimulator(DateTime end, string reason = "");
        public static event FinishSimulator? FinishSimulatorEvent = null;
        volatile private static bool stopRequest = false;
        public static bool IsAlive { get; set; } = false;
        public static void startSimulator()
        {
            IsAlive = true;
            Thread thread = new Thread(Run);
            stopRequest = false;
            thread.Start();
        }

        public static void Run()
        {
            string newStatus="";
            IBl bl = BLApi.Factory.Get();
            Random random = new Random();
            string reasonOfStopping = "";
            while (!stopRequest)
            {
                int? curOrderId = bl?.Order.OrderSelection();
                if (curOrderId == null)
                {
                    stopRequest = true;
                    reasonOfStopping = "no orders to update";
                    break;
                }
                BO.Order curOrder = bl.Order.GetOrderDetails(Convert.ToInt32(curOrderId));
                if (stopRequest)
                    break;
                int CurrentHandleTime = random.Next(1000, 5000);
                BO.Status? prevStatus = curOrder?.OrderStatus;
                DateTime startOfChange = DateTime.Now;
                Thread.Sleep(CurrentHandleTime);
                if (curOrder?.OrderStatus == BO.Status.received)
                {
                    bl?.Order.UpdateOrderSent(Convert.ToInt32(curOrderId));
                    newStatus = "sent";
                }
                else
                    if (curOrder?.OrderStatus == BO.Status.sent)
                {
                    bl?.Order.UpdateOrderSupply(Convert.ToInt32(curOrderId));
                    newStatus = "Supply";
                }
                DateTime endOfChange = DateTime.Now;
                StatusChangedEvent?.Invoke(curOrder ?? throw new Exception(), newStatus, startOfChange, endOfChange);
                Thread.Sleep(1000);
            }
            FinishSimulatorEvent?.Invoke(DateTime.Now, reasonOfStopping);
        }
        public static void Stop()
        {
            stopRequest = true;
            IsAlive = false;
        }
    }
}
















//public delegate void stopEvent();
//public delegate void StateChange(BO.Status prevStatus, BO.Status newStatus);
//public delegate void OrderSelected(BO.Order order, int second);
//private static BO.Status _prevStatus;
//private static BO.Status _nextStatus;
//private static volatile bool _toStop = false;
//public static event stopEvent? stopEvent = null;
//public static event StateChange? stateChange = null;
//public static event OrderSelected? orderSelected = null;


//_toStop = false;
//BO.Order allOrder;
//BLApi.IBl bl = BLApi.Factory.Get();
//int? orderId = bl.Order.OrderSelection();
//Random rand = new Random();
//Thread? t = new Thread(() =>
//{
//    orderId = bl.Order.OrderSelection();
//    int curRand = rand.Next(1000, 5000);
//    if (orderId == null)//?
//        return;
//    allOrder = bl.Order.GetOrderDetails((int)orderId);
//    _prevStatus = allOrder.OrderStatus;
//    if (orderSelected != null)
//        orderSelected(allOrder, curRand);
//    Thread.Sleep(curRand);
//    if (allOrder?.ShipDate == null)
//        bl.Order.UpdateOrderSent((int)orderId);
//    else
//    if (allOrder?.DeliveryDate == null)
//        bl.Order.UpdateOrderSupply((int)orderId);
//    _nextStatus = bl.Order.GetOrderDetails((int)orderId).OrderStatus;
//    if (stateChange != null)
//        stateChange(_prevStatus, _nextStatus);
//})
//{

//};