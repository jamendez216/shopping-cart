namespace ShoppingCart.App.Models;

public class GroceryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Available { get; set; }
    public int Amount { get; set; }
    // Average lifetime in weeks; 0 means not tracked
    public int LifetimeWeeks { get; set; }
    public DateTime? PurchasedOn { get; set; }

    public bool IsDueForRestock(DateTime today) =>
        Available && LifetimeWeeks > 0 && PurchasedOn is { } p && (today - p).TotalDays >= (long)LifetimeWeeks * 7;
}
