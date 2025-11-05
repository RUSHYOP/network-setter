# GitHub Setup Guide

This guide will help you push your Network Setter project to GitHub.

## Prerequisites

### Install Git (if not already installed)

1. Download Git from: https://git-scm.com/download/win
2. Run the installer with default settings
3. Verify installation:
   ```bash
   git --version
   ```

## Quick Setup (Automated)

### Step 1: Initialize Git Repository

Run the setup script:
```bash
setup_git.bat
```

This will:
- Initialize a Git repository
- Rename README files appropriately
- Add all files to Git
- Create the initial commit
- Set up the main branch

### Step 2: Create GitHub Repository

1. Go to https://github.com/new
2. Fill in the details:
   - **Repository name:** `network-setter`
   - **Description:** "Windows application for managing network adapter TCP/IP settings with preset support"
   - **Visibility:** Choose Public or Private
   - **DO NOT** initialize with README, .gitignore, or license (we already have these)
3. Click **"Create repository"**

### Step 3: Connect to GitHub

Replace `yourusername` with your actual GitHub username:

```bash
git remote add origin https://github.com/yourusername/network-setter.git
git push -u origin main
```

### Step 4: Update Repository Information

Update these files with your information:

**README.md** (line 164):
```markdown
**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
```

**installer.iss** (line 8):
```ini
#define MyAppURL "https://github.com/yourusername/network-setter"
```

Commit and push the changes:
```bash
git add .
git commit -m "docs: Update repository links"
git push
```

## Manual Setup (Alternative)

If you prefer manual setup:

### 1. Initialize Git

```bash
git init
git branch -M main
```

### 2. Add Files

```bash
git add .
```

### 3. Create Initial Commit

```bash
git commit -m "Initial commit: Network Setter v1.0.0"
```

### 4. Add Remote

```bash
git remote add origin https://github.com/yourusername/network-setter.git
```

### 5. Push to GitHub

```bash
git push -u origin main
```

## Customizing for GitHub

### Repository Settings

After pushing, configure these on GitHub:

1. **About Section** (right side of main page)
   - Add description
   - Add website URL
   - Add topics: `windows`, `networking`, `ip-configuration`, `csharp`, `winforms`, `network-management`

2. **Repository Settings**
   - Enable Issues
   - Enable Discussions (optional)
   - Set default branch to `main`

3. **Add Releases**
   - Go to "Releases" → "Create a new release"
   - Tag: `v1.0.0`
   - Title: `Network Setter v1.0.0`
   - Upload the installer: `NetworkSetter_Setup_v1.0.0.exe`
   - Add release notes

### Creating Your First Release

1. Build the installer:
   ```bash
   create_installer.bat
   ```

2. Go to your GitHub repository

3. Click "Releases" → "Create a new release"

4. Fill in:
   - **Tag:** `v1.0.0`
   - **Title:** `Network Setter v1.0.0 - Initial Release`
   - **Description:**
     ```markdown
     ## 🎉 Initial Release
     
     First stable release of Network Setter!
     
     ### ✨ Features
     - IPv4 and IPv6 configuration support
     - DHCP and static IP management
     - Network preset system for quick switching
     - Real-time network settings display
     - Modern Windows Forms interface
     
     ### 📥 Installation
     Download `NetworkSetter_Setup_v1.0.0.exe` and run the installer.
     
     ### ⚠️ Requirements
     - Windows 10 or Windows 11
     - Administrator privileges
     
     ### 📖 Documentation
     See [README.md](../README.md) for complete documentation.
     ```

5. Upload the installer file from `installer_output/NetworkSetter_Setup_v1.0.0.exe`

6. Click "Publish release"

## Adding Screenshots

### 1. Take Screenshots

Run your application and take screenshots of:
- Main window with current settings
- Static IP configuration
- Preset management
- Settings dialog

### 2. Create Screenshots Directory

```bash
mkdir screenshots
```

### 3. Add Screenshots to README

