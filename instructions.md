# Development Instructions

Guidelines every change to this project should follow.

## Stack
- Blazor WebAssembly app in `src/ShoppingCart.App` (target framework: .NET 10).
- Models in `Models/`, state and business logic in `Services/`, UI in `Pages/` and `Layout/`.

## Code conventions
- Keep business logic in services; pages only call services and render.
- Use nullable reference types; avoid `null!` and suppressions.
- Prefer small, focused methods and descriptive names. Comment only what needs clarification.
- Razor: escape `@` in inline CSS (`@@media`).

## Domain rules
- Each grocery item has an average lifetime in weeks (`LifetimeWeeks`, 0 = not tracked).
- When an item is marked available, record `PurchasedOn`; once its lifetime elapses it is suggested for the weekly list.
- Lifetimes must stay user-adjustable at any time.

## Quality checklist
- The solution builds without warnings: `dotnet build ShoppingCart.slnx`.
- All development must ship with unit tests created alongside the change (new features and bug fixes), and all tests must pass.
- No secrets, credentials or personal data in source control.
- Update `README.md` and this file when conventions or features change.

## Workflow
- Work on a feature branch; keep commits small with clear messages.
- Open a pull request into `main` and describe what changed and why.
