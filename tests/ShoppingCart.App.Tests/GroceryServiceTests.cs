using ShoppingCart.App.Models;
using ShoppingCart.App.Services;
using Xunit;

namespace ShoppingCart.App.Tests;

public class GroceryItemTests
{
    private static readonly DateTime Today = new(2026, 11, 22);

    [Theory]
    [InlineData(1_000_000)]
    [InlineData(int.MaxValue)]
    public void IsDueForRestock_HugeLifetime_DoesNotOverflow(int weeks)
    {
        var item = new GroceryItem { Available = true, LifetimeWeeks = weeks, PurchasedOn = Today.AddDays(-7) };

        Assert.False(item.IsDueForRestock(Today));
    }

    [Theory]
    [InlineData(1, 7, true)]
    [InlineData(1, 6, false)]
    [InlineData(2, 14, true)]
    [InlineData(2, 13, false)]
    public void IsDueForRestock_RespectsLifetimeWeeks(int weeks, int daysAgo, bool expected)
    {
        var item = new GroceryItem { Available = true, LifetimeWeeks = weeks, PurchasedOn = Today.AddDays(-daysAgo) };

        Assert.Equal(expected, item.IsDueForRestock(Today));
    }

    [Fact]
    public void IsDueForRestock_FalseWhenLifetimeNotTracked()
    {
        var item = new GroceryItem { Available = true, LifetimeWeeks = 0, PurchasedOn = Today.AddDays(-100) };

        Assert.False(item.IsDueForRestock(Today));
    }

    [Fact]
    public void IsDueForRestock_FalseWhenNotAvailableOrNeverPurchased()
    {
        Assert.False(new GroceryItem { Available = false, LifetimeWeeks = 1, PurchasedOn = Today.AddDays(-30) }.IsDueForRestock(Today));
        Assert.False(new GroceryItem { Available = true, LifetimeWeeks = 1, PurchasedOn = null }.IsDueForRestock(Today));
    }
}

public class GroceryServiceTests
{
    private static GroceryItem Find(GroceryService s, string name) =>
        s.GetItems().Single(i => i.Name == name);

    [Fact]
    public void AddItem_StoresLifetimeAndClampsNegative()
    {
        var service = new GroceryService();

        service.AddItem("Yogurt", 2, 3);
        service.AddItem("Rice", 1, -5);

        Assert.Equal(3, Find(service, "Yogurt").LifetimeWeeks);
        Assert.Equal(0, Find(service, "Rice").LifetimeWeeks);
    }

    [Fact]
    public void ToggleAvailable_SetsAndClearsPurchasedOn()
    {
        var service = new GroceryService();
        service.AddItem("Yogurt", 1, 1);
        var id = Find(service, "Yogurt").Id;

        service.ToggleAvailable(id);
        Assert.Equal(DateTime.Today, Find(service, "Yogurt").PurchasedOn);

        service.ToggleAvailable(id);
        Assert.Null(Find(service, "Yogurt").PurchasedOn);
    }

    [Fact]
    public void SetLifetimeWeeks_UpdatesAndClampsNegative()
    {
        var service = new GroceryService();
        var id = Find(service, "Bread").Id;

        service.SetLifetimeWeeks(id, 2);
        Assert.Equal(2, Find(service, "Bread").LifetimeWeeks);

        service.SetLifetimeWeeks(id, -1);
        Assert.Equal(0, Find(service, "Bread").LifetimeWeeks);
    }

    [Fact]
    public void GetSuggestedItems_ReturnsOnlyDueItems_OrderedByLifetime()
    {
        var service = new GroceryService();
        service.AddItem("Lettuce", 1, 2);
        service.AddItem("Cereal", 1, 1);
        service.AddItem("Rice", 1, 4);
        foreach (var name in new[] { "Lettuce", "Cereal", "Rice" })
        {
            service.ToggleAvailable(Find(service, name).Id);
        }

        Find(service, "Lettuce").PurchasedOn = DateTime.Today.AddDays(-14);
        Find(service, "Cereal").PurchasedOn = DateTime.Today.AddDays(-7);

        var suggested = service.GetSuggestedItems().Select(i => i.Name).ToList();

        Assert.Equal(new[] { "Cereal", "Milk", "Lettuce" }, suggested);
    }

    [Fact]
    public void AddSuggestedToList_MarksUnavailableAndRemovesSuggestion()
    {
        var service = new GroceryService();
        var milk = Find(service, "Milk");
        Assert.Contains(service.GetSuggestedItems(), i => i.Id == milk.Id);

        service.AddSuggestedToList(milk.Id);

        Assert.False(Find(service, "Milk").Available);
        Assert.DoesNotContain(service.GetSuggestedItems(), i => i.Id == milk.Id);
    }
}
