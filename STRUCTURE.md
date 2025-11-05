# Project Structure

This document explains the organization of the Network Setter project.

## 📁 Folder Organization

```
network-setter/
│
├── 📂 src/                          # Source Code
│   ├── MainForm.cs                  # Main application window and UI logic
│   ├── NetworkManager.cs            # Network configuration operations (netsh, WMI)
│   ├── NetworkConfig.cs             # Data model for network configurations
│   ├── PresetManager.cs             # Preset save/load/delete operations
│   ├── Program.cs                   # Application entry point
│   ├── app.manifest                 # UAC elevation manifest
│   ├── NetworkSetter.csproj         # .NET project file
│   ├── bin/ (gitignored)            # Build outputs
│   └── obj/ (gitignored)            # Intermediate build files
│
├── 📂 bat scripts/                  # Utility Scripts
│   ├── build.bat                    # Build the application
│   ├── run.bat                      # Run the application
│   ├── create_installer.bat         # Create Windows installer
│   ├── package.bat                  # Create distribution package
│   └── setup_git.bat                # Initialize Git repository
│
├── 📂 .github/                      # GitHub Configuration
│   ├── ISSUE_TEMPLATE/              # Issue templates
│   │   ├── bug_report.md            # Bug report template
│   │   └── feature_request.md       # Feature request template
│   └── pull_request_template.md     # Pull request template
│
├── 📂 installer_output/ (gitignored) # Installer build output
│   └── NetworkSetter_Setup_v1.0.0.exe
│
├── 📂 dist/ (gitignored)            # Distribution package output
│
├── 📄 build.bat                     # Wrapper: calls bat scripts/build.bat
├── 📄 run.bat                       # Wrapper: calls bat scripts/run.bat
├── 📄 create_installer.bat          # Wrapper: calls bat scripts/create_installer.bat
│
├── 📄 installer.iss                 # Inno Setup installer script
│
├── 📄 README.md                     # Main documentation (GitHub README)
├── 📄 README_USER.md                # Original user documentation
├── 📄 QUICK_START.md                # Quick start guide for users
├── 📄 INSTALLER_GUIDE.md            # Guide for creating installers
├── 📄 CONTRIBUTING.md               # Contribution guidelines
├── 📄 GITHUB_SETUP.md               # GitHub setup instructions
├── 📄 PROJECT_CHECKLIST.md          # Pre-release checklist
├── 📄 STRUCTURE.md                  # This file
│
├── 📄 LICENSE.txt                   # MIT License
├── 📄 .gitignore                    # Git ignore rules
└── 📄 .gitattributes                # Git line ending configuration
```

## 🎯 Why This Structure?

### Source Code in `src/`
- **Clean separation** of source code from documentation and scripts
- **Standard convention** followed by many .NET projects
- **Easy to navigate** - all code is in one place
- **Build outputs** stay contained in `src/bin/` and `src/obj/`

### Scripts in `bat scripts/`
- **Organized utilities** - all scripts in one location
- **Prevents root clutter** - keeps project root clean
- **Easy to find** - developers know where to look for scripts
- **Wrapper scripts in root** - users can still run `build.bat` from root

### Documentation in Root
- **Immediate visibility** - README.md shows first on GitHub
- **Easy discovery** - users find guides without navigation
- **GitHub convention** - standard practice for open source projects

### Hidden Folders
- **`.git/`** - Git repository metadata (managed by Git)
- **`.github/`** - GitHub-specific templates and workflows
- **`src/bin/`, `src/obj/`** - Build outputs (gitignored)
- **`installer_output/`** - Installer files (gitignored)
- **`dist/`** - Distribution packages (gitignored)

## 🚀 Working with This Structure

### Building the Project

From anywhere in the project:
```bash
# From root directory
build.bat

# Or directly from bat scripts
cd "bat scripts"
build.bat
```

### Running the Application

```bash
# From root directory
run.bat

# Or from src directory
cd src
dotnet run
```

### Creating Installer

```bash
# From root directory
create_installer.bat
```

### Navigating Source Code

All source files are in `src/`:
```bash
cd src
# View source files
dir *.cs
```

## 📝 File Purposes

### Source Files (`src/`)

| File | Purpose |
|------|---------|
| `MainForm.cs` | Main window, UI controls, event handlers |
| `NetworkManager.cs` | Network operations (read/write configs) |
| `NetworkConfig.cs` | Data model for network settings |
| `PresetManager.cs` | JSON persistence for presets |
| `Program.cs` | Application startup |
| `app.manifest` | Requests admin elevation |
| `NetworkSetter.csproj` | Project configuration |

### Scripts (`bat scripts/`)

| File | Purpose |
|------|---------|
| `build.bat` | Builds release version |
| `run.bat` | Runs the application |
| `create_installer.bat` | Creates Windows installer |
| `package.bat` | Creates portable distribution |
| `setup_git.bat` | Initializes Git repository |

### Documentation

| File | Audience |
|------|----------|
| `README.md` | All users (GitHub main page) |
| `QUICK_START.md` | New users (installation guide) |
| `INSTALLER_GUIDE.md` | Distributors (installer creation) |
| `CONTRIBUTING.md` | Contributors (development guide) |
| `GITHUB_SETUP.md` | Maintainers (repository setup) |
| `PROJECT_CHECKLIST.md` | Maintainers (release checklist) |

## 🔧 Modifying the Structure

If you need to add files:

### Adding Source Files
```bash
# Place in src/
src/NewFeature.cs
```

### Adding Scripts
```bash
# Place in bat scripts/
bat scripts/new_script.bat

# Create wrapper in root if needed
call "bat scripts\new_script.bat"
```

### Adding Documentation
```bash
# Place in root
NEW_GUIDE.md
```

### Adding Assets (Future)
```bash
# Create assets folder
assets/
├── icons/
├── images/
└── screenshots/
```

## 📊 Benefits of This Organization

✅ **Clean root directory** - Not cluttered with source files  
✅ **Easy navigation** - Logical grouping of related files  
✅ **Scales well** - Easy to add more folders as project grows  
✅ **Standard practice** - Follows .NET and GitHub conventions  
✅ **Clear ownership** - Each folder has a specific purpose  
✅ **Build isolation** - Build outputs don't pollute source  
✅ **Git friendly** - Easy to write .gitignore rules  

## 🎓 Learning the Structure

New contributors should:

1. **Start with README.md** - Overview of the project
2. **Read STRUCTURE.md** - Understand organization (this file)
3. **Check CONTRIBUTING.md** - Learn contribution process
4. **Explore `src/`** - Understand the code
5. **Try the scripts** - Build and run the project

## 🔍 Finding Things Quickly

| Looking for... | Check here... |
|----------------|---------------|
| Source code | `src/` folder |
| How to build | `bat scripts/build.bat` or README.md |
| How to contribute | `CONTRIBUTING.md` |
| Installation guide | `QUICK_START.md` |
| Issue templates | `.github/ISSUE_TEMPLATE/` |
| License | `LICENSE.txt` |
| Project setup | `GITHUB_SETUP.md` |

---

**Questions about the structure?** Open an issue or check `CONTRIBUTING.md`!
