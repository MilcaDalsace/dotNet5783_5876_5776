﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    
    public class OutOfStockExcption : Exception  
    {
        public override string Message => "Out of stock";

    }
    public class NoSuchObjectExcption : Exception
    {
        public NoSuchObjectExcption(ObjectNotFoundException ex) : base("Object not found exception", ex) { }
        public NoSuchObjectExcption() { }
        public override string Message => "No such object excption";

    }
    public class OneFieldsInCorrect : Exception  //
    {
        public override string Message => "One fields inCorrect";

    }
    public class OutOfStock : Exception  //
    {
        public OutOfStock(OutOfStock ex) : base("Oout of stock", ex) { }
        public OutOfStock() { }
        public override string Message => "Out of stock";

    }
    public class TheArrayIsFullException : Exception  //
    {
        public TheArrayIsFullException(TheArrayIsFull ex) : base("the array is full", ex) { }
        public TheArrayIsFullException() { }
        public override string Message => "The array is full exception";

    }
    public class ProductInOrder : Exception  //
    {
        public override string Message => "Product in order";

    }
}
