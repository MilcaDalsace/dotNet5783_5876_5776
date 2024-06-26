﻿namespace DO;

public struct OrderItem
{
    public int ID { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public float Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
        OrderItem ID: {ID}, 
        OrderId: {OrderId}
        ProductId:{ProductId}
    	Price: {Price}
    	Amount : {Amount}
";
}
