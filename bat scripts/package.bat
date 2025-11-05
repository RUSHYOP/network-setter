@echo off
echo ========================================
echo Network Setter - Distribution Package
echo ========================================
echo.

REM Create output directory
if not exist "dist" mkdir dist

echo [1/3] Building self-contained application...
cd ..\src
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
cd ..

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo Build failed!
    pause
    exit /b 1
)

echo.
echo [2/3] Copying files to distribution folder...
xcopy "src\bin\Release\net8.0-windows\win-x64\publish\*.*" "dist\" /Y /I

echo.
echo [3/3] Creating README for distribution...
(
echo Network Setter - IP Configuration Manager
echo ==========================================
echo.
echo REQUIREMENTS:
echo - Windows 10/11
echo - Administrator privileges
echo.
echo HOW TO RUN:
echo 1. Right-click NetworkSetter.exe
echo 2. Select "Run as administrator"
echo.
echo FEATURES:
echo - Configure IPv4 and IPv6 settings
echo - Save network presets for different locations
echo - Switch between DHCP and static IP
echo - Manage DNS settings
echo.
echo IMPORTANT:
echo This application modifies network settings and MUST be run as Administrator.
echo.
echo For more information, visit: https://github.com/yourusername/networksetter
) > "dist\README.txt"

echo.
echo ========================================
echo Package created successfully!
echo ========================================
echo.
echo Distribution files are in the 'dist' folder
echo.
echo You can now:
echo 1. Zip the 'dist' folder
echo 2. Send it to your friend
echo.
echo Your friend only needs to:
echo - Extract the zip
echo - Right-click NetworkSetter.exe
echo - Run as Administrator
echo.
pause
