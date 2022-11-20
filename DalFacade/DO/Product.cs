namespace DO;

public struct Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public DO.Categories Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name} 
        category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
}


