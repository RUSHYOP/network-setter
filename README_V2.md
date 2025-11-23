# Network Setter V2 - Enhanced Version

## 🎉 What's New in Version 2

Network Setter V2 is a complete redesign with powerful new features for managing network configurations on Windows.

### ✨ New Features

#### 🔄 Background Operation
- **System Tray Integration**: Runs silently in the background with a system tray icon
- **Single Instance**: Only one instance of the app can run at a time
- **Minimize to Tray**: Minimize the app to system tray instead of taskbar
- **Quick Access Popup**: Left-click the tray icon for a compact popup window

#### 🚀 Startup Options
- **Run at Windows Startup**: Automatically start with Windows
- **Start Minimized**: Launch minimized to tray for seamless background operation
- **Notification Support**: Get notified when settings are applied (optional)

#### 🎨 Theme Support
- **Light Theme**: Clean, bright interface for daytime use
- **Dark Theme**: Eye-friendly dark interface for night work
- **System Default**: Automatically matches your Windows theme
- **Dynamic Switching**: Change themes on-the-fly from the menu or settings

#### 📱 Compact Tray Popup
- **Vertical Rectangle Window**: Appears in bottom-right corner when clicking tray icon
- **Quick DHCP Toggle**: Enable DHCP with one click
- **Quick Presets**: Apply saved presets instantly
- **Minimal Interface**: Access essential features without opening the main window

### 📋 Features from V1 (Still Included)

- ✅ Manage network adapters (IPv4 and IPv6)
- ✅ Switch between DHCP and Static IP
- ✅ Configure IP address, subnet mask, gateway
- ✅ Set DNS servers (primary and alternate)
- ✅ Save and load network configuration presets
- ✅ View current network settings
- ✅ Support for multiple network adapters

## 🖥️ User Interface

### Main Window
The full-featured interface with:
- Menu bar (File, View, Tools, Help)
- Adapter selection and IP version
- Current settings display
- Configuration panel (DHCP/Static)
- Network presets management

### Tray Popup Window
Compact vertical window (280x400) with:
- Quick adapter selection
- One-click DHCP enable
- Quick access to saved presets (top 4)
- Open full interface button
- Settings and Exit buttons

### System Tray Menu
Right-click the tray icon for:
- Open Network Setter (main window)
- Quick Access (popup window)
- Settings
- Exit

## ⚙️ Settings

Access settings via:
- **Main Window**: Tools → Settings (Ctrl+S)
- **Tray Popup**: Click the ⚙️ Settings button
- **Tray Menu**: Right-click icon → Settings

### Available Settings

#### General Settings
- ☑️ Run at Windows startup
- ☑️ Minimize to system tray (instead of taskbar)
- ☑️ Start minimized to tray
- ☑️ Show notifications when settings are applied

#### Theme Settings
- ☀️ Light theme
- 🌙 Dark theme
- 💻 System Default (follows Windows theme)

## 🎯 Usage Scenarios

### Background Worker Setup
1. Enable "Run at Windows startup" in Settings
2. Enable "Start minimized to tray"
3. Enable "Minimize to tray"
4. The app will now run silently in the background
5. Access it anytime via the tray icon

### Quick Network Switching
1. Save your common network configurations as presets
2. Click the tray icon to open the popup
3. Click on a preset to apply it instantly
4. Or use Quick DHCP for immediate DHCP

### Full Configuration
1. Double-click the tray icon or use the tray menu
2. Configure detailed network settings
3. Save new presets
4. Manage existing presets

## 🔐 Administrator Rights

Network Setter V2 requires administrator privileges to modify network settings. The app will request elevation when needed.

## 💾 Data Storage

Settings and presets are stored in:
```
%APPDATA%\NetworkSetter\
  ├── settings.json   (App settings: startup, theme, etc.)
  └── presets.json    (Network configuration presets)
```

## 🎨 Theme Details

### Light Theme
- White background
- Black text
- Standard button styling
- Clean and professional appearance

### Dark Theme
- Dark gray background (#202020)
- White text
- Flat button styling with borders
- Reduced eye strain in low-light conditions

### System Default
- Automatically detects Windows theme preference
- Switches between Light and Dark based on system settings
- Updates when Windows theme changes

## ⌨️ Keyboard Shortcuts

- **Ctrl+M**: Minimize to Tray
- **Ctrl+S**: Open Settings
- **Alt+F4**: Exit Application

## 🔄 Upgrading from V1

Version 2 maintains compatibility with V1 presets. Your saved network configurations will automatically work with the new version.

New features include:
- Background operation
- System tray integration
- Theme support
- Quick access popup
- Enhanced settings management

## 🛠️ Building from Source

### Requirements
- .NET 8.0 SDK
- Windows OS
- Visual Studio 2022 or VS Code with C# extension

### Build Commands
```powershell
# Build the project
dotnet build src/NetworkSetter.csproj --configuration Release

# Run the application
dotnet run --project src/NetworkSetter.csproj

# Create installer (requires Inno Setup)
.\create_installer.bat
```

## 📝 Command Line Arguments

- `--minimized`: Start the application minimized to tray

Example:
```powershell
NetworkSetter.exe --minimized
```

## 🐛 Troubleshooting

### App won't start at Windows startup
- Check Settings → "Run at Windows startup" is enabled
- Verify registry entry: `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run\NetworkSetter`

### Tray icon not visible
- Check Windows system tray settings
- Ensure "Network Setter" is allowed to show notifications and icon

### Theme not applying
- Try restarting the application
- Check Settings → Theme selection
- Ensure you're using .NET 8.0 runtime

### Settings not saving
- Check write permissions for `%APPDATA%\NetworkSetter\`
- Run as administrator if needed

## 🔒 Security

- App requires administrator privileges for network changes
- Settings and presets stored locally in user's AppData folder
- No telemetry or external connections
- Single instance ensures no conflicts

## 📜 License

See LICENSE.txt for licensing information.

## 🤝 Contributing

See CONTRIBUTING.md for contribution guidelines.

## 📧 Support

For issues, questions, or feature requests, please open an issue on the GitHub repository.

---

**Network Setter V2** - Professional Network Configuration Management
