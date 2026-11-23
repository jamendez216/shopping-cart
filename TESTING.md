# Testing Locally and on Your Phone

## Prerequisites
- .NET 10 SDK (`dotnet --version` should print 10.x)

## Run the unit tests
```powershell
dotnet test ShoppingCart.slnx
```

## Run the app locally
```powershell
dotnet run --project src/ShoppingCart.App --launch-profile http
```
Open http://localhost:5222/groceries (HTTPS profile: https://localhost:7289).

## Test on your phone

### Option A: Same Wi-Fi (quickest)
1. Connect the phone and PC to the same Wi-Fi network.
2. Find the PC's IP: `ipconfig` (IPv4 address, e.g. `192.168.1.50`).
3. Run the app listening on all interfaces:
   ```powershell
   dotnet run --project src/ShoppingCart.App --launch-profile http --urls "http://0.0.0.0:5222"
   ```
4. Allow it through Windows Firewall (admin PowerShell):
   ```powershell
   New-NetFirewallRule -DisplayName "ShoppingCart dev" -Direction Inbound -Protocol TCP -LocalPort 5222 -Action Allow
   ```
5. On the phone open `http://<PC-IP>:5222/groceries`.

Remove the rule when done: `Remove-NetFirewallRule -DisplayName "ShoppingCart dev"`.

### Option B: Dev tunnel (works over mobile data, gives HTTPS)
```powershell
winget install Microsoft.devtunnel
devtunnel user login
devtunnel host -p 5222 --allow-anonymous
```
Start the app with the `http` profile, then open the tunnel URL printed by `devtunnel` on your phone.

### Option C: Published build hosted statically
```powershell
dotnet publish src/ShoppingCart.App -c Release -o publish
```
Serve `publish/wwwroot` from any static host (e.g. GitHub Pages, Azure Static Web Apps) and open the URL on the phone.
If hosted under a sub-path (GitHub Pages), set `<base href="/repo-name/" />` in `wwwroot/index.html`.

## Notes
- App data is in memory only; it resets on every reload.
- Use browser dev tools device emulation (F12 > toggle device toolbar) for a quick mobile layout check before using the phone.
- Add to home screen from the phone browser menu for an app-like experience.
