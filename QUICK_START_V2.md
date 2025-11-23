# Network Setter V2 - Quick Start Guide

## 🚀 Getting Started in 3 Steps

### 1. First Launch
- Run `NetworkSetter.exe` as Administrator
- The main window will open with all features visible
- A tray icon appears in your system tray (bottom-right corner)

### 2. Configure Your Preferences
Click **Tools → Settings** (or press Ctrl+S) to configure:

**Recommended Settings for Background Use:**
```
☑️ Run at Windows startup
☑️ Minimize to system tray
☑️ Start minimized to tray
☑️ Show notifications
Theme: System Default (or choose your preference)
```

Click **Save** to apply settings.

### 3. Save Your Network Presets
1. Select your network adapter
2. Configure your network settings (DHCP or Static)
3. Enter a name in "Preset Name" field
4. Click **Save Current as Preset**

Repeat for each network configuration you use regularly (home, work, etc.)

## 📱 Using the Tray Popup (Quick Access)

### Opening the Popup
- **Left-click** the tray icon in bottom-right corner
- A vertical window appears near the tray

### Popup Features
- **Adapter Dropdown**: Select which network adapter to modify
- **🔄 Enable DHCP**: One-click DHCP activation
- **Quick Presets**: Your first 4 saved presets for instant access
- **📋 Open Full Interface**: Open main window for detailed settings
- **⚙️ Settings**: Configure app preferences
- **❌ Exit**: Close the application

### Quick DHCP Switch
1. Open tray popup
2. Select your adapter
3. Click **🔄 Enable DHCP**
4. Done! Status shows "✓ DHCP enabled successfully"

### Apply Preset from Popup
1. Open tray popup
2. Click on any preset in the list
3. Settings apply immediately
4. Status confirms application

## 🖥️ Using the Main Window

### Accessing Main Window
- **Double-click** tray icon, OR
- **Right-click** tray icon → "Open Network Setter", OR
- Click **📋 Open Full Interface** in popup

### Main Window Sections

#### 1. Adapter Selection (Top)
- Choose network adapter from dropdown
- Select IP version (IPv4/IPv6)
- Click **Refresh** to reload adapter list

#### 2. Current Settings
Displays your current network configuration:
- IP Address
- Subnet Mask
- Gateway
- DNS Servers

#### 3. Configure Settings
Choose between:
- **DHCP**: Automatic configuration
- **Static**: Manual IP configuration
  - Enter IP Address
  - Enter Subnet Mask
  - Enter Gateway (optional)
  - Enter DNS servers (optional)

Click **Apply Settings** to save changes.

#### 4. Network Presets
- **Saved Presets List**: Double-click to load
- **Preset Name**: Enter name for new preset
- **Save Current as Preset**: Save current configuration
- **Apply Preset**: Load selected preset
- **Delete Preset**: Remove selected preset

## 🎨 Changing Themes

### Method 1: Menu Bar
1. Open main window
2. Click **View → Theme**
3. Choose: Light, Dark, or System Default

### Method 2: Settings
1. Open **Tools → Settings**
2. Select theme option in Theme section
3. Click **Save**

### Theme Options
- **☀️ Light**: Bright theme for well-lit environments
- **🌙 Dark**: Dark theme for night use or low-light
- **💻 System Default**: Matches Windows theme automatically

## 🔄 Background Operation

### Setting Up Background Mode
1. Open Settings (Ctrl+S)
2. Enable these options:
   - ☑️ Run at Windows startup
   - ☑️ Minimize to system tray
   - ☑️ Start minimized to tray
3. Click Save
4. Restart the app

### How It Works
- App starts automatically with Windows
- Runs minimized in system tray
- Access via tray icon anytime
- No taskbar clutter
- Minimal resource usage

### Minimizing to Tray
When "Minimize to tray" is enabled:
- Clicking **X** (close) minimizes to tray instead of closing
- Use tray menu → **Exit** to actually close the app
- Or File → Exit from main window

## 📋 System Tray Menu

**Right-click** the tray icon to see:

```
┌─────────────────────────────┐
│ Open Network Setter        │  ← Opens main window
├─────────────────────────────┤
│ Quick Access               │  ← Opens popup window
│ Settings                   │  ← Opens settings dialog
├─────────────────────────────┤
│ Exit                       │  ← Closes application
└─────────────────────────────┘
```

**Left-click** tray icon: Opens quick access popup

**Double-click** tray icon: Opens main window

## ⚡ Common Tasks

### Switch to DHCP Quickly
**Quick Method (5 seconds):**
1. Left-click tray icon
2. Select adapter
3. Click "🔄 Enable DHCP"

**Full Method:**
1. Open main window
2. Select adapter
3. Choose "Obtain IP automatically (DHCP)"
4. Click "Apply Settings"

### Apply Saved Network Configuration
**Quick Method (3 seconds):**
1. Left-click tray icon
2. Click your preset name

**Full Method:**
1. Open main window
2. Select preset from list
3. Click "Apply Preset"

### Create New Network Preset
1. Open main window
2. Configure your network settings
3. Enter a name in "Preset Name"
4. Click "Save Current as Preset"

### Switch Between Home and Work Networks
1. Save both configurations as presets:
   - "Home Network"
   - "Work Network"
2. Use tray popup to switch with one click
3. Or use main window presets section

## 🔐 Administrator Requirements

Network Setter requires administrator privileges to change network settings.

**If UAC prompt appears:**
- Click **Yes** to allow changes
- Without admin rights, settings won't apply

**Running as Admin:**
- Right-click NetworkSetter.exe
- Select "Run as administrator"

## 💡 Tips & Tricks

### Tip 1: Quick Workspace Switching
Save presets for each location:
- "Home WiFi"
- "Office Wired"
- "Coffee Shop"
- "VPN Config"

Switch between them instantly from the tray popup.

### Tip 2: Notification Management
If notifications are distracting:
- Open Settings
- Uncheck "Show notifications"
- Status still visible in popup window

### Tip 3: Keyboard Shortcuts
- **Ctrl+M**: Minimize to tray
- **Ctrl+S**: Open settings
- **Alt+F4**: Exit app

### Tip 4: Theme Scheduling
- Set theme to "System Default"
- Windows will switch between light/dark automatically
- App follows your system preference

### Tip 5: Startup Behavior
For seamless background operation:
```
☑️ Run at Windows startup
☑️ Minimize to tray
☑️ Start minimized
```
App will be ready but invisible until needed.

## ❓ FAQ

**Q: Where are my settings stored?**
A: `%APPDATA%\NetworkSetter\settings.json`

**Q: Where are my presets saved?**
A: `%APPDATA%\NetworkSetter\presets.json`

**Q: Can I run V1 and V2 simultaneously?**
A: Yes, but only one instance of V2 can run at a time.

**Q: Will V1 presets work in V2?**
A: Yes, V2 is fully compatible with V1 preset files.

**Q: How do I completely exit the app?**
A: Right-click tray icon → Exit, or File → Exit from main window.

**Q: Can I use it without admin rights?**
A: You can open the app, but changing network settings requires admin rights.

**Q: Does it use internet connection?**
A: No, it's completely offline. No telemetry or external connections.

**Q: How much memory does it use?**
A: Minimal. Typically 20-40 MB when minimized to tray.

## 🆘 Need More Help?

- Check **README_V2.md** for detailed documentation
- See **TROUBLESHOOTING.md** for common issues
- Open an issue on GitHub for support

---

**Welcome to Network Setter V2!** 🎉

Start by saving your most-used network configurations as presets, then enjoy one-click network switching from the system tray.
