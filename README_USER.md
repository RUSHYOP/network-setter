# Network Setter - IP Configuration Manager

A Windows Forms application for managing network adapter TCP/IP settings with preset support for IPv4 and IPv6.

## Features

- ✅ **IPv4 and IPv6 Support**: Configure both IP versions
- ✅ **DHCP or Static IP**: Switch between automatic and manual configuration
- ✅ **Network Presets**: Save and load different network configurations (Home, Work, College, etc.)
- ✅ **Real-time Display**: Shows current IP, Gateway, Subnet, and DNS settings
- ✅ **Easy Management**: Apply presets with one click
- ✅ **Administrator Mode**: Automatically requests elevated permissions

## Requirements

- Windows 10/11
- .NET 8.0 SDK or Runtime
- Administrator privileges (required to change network settings)

## Installation

### Option 1: Build from Source

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Open a terminal in the project directory
3. Run the build command:
   ```powershell
   dotnet build -c Release
   ```
4. The executable will be in: `bin\Release\net8.0-windows\NetworkSetter.exe`

### Option 2: Run Directly

```powershell
dotnet run
```

## Usage

### 1. Launch the Application
- Right-click `NetworkSetter.exe` and select "Run as Administrator"
- Or run `dotnet run` from an elevated PowerShell

### 2. View Current Settings
- Select your network adapter from the dropdown
- Choose IPv4 or IPv6
- Current settings will display automatically

### 3. Configure Network Settings

#### DHCP (Automatic):
1. Select "Obtain an IP address automatically (DHCP)"
2. Click "Apply Settings"

#### Static IP:
1. Select "Use the following IP address"
2. Enter:
   - IP Address (e.g., 192.168.1.100)
   - Subnet Mask (e.g., 255.255.255.0)
   - Default Gateway (e.g., 192.168.1.1)
   - Preferred DNS (e.g., 8.8.8.8)
   - Alternate DNS (optional, e.g., 8.8.4.4)
3. Click "Apply Settings"
4. Confirm the changes

### 4. Save Network Presets
1. Configure your network settings
2. Enter a preset name (e.g., "Home", "Work", "College")
3. Click "Save Current as Preset"

### 5. Apply Network Presets
1. Select a preset from the list
2. Click "Apply Preset" (or double-click the preset)
3. Confirm to apply the saved configuration

### 6. Delete Presets
1. Select a preset from the list
2. Click "Delete Preset"
3. Confirm deletion

## Configuration Storage

Presets are stored in:
```
%APPDATA%\NetworkSetter\presets.json
```

This file is automatically created and updated as you manage presets.

## Important Notes

⚠️ **Administrator Rights Required**: The application must run with administrator privileges to modify network settings.

⚠️ **Network Interruption**: Changing network settings may temporarily disconnect your network connection.

⚠️ **IPv6 Considerations**: Some routers may not fully support IPv6. Ensure your network infrastructure supports it before configuring.

⚠️ **Backup Settings**: Consider saving your current configuration as a preset before making changes.

## Troubleshooting

### "Access Denied" Error
- Ensure the application is running as Administrator
- Right-click the executable and select "Run as Administrator"

### Changes Not Taking Effect
- Wait 5-10 seconds after applying changes
- Click the "Refresh" button to reload current settings
- Restart the network adapter from Windows settings

### Adapter Not Listed
- Ensure the adapter is enabled
- Check Device Manager for driver issues
- Click "Refresh" to reload adapters

### DNS Not Updating
- Clear DNS cache: `ipconfig /flushdns`
- Restart the DNS Client service

## Technical Details

### Technologies Used
- **Framework**: .NET 8.0 Windows Forms
- **Network Management**: System.Management (WMI) and netsh
- **Data Storage**: JSON (Newtonsoft.Json)
- **Network Info**: System.Net.NetworkInformation

### How It Works
- Uses `netsh` commands to configure network adapters
- Reads current settings via `System.Net.NetworkInformation`
- Stores presets as JSON in the user's AppData folder
- Requires UAC elevation for administrative operations

## License

This project is provided as-is for educational and personal use.

## Support

For issues or questions, please check:
- Windows Event Viewer for system errors
- Application logs (if implemented)
- Ensure .NET 8.0 Runtime is installed
