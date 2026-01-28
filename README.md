# Shopping Cart - Grocery Shopping List App

A mobile-friendly .NET Blazor WebAssembly application for managing grocery shopping lists.

## Features

- ✅ Add grocery items with name and amount
- ✅ Mark items as available/unavailable with checkboxes
- ✅ Automatic sorting: unavailable items first, then by amount
- ✅ Delete items from the list
- ✅ Clean, mobile-optimized UI
- ✅ Visual distinction between available (green) and unavailable (yellow) items

## Tech Stack

- .NET 10.0
- Blazor WebAssembly
- Bootstrap 5

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the application:

```bash
cd src/ShoppingCart.App
dotnet run
```

4. Open your browser and navigate to the URL shown in the terminal (typically http://localhost:5222)

### Building for Production

```bash
dotnet publish -c Release
```

The compiled application will be in `src/ShoppingCart.App/bin/Release/net10.0/publish/wwwroot/`

## Usage

1. **Add Items**: Enter the item name and amount, then click "Add Item"
2. **Toggle Availability**: Check/uncheck the checkbox next to an item to mark it as available/unavailable
3. **Delete Items**: Click the ❌ button to remove an item from the list
4. **View Sorted List**: Items are automatically sorted with unavailable items first, then by amount

## Project Structure

- `Models/GroceryItem.cs` - Data model for grocery items
- `Services/GroceryService.cs` - Service for managing grocery items
- `Pages/Groceries.razor` - Main grocery list page component
- `Pages/Home.razor` - Home page (redirects to groceries)
- `Layout/` - Layout components and navigation

## License

MIT

