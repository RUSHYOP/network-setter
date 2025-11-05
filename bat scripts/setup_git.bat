@echo off
echo ========================================
echo Git Repository Setup
echo ========================================
echo.

REM Check if git is installed
git --version >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Git is not installed!
    echo.
    echo Please install Git from: https://git-scm.com/download/win
    echo.
    pause
    exit /b 1
)

cd ..

echo [1/5] Initializing Git repository...
git init

echo.
echo [2/5] Checking README files...
if exist README_USER.md (
    echo README_USER.md found - keeping both versions
) else (
    echo Using current README.md
)

echo.
echo [3/5] Adding files to Git...
git add .

echo.
echo [4/5] Creating initial commit...
git commit -m "Initial commit: Network Setter v1.0.0

Features:
- IPv4 and IPv6 configuration support
- DHCP and static IP management
- Network preset system
- Modern Windows Forms UI
- Administrator elevation
- Inno Setup installer script"

echo.
echo [5/5] Setting up branch...
git branch -M main

echo.
echo ========================================
echo Git repository setup complete!
echo ========================================
echo.
echo Next steps:
echo.
echo 1. Create a new repository on GitHub:
echo    https://github.com/new
echo.
echo 2. Set the remote URL (replace 'yourusername'):
echo    git remote add origin https://github.com/yourusername/network-setter.git
echo.
echo 3. Push your code:
echo    git push -u origin main
echo.
echo Optional: Update these files with your information:
echo    - README.md (line 164: Update author info)
echo    - installer.iss (lines 7-8: Update publisher and URL)
echo.
pause
