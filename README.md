# Network Setter V2

A powerful Windows desktop application for managing network adapter configurations with system tray integration, themes, and quick access features.

![Version](https://img.shields.io/badge/version-2.0.0-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-lightgrey.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)

## 🎉 What's New in V2

- 🔄 **System Tray Integration** - Runs in background with quick access
- 🎨 **Theme Support** - Light, Dark, and System themes
- 📱 **Quick Access Popup** - Compact interface from system tray
- 🚀 **Run at Startup** - Automatic startup with Windows
- ⚡ **Enhanced Performance** - Faster network switching
- 💾 **Settings Management** - Persistent configuration

[See full comparison: V1 vs V2](V1_VS_V2.md)

## ✨ Features

### Core Functionality
- Configure network adapters (IPv4 and IPv6)
- Switch between DHCP and Static IP with one click
- Set IP address, subnet mask, gateway, and DNS servers
- Save and load network configuration presets
- View current network settings in real-time
- Support for multiple network adapters

### V2 Enhancements
- **System Tray** - Always accessible from notification area
- **Quick Popup** - Instant access to common tasks (280x400 vertical window)
- **Themes** - Light, Dark, or System-matched appearance
- **Background Mode** - Minimize to tray instead of taskbar
- **Startup Integration** - Launch automatically with Windows
- **Smart Installer** - Upgrades from V1 seamlessly

## 📋 Requirements

- **OS**: Windows 10/11 (64-bit)
- **Runtime**: .NET 8.0 (included in installer)
- **Privileges**: Administrator rights for network changes
- **Memory**: 512 MB RAM minimum
- **Disk**: 50 MB free space

## 🚀 Quick Start

### Option 1: Install from Release (Recommended)
1. Download `NetworkSetterV2_Setup.exe` from [installer_output](installer_output/NetworkSetterV2_Setup.exe)
2. Run the installer as Administrator
3. Choose installation options (desktop icon, run at startup)
4. Launch and enjoy!

### Option 2: Build from Source
```powershell
# Clone the repository
git clone https://github.com/RUSHYOP/network-setter.git
cd network-setter

# Build the project
dotnet build src/NetworkSetter.csproj --configuration Release

# Run the application
.\src\bin\Release\net8.0-windows\NetworkSetter.exe
```

## 📖 Documentation

- **[Quick Start Guide](QUICK_START_V2.md)** - Get started in 3 steps
- **[User Manual](README_V2.md)** - Complete feature documentation
- **[V1 vs V2](V1_VS_V2.md)** - Feature comparison
- **[Contributing](CONTRIBUTING.md)** - Development guidelines

## 🎯 Usage

### Main Window
1. Launch as Administrator (right-click → Run as administrator)
2. Select network adapter from dropdown
3. Choose DHCP or Static IP configuration
4. Apply settings and save as preset for future use

### System Tray Quick Access
1. **Left-click** tray icon → Quick popup window
2. **Right-click** tray icon → Full menu
3. **Double-click** tray icon → Open main window

### Quick Actions from Tray Popup
- Select adapter and enable DHCP with 2 clicks
- Apply saved presets instantly
- Open settings or full interface

## 🛠️ Building

### Development Build
```powershell
dotnet build src/NetworkSetter.csproj
```

### Release Build
```powershell
dotnet build src/NetworkSetter.csproj --configuration Release
```

### Create Installer
```powershell
# Requires Inno Setup installed
# Compile with: "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer.iss
# Output: installer_output\NetworkSetterV2_Setup.exe
```

## 📂 Project Structure

```
network-setter/
├── src/                          # Source code
│   ├── Program.cs               # Entry point & tray management
│   ├── MainForm.cs              # Main window UI
│   ├── TrayPopupForm.cs         # Quick access popup
│   ├── SettingsForm.cs          # Settings dialog
│   ├── ThemeManager.cs          # Theme system
│   ├── SettingsManager.cs       # Settings persistence
│   ├── NetworkManager.cs        # Network configuration
│   ├── NetworkConfig.cs         # Data model
│   ├── PresetManager.cs         # Preset management
│   ├── IconGenerator.cs         # Dynamic icon creation
│   └── NetworkSetter.csproj     # Project file
├── installer_output/             # Compiled installer
│   └── NetworkSetterV2_Setup.exe
├── installer.iss                 # Inno Setup script
├── LICENSE.txt
├── README.md                     # This file
├── README_V2.md                  # User manual
├── QUICK_START_V2.md            # Quick start guide
└── V1_VS_V2.md                  # Version comparison
```

## 🎨 Themes

Network Setter V2 supports three theme modes:

- **☀️ Light** - Clean, bright interface
- **🌙 Dark** - Professional dark mode
- **💻 System** - Matches Windows theme automatically

Change themes via: `View → Theme` or `Tools → Settings`

## 🐛 Troubleshooting

### Application Won't Start
- Ensure .NET 8.0 Runtime is installed
- Check Windows Event Viewer for errors
- Run from command line to see error messages

### "Access Denied" Errors
- Run as Administrator (required for network changes)
- Check User Account Control (UAC) settings
- Verify you have network configuration permissions

### Settings Not Applying
- Confirm Administrator privileges
- Check network adapter is enabled
- Verify adapter name is correct
- Wait 2-3 seconds for changes to take effect

### Tray Icon Not Appearing
- Check Windows notification area settings
- Restart the application
- Enable "Show all icons" in taskbar settings

For more help, see [README_V2.md](README_V2.md) troubleshooting section.

## 🤝 Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for:
- Code style guidelines
- Development setup
- Pull request process
- Issue reporting

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## 👤 Author

**Your Name**
- GitHub: [@yourusername](https://github.com/RUSHYOP)

## 🙏 Acknowledgments

- Built with .NET 8.0 and Windows Forms
- Configuration management: Newtonsoft.Json
- Network configuration: Windows Management Instrumentation (WMI)
- System integration: Windows Registry API

## 📧 Support

- **Issues**: [GitHub Issues](../../issues)
- **Discussions**: [GitHub Discussions](../../discussions)
- **Documentation**: See docs in this repository

---

**Network Setter V2** - Professional Network Configuration Made Easy 🚀
