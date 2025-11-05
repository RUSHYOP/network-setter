@echo off
echo ========================================
echo Network Setter - Installer Builder
echo ========================================
echo.

REM Check if Inno Setup is installed
set "INNO_PATH=C:\Program Files (x86)\Inno Setup 6\ISCC.exe"

if not exist "%INNO_PATH%" (
    echo ERROR: Inno Setup not found!
    echo.
    echo Please install Inno Setup 6 from:
    echo https://jrsoftware.org/isdl.php
    echo.
    echo After installation, run this script again.
    echo.
    pause
    exit /b 1
)

echo [1/2] Building self-contained application...
echo.
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
echo [2/2] Creating installer...
echo.
"%INNO_PATH%" "installer.iss"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Installer created successfully!
    echo ========================================
    echo.
    echo Location: installer_output\NetworkSetter_Setup_v1.0.0.exe
    echo.
    echo You can now distribute this installer to anyone!
    echo.
    echo The installer will:
    echo - Install Network Setter to Program Files
    echo - Create Start Menu shortcuts
    echo - Optionally create Desktop shortcut
    echo - Add uninstaller to Windows
    echo - Check for Administrator privileges
    echo.
) else (
    echo.
    echo Installer creation failed!
    echo Check the error messages above.
)

pause
