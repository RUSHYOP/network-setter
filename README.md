# Network Setter

> A Windows desktop application for managing network adapter TCP/IP settings with preset support for IPv4 and IPv6.

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE.txt)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%2F11-blue.svg)](https://www.microsoft.com/windows)

## ✨ Features

- 🌐 **IPv4 & IPv6 Support** - Configure both IP protocol versions
- 🔄 **DHCP or Static IP** - Easy switching between automatic and manual configuration
- 💾 **Network Presets** - Save and quickly apply different network configurations (Home, Work, College, etc.)
- 📊 **Real-time Display** - View current IP address, gateway, subnet mask, and DNS settings
- ⚡ **One-Click Apply** - Switch between network configurations instantly
- 🔐 **Elevated Privileges** - Automatically requests administrator permissions when needed
- 🎨 **Modern UI** - Clean Windows Forms interface

## 📸 Screenshots

<!-- Add your screenshots here -->
_Coming soon - Add screenshots of your application_

## 🚀 Quick Start

### Prerequisites

- Windows 10 or Windows 11
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for building from source)
- Administrator privileges

### Installation Options

#### Option 1: Download Installer (Recommended)

1. Download the latest `NetworkSetter_Setup_v1.0.0.exe` from [Releases](../../releases)
2. Run the installer
3. Follow the installation wizard
4. Launch from Start Menu

#### Option 2: Build from Source

```powershell
# Clone the repository
git clone https://github.com/yourusername/network-setter.git
cd network-setter

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

> **Note:** Must be run in an elevated PowerShell (Administrator)

## 📖 Usage

### 1. Viewing Current Settings

- Select your network adapter from the dropdown
- Choose IPv4 or IPv6
- Current settings display automatically

### 2. Configuring Network Settings

#### DHCP (Automatic):
1. Select "Obtain an IP address automatically (DHCP)"
2. Click "Apply Settings"

#### Static IP:
1. Select "Use the following IP address"
2. Enter your network details:
   - IP Address (e.g., `192.168.1.100`)
   - Subnet Mask (e.g., `255.255.255.0`)
   - Gateway (e.g., `192.168.1.1`)
   - Preferred DNS (e.g., `8.8.8.8`)
   - Alternate DNS (optional)
3. Click "Apply Settings"

### 3. Using Presets

#### Save a Preset:
1. Configure your network settings
2. Enter a preset name (e.g., "Home", "Work", "College")
3. Click "Save Current as Preset"

#### Apply a Preset:
1. Select a preset from the list
2. Click "Apply Preset" or double-click it
3. Confirm the changes

#### Delete a Preset:
1. Select a preset
2. Click "Delete Preset"

## 🛠️ Building & Distribution

### Build Release Version

```powershell
dotnet build -c Release
```

### Create Self-Contained Executable

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

### Create Installer

1. Install [Inno Setup 6](https://jrsoftware.org/isdl.php)
2. Run `create_installer.bat` as Administrator
3. Find installer in `installer_output/NetworkSetter_Setup_v1.0.0.exe`

See [INSTALLER_GUIDE.md](INSTALLER_GUIDE.md) for detailed instructions.

## 📂 Project Structure

```
network-setter/
├── src/                     # Source code
│   ├── MainForm.cs          # Main UI form and logic
│   ├── NetworkManager.cs    # Network configuration operations
│   ├── NetworkConfig.cs     # Configuration data model
│   ├── PresetManager.cs     # Preset storage and management
│   ├── Program.cs           # Application entry point
│   ├── app.manifest         # Admin elevation manifest
│   └── NetworkSetter.csproj # Project file
├── bat scripts/             # Build and utility scripts
│   ├── build.bat            # Build the project
│   ├── run.bat              # Run the application
│   ├── create_installer.bat # Create installer
│   ├── package.bat          # Package distribution
│   └── setup_git.bat        # Initialize Git repository
├── .github/                 # GitHub templates
│   ├── ISSUE_TEMPLATE/      # Issue templates
│   └── pull_request_template.md
├── installer.iss            # Inno Setup installer script
├── README.md                # This file
├── QUICK_START.md           # Quick setup guide
├── INSTALLER_GUIDE.md       # Installer creation guide
├── CONTRIBUTING.md          # Contribution guidelines
├── GITHUB_SETUP.md          # GitHub setup instructions
└── LICENSE.txt              # MIT License
```

## ⚙️ Technical Details

- **Framework:** .NET 8.0 Windows Forms
- **Network Management:** Windows Management Instrumentation (WMI) and `netsh`
- **Data Storage:** JSON (Newtonsoft.Json)
- **Network Info:** System.Net.NetworkInformation API

### How It Works

- Uses `netsh` commands to configure network adapters at the TCP/IP level
- Reads current settings via `System.Net.NetworkInformation`
- Stores presets as JSON in `%APPDATA%\NetworkSetter\presets.json`
- Requires UAC elevation for administrative operations

## 🔒 Security & Permissions

This application requires **Administrator privileges** to function because it modifies system-level network settings. The application:

- ✅ Uses Windows built-in tools (`netsh`) for all operations
- ✅ Requests elevation through standard UAC prompts
- ✅ Does not collect or transmit any user data
- ✅ Stores presets locally on your machine
- ✅ Open source - audit the code yourself!

## ⚠️ Important Notes

- **Administrator Rights Required:** The app must run with elevated privileges
- **Network Interruption:** Changing settings may temporarily disconnect your network
- **IPv6 Support:** Ensure your router/network supports IPv6 before configuring
- **Backup Settings:** Save your current configuration as a preset before making changes

## 🐛 Troubleshooting

### "Access Denied" Error
- Ensure the application is running as Administrator
- Right-click and select "Run as Administrator"

### Changes Not Taking Effect
- Wait 5-10 seconds after applying changes
- Click "Refresh" to reload current settings
- Restart the network adapter if necessary

### Adapter Not Listed
- Ensure the adapter is enabled in Windows settings
- Check Device Manager for driver issues
- Click "Refresh" to reload adapters

See [QUICK_START.md](QUICK_START.md) for more troubleshooting tips.

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Ideas for Contributions

- Add support for more network protocols
- Implement network adapter restart functionality
- Add import/export for presets
- Create a dark mode theme
- Add network diagnostics tools
- Improve error handling and logging

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## 👤 Author

**Your Name**
- GitHub: [@yourusername](https://github.com/RUSHYOP)

## 🙏 Acknowledgments

- Built with [.NET Windows Forms](https://docs.microsoft.com/dotnet/desktop/winforms/)
- Installer created with [Inno Setup](https://jrsoftware.org/isinfo.php)
- JSON serialization by [Newtonsoft.Json](https://www.newtonsoft.com/json)

## 📊 Project Status

🚧 **Active Development** - Version 1.0.0

### Roadmap

- [ ] Add network adapter restart functionality
- [ ] Implement preset import/export
- [ ] Add network diagnostics tools
- [ ] Create system tray icon for quick access
- [ ] Add support for wireless network profiles
- [ ] Implement logging system
- [ ] Add multi-language support

## 💬 Support

If you encounter any issues or have questions:

1. Check the [documentation](README.md)
2. Search [existing issues](../../issues)
3. Create a [new issue](../../issues/new) if needed

---

⭐ **Star this repository if you find it helpful!**
