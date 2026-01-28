namespace ShoppingCart.App.Models;

public class GroceryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Available { get; set; }
    public int Amount { get; set; }
}
