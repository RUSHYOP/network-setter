# Creating an Installer for Network Setter

This guide explains how to create a professional Windows installer for Network Setter.

## Prerequisites

### 1. Install Inno Setup (Free)

**Download Inno Setup 6:**
- Go to: https://jrsoftware.org/isdl.php
- Download **Inno Setup 6.x** (latest version)
- Run the installer
- Use default installation settings

**Why Inno Setup?**
- ✅ Completely free and open source
- ✅ Industry standard for Windows installers
- ✅ Creates professional MSI-style installers
- ✅ Easy to use and customize
- ✅ Used by major applications (Notepad++, FileZilla, etc.)

### 2. Verify .NET SDK is Installed

```powershell
dotnet --version
```

Should show version 8.0.x or higher.

## Creating the Installer

### Method 1: Using the Batch Script (Easiest)

1. **Right-click** `create_installer.bat`
2. Select **"Run as Administrator"**
3. Wait for the build and installer creation to complete
4. Find your installer in: `installer_output\NetworkSetter_Setup_v1.0.0.exe`

### Method 2: Manual Steps

1. **Build the self-contained application:**
   ```powershell
   dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
   ```

2. **Open Inno Setup Compiler**
   - Start Menu → Inno Setup → Inno Setup Compiler

3. **Compile the installer script:**
   - File → Open → Select `installer.iss`
   - Build → Compile (or press Ctrl+F9)

4. **Get your installer:**
   - Location: `installer_output\NetworkSetter_Setup_v1.0.0.exe`

## Installer Features

The created installer will:

### ✅ Installation Features
- Install to `C:\Program Files\Network Setter` (or user-chosen location)
- Create Start Menu shortcuts
- Optional Desktop shortcut
- Include documentation (README, Quick Start)
- Check for Administrator privileges
- Support uninstallation via Windows Settings

### ✅ User Experience
- Modern wizard-style interface
- License agreement display
- Custom installation directory selection
- Option to launch app after installation
- Automatic uninstaller registration

### ✅ Technical Details
- Compressed installer (LZMA compression)
- ~80-100 MB installer size (includes .NET runtime)
- No .NET installation required by users
- 64-bit optimized
- Digitally signable (if you have a code signing certificate)

## Customizing the Installer

Edit `installer.iss` to customize:

### Change Version Number
```ini
#define MyAppVersion "1.0.0"
```

### Change Publisher Name
```ini
#define MyAppPublisher "Your Name"
```

### Change Website URL
```ini
#define MyAppURL "https://github.com/yourusername/networksetter"
```

### Add Custom Icon
1. Save your icon as `icon.ico` in the project folder
2. The installer will automatically use it

### Change Install Location
```ini
DefaultDirName={autopf}\YourFolderName
```

## Distributing the Installer

Once created, you can share `NetworkSetter_Setup_v1.0.0.exe` with:

### ✅ Anyone can install it by:
1. Double-clicking the installer
2. Following the wizard
3. Accepting the license
4. Choosing install location
5. Clicking Install

### ✅ No additional requirements needed
- ❌ No .NET installation needed
- ❌ No manual configuration
- ❌ No technical knowledge required
- ✅ Just run the installer!

## Troubleshooting

### "Inno Setup not found"
- Install Inno Setup from: https://jrsoftware.org/isdl.php
- Make sure it's installed to the default location: `C:\Program Files (x86)\Inno Setup 6\`

### "Build failed"
- Make sure .NET 8.0 SDK is installed
- Run the build command in an Administrator PowerShell

### Installer doesn't include all files
- Check that the publish command completed successfully
- Verify files exist in: `bin\Release\net8.0-windows\win-x64\publish\`

### Want to sign the installer?
1. Get a code signing certificate
2. Add to `installer.iss`:
   ```ini
   SignTool=signtool sign /f "path\to\certificate.pfx" /p "password" $f
   ```

## Advanced Options

### Create Portable Version
Set in `installer.iss`:
```ini
PrivilegesRequired=lowest
DefaultDirName={autopf}\Network Setter
```

### Multiple Languages
Add to `[Languages]` section:
```ini
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
```

### Custom Installation Types
Add to `[Types]` section:
```ini
Name: "full"; Description: "Full installation"
Name: "compact"; Description: "Compact installation"
Name: "custom"; Description: "Custom installation"; Flags: iscustom
```

## File Size Comparison

| Distribution Method | Size | Requirements |
|-------------------|------|--------------|
| Regular build | ~5-10 MB | .NET 8.0 Runtime |
| Self-contained | ~80-100 MB | None |
| Installer | ~80-100 MB | None |

## Recommended Distribution

For best user experience, distribute the **installer**:
- ✅ Professional appearance
- ✅ Easy installation
- ✅ Automatic Start Menu integration
- ✅ Proper uninstallation support
- ✅ No technical knowledge required

## Support

For Inno Setup documentation:
- Official Docs: https://jrsoftware.org/ishelp/
- Examples: `C:\Program Files (x86)\Inno Setup 6\Examples\`
- Community: https://stackoverflow.com/questions/tagged/inno-setup
