@echo off
echo Building Network Setter...
echo.

cd ..\src
dotnet build -c Release
cd ..\bat scripts

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Build successful!
    echo.
    echo Executable location: src\bin\Release\net8.0-windows\NetworkSetter.exe
    echo.
    echo Remember to run as Administrator!
    pause
) else (
    echo.
    echo Build failed! Make sure .NET 8.0 SDK is installed.
    echo Download from: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
)
