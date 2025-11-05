@echo off
echo Starting Network Setter...
echo.
echo Note: This application requires Administrator privileges.
echo If prompted, please allow elevated access.
echo.

cd ..\src
dotnet run
cd ..\bat scripts

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo Failed to run! Make sure .NET 8.0 SDK is installed.
    echo Download from: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
)
