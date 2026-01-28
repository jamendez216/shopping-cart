using ShoppingCart.App.Models;

namespace ShoppingCart.App.Services;

public class GroceryService
{
    private List<GroceryItem> _items = new();
    private int _nextId = 1;

    public GroceryService()
    {
        // Initialize with some sample items
        _items.Add(new GroceryItem { Id = _nextId++, Name = "Milk", Available = true, Amount = 2 });
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

    public void AddItem(string name, int amount)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _items.Add(new GroceryItem
            {
                Id = _nextId++,
                Name = name,
                Available = false,
                Amount = amount > 0 ? amount : 1
            });
        }
    }

    public void ToggleAvailable(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            item.Available = !item.Available;
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
