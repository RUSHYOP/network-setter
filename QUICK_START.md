# Quick Start Guide - Network Setter

## Prerequisites

Before running this application, you need to install .NET 8.0 SDK:

### Download & Install .NET 8.0 SDK
1. Go to: https://dotnet.microsoft.com/download/dotnet/8.0
2. Download the **SDK** (not just Runtime) for Windows x64
3. Run the installer
4. Restart your terminal/PowerShell after installation

### Verify Installation
Open PowerShell and run:
```powershell
dotnet --version
```

You should see version 8.0.x or higher.

## Running the Application

### Method 1: Using Batch Files (Easiest)

1. **To run directly:**
   - Right-click `run.bat`
   - Select "Run as Administrator"

2. **To build an executable:**
   - Right-click `build.bat`
   - Select "Run as Administrator"
   - The .exe will be in: `bin\Release\net8.0-windows\NetworkSetter.exe`

### Method 2: Using PowerShell

1. Open PowerShell as Administrator (Right-click → "Run as Administrator")

2. Navigate to the project directory:
   ```powershell
   cd "C:\Users\alway\Desktop\network setter"
   ```

3. Restore dependencies:
   ```powershell
   dotnet restore
   ```

4. Run the application:
   ```powershell
   dotnet run
   ```

### Method 3: Build and Run Executable

1. Open PowerShell as Administrator

2. Navigate to the project directory:
   ```powershell
   cd "C:\Users\alway\Desktop\network setter"
   ```

3. Build the project:
   ```powershell
   dotnet build -c Release
   ```

4. Run the executable:
   ```powershell
   .\bin\Release\net8.0-windows\NetworkSetter.exe
   ```

## First Time Use

1. **Select Network Adapter**: Choose your Wi-Fi or Ethernet adapter from the dropdown
2. **Choose IP Version**: Select IPv4 or IPv6
3. **View Current Settings**: The app displays your current IP configuration
4. **Save Current as Preset**: Save your current settings before making changes

## Creating Your First Preset

### Example: Home Network
1. Select "Use the following IP address"
2. Enter:
   - IP Address: `192.168.1.100`
   - Subnet Mask: `255.255.255.0`
   - Gateway: `192.168.1.1`
   - Preferred DNS: `8.8.8.8`
3. Enter preset name: "Home"
4. Click "Save Current as Preset"

### Example: DHCP Preset
1. Select "Obtain an IP address automatically (DHCP)"
2. Enter preset name: "Auto (DHCP)"
3. Click "Save Current as Preset"

## Applying Presets

1. Select a preset from the list
2. Click "Apply Preset" (or double-click it)
3. Confirm the changes
4. Wait a few seconds for network to reconfigure

## Tips

✅ **Always save your working configuration** as a preset before experimenting

✅ **Test network connectivity** after applying changes

✅ **Use descriptive preset names** like "Home WiFi", "Office Wired", "College Lab"

✅ **Keep a DHCP preset** as a fallback option

⚠️ **Administrator rights are required** - always run as admin

⚠️ **Network will briefly disconnect** when changing settings

## Troubleshooting

### "dotnet command not found"
- Install .NET 8.0 SDK (see Prerequisites above)
- Restart your terminal after installation

### "Access Denied"
- Run PowerShell or batch file as Administrator
- Right-click → "Run as Administrator"

### Changes not taking effect
- Click the "Refresh" button
- Wait 5-10 seconds after applying
- Check Windows Event Viewer for errors

### Can't find my adapter
- Make sure the adapter is enabled in Windows settings
- Click the "Refresh" button in the app

## Support

For detailed documentation, see `README.md`
