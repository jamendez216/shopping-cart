using ShoppingCart.App.Models;

namespace ShoppingCart.App.Services;

public class GroceryService
{
    private List<GroceryItem> _items = new();
    private int _nextId = 1;

    public GroceryService()
    {
        // Initialize with some sample items
        _items.Add(new GroceryItem { Id = _nextId++, Name = "Milk", Available = true, Amount = 2, LifetimeWeeks = 1, PurchasedOn = DateTime.Today.AddDays(-7) });
        _items.Add(new GroceryItem { Id = _nextId++, Name = "Bread", Available = false, Amount = 1 });
        _items.Add(new GroceryItem { Id = _nextId++, Name = "Eggs", Available = false, Amount = 12 });
        _items.Add(new GroceryItem { Id = _nextId++, Name = "Cheese", Available = true, Amount = 1 });
        _items.Add(new GroceryItem { Id = _nextId++, Name = "Apples", Available = false, Amount = 6 });
    }

    public List<GroceryItem> GetItems()
    {
        // Order by Available = false first, then by Amount
        return _items
            .OrderBy(item => item.Available)
            .ThenBy(item => item.Amount)
            .ToList();
    }

    public void AddItem(string name, int amount, int lifetimeWeeks = 0)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _items.Add(new GroceryItem
            {
                Id = _nextId++,
                Name = name,
                Available = false,
                Amount = amount > 0 ? amount : 1,
                LifetimeWeeks = Math.Max(0, lifetimeWeeks)
            });
        }
    }

    public void ToggleAvailable(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            item.Available = !item.Available;
            item.PurchasedOn = item.Available ? DateTime.Today : null;
        }
    }

    public void SetLifetimeWeeks(int id, int weeks)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            item.LifetimeWeeks = Math.Max(0, weeks);
        }
    }

    public List<GroceryItem> GetSuggestedItems() =>
        _items.Where(i => i.IsDueForRestock(DateTime.Today))
              .OrderBy(i => i.LifetimeWeeks)
              .ThenBy(i => i.Name)
              .ToList();

    public void AddSuggestedToList(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            item.Available = false;
            item.PurchasedOn = null;
        }
    }

    public void DeleteItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            _items.Remove(item);
        }
    }
}