Edit `README.md` and update the Screenshots section:

```markdown
## 📸 Screenshots

### Main Interface
![Main Window](screenshots/main-window.png)

### Network Configuration
![Static IP Setup](screenshots/static-ip.png)

### Preset Management
![Presets](screenshots/presets.png)
```

### 4. Commit Screenshots

```bash
git add screenshots/
git commit -m "docs: Add application screenshots"
git push
```

## Setting Up GitHub Pages (Optional)

Create a documentation website:

1. Create `docs` directory:
   ```bash
   mkdir docs
   ```

2. Add `docs/index.md`:
   ```markdown
   # Network Setter Documentation
   
   Welcome to Network Setter documentation!
   
   [View on GitHub](https://github.com/yourusername/network-setter)
   ```

3. Enable GitHub Pages:
   - Repository Settings → Pages
   - Source: Deploy from a branch
   - Branch: main
   - Folder: /docs
   - Save

## Maintaining Your Repository

### Regular Updates

```bash
# Make changes to your code
git add .
git commit -m "feat: Add new feature"
git push
```

### Semantic Versioning

Use semantic versioning for releases:
- **Major** (1.x.x): Breaking changes
- **Minor** (x.1.x): New features
- **Patch** (x.x.1): Bug fixes

Example:
```bash
# Update version in installer.iss
# Update version in README.md
git add .
git commit -m "chore: Bump version to 1.1.0"
git tag v1.1.0
git push
git push --tags
```

### Branch Strategy

**Main Branch:**
- Always stable and deployable
- Protected from direct commits

**Feature Branches:**
```bash
git checkout -b feature/new-feature
# Make changes
git commit -m "feat: Add new feature"
git push -u origin feature/new-feature
# Create Pull Request on GitHub
```

**Bug Fix Branches:**
```bash
git checkout -b fix/bug-description
# Make changes
git commit -m "fix: Resolve bug description"
git push -u origin fix/bug-description
# Create Pull Request on GitHub
```

## GitHub Actions (Optional)

### Automated Build

Create `.github/workflows/build.yml`:

```yaml
name: Build

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: windows-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --no-restore -c Release
      
    - name: Publish
      run: dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## Common Git Commands

### Check Status
```bash
git status
```

### View Changes
```bash
git diff
```

### View Commit History
```bash
git log --oneline
```

### Undo Changes
```bash
# Discard changes in working directory
git checkout -- filename

# Undo last commit (keep changes)
git reset --soft HEAD~1

# Undo last commit (discard changes)
git reset --hard HEAD~1
```

### Update from Remote
```bash
git pull
```

### Create and Switch Branch
```bash
git checkout -b branch-name
```

### Merge Branch
```bash
git checkout main
git merge branch-name
```

## Troubleshooting

### "Repository not found"
- Check the remote URL: `git remote -v`
- Update if needed: `git remote set-url origin https://github.com/yourusername/network-setter.git`

### Authentication Issues
- Use a Personal Access Token (PAT) instead of password
- Generate at: https://github.com/settings/tokens

### Large File Issues
- Files over 100MB need Git LFS
- Add to .gitignore instead if they're build outputs

### Merge Conflicts
```bash
# View conflicted files
git status

# Edit files to resolve conflicts
# Look for <<<<<<< markers

# Mark as resolved
git add filename

# Complete merge
git commit
```

## Next Steps

1. ✅ Initialize Git repository
2. ✅ Push to GitHub
3. 📸 Add screenshots
4. 🎉 Create first release
5. 📝 Write detailed README
6. 🐛 Enable issue tracking
7. 🤝 Add contributing guidelines
8. ⭐ Share your project!

## Resources

- **Git Documentation:** https://git-scm.com/doc
- **GitHub Guides:** https://guides.github.com/
- **GitHub Flow:** https://guides.github.com/introduction/flow/
- **Semantic Versioning:** https://semver.org/

---

Need help? Check the [GitHub Documentation](https://docs.github.com/) or create an issue!
