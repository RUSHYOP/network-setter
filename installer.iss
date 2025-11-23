; Network Setter V2 Installer Script
; Requires Inno Setup 6.0 or later
; Download from: https://jrsoftware.org/isdl.php
; 
; This installer handles both:
; - Fresh installations (first time)
; - Upgrades from previous versions (including V1)

#define MyAppName "Network Setter"
#define MyAppVersion "2.0.0"
#define MyAppPublisher "Network Setter"
#define MyAppURL "https://github.com/RUSHYOP/network-setter"
#define MyAppExeName "NetworkSetter.exe"

[Setup]
; NOTE: The AppId must remain the same to enable upgrades
; This ensures V2 upgrades V1 properly
AppId={{A7B8C9D0-E1F2-4A5B-8C7D-9E0F1A2B3C4D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
AllowNoIcons=yes
LicenseFile=LICENSE.txt
InfoBeforeFile=README_V2.md
OutputDir=installer_output
OutputBaseFilename=NetworkSetterV2_Setup
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=dialog
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName} V2
VersionInfoVersion={#MyAppVersion}
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription={#MyAppName} V2 Setup
VersionInfoCopyright=Copyright (C) 2025 {#MyAppPublisher}
; Close running application before upgrade
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "runonstartup"; Description: "Run at Windows startup (minimized to tray)"; GroupDescription: "Additional options:"; Flags: unchecked

[Files]
; Main application files
Source: "src\bin\Release\net8.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "src\bin\Release\net8.0-windows\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "src\bin\Release\net8.0-windows\*.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "src\bin\Release\net8.0-windows\*.pdb"; DestDir: "{app}"; Flags: ignoreversion

; Runtime dependencies (important for System.Management and other native libraries)
Source: "src\bin\Release\net8.0-windows\runtimes\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs

; Documentation files
Source: "README_V2.md"; DestDir: "{app}"; Flags: ignoreversion isreadme
Source: "QUICK_START_V2.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "V1_VS_V2.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "LICENSE.txt"; DestDir: "{app}"; Flags: ignoreversion

; Preserve user data during upgrade
[Dirs]
Name: "{userappdata}\NetworkSetter"; Flags: uninsneveruninstall

[Icons]
Name: "{group}\{#MyAppName} V2"; Filename: "{app}\{#MyAppExeName}"; Comment: "Network configuration with system tray support"; IconFilename: "{app}\{#MyAppExeName}"
Name: "{group}\Quick Start Guide"; Filename: "{app}\QUICK_START_V2.md"
Name: "{group}\User Manual"; Filename: "{app}\README_V2.md"
Name: "{group}\Uninstall {#MyAppName} V2"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; Comment: "Network Setter V2"
Name: "{commonstartup}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Parameters: "--minimized"; Tasks: runonstartup; Comment: "Network Setter V2 (Background)"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}} V2"; Flags: nowait postinstall skipifsilent shellexec

[Registry]
; Only add registry entry if "Run at startup" task is selected during installation
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "NetworkSetter"; ValueData: """{app}\{#MyAppExeName}"" --minimized"; Tasks: runonstartup
; Clean up old registry entries if they exist
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: none; ValueName: "NetworkSetter"; Flags: deletevalue; Check: not IsTaskSelected('runonstartup')

[Code]
var
  IsUpgrade: Boolean;
  OldVersion: String;

function GetInstalledVersion(): String;
var
  Version: String;
begin
  Version := '';
  if RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{A7B8C9D0-E1F2-4A5B-8C7D-9E0F1A2B3C4D}_is1', 
    'DisplayVersion', Version) then
    Result := Version
  else if RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{A7B8C9D0-E1F2-4A5B-8C7D-9E0F1A2B3C4D}_is1', 
    'DisplayVersion', Version) then
    Result := Version
  else
    Result := '';
end;

function IsAppRunning(): Boolean;
var
  ResultCode: Integer;
begin
  // Check if NetworkSetter.exe is running
  Result := Exec('tasklist.exe', '/FI "IMAGENAME eq NetworkSetter.exe" /NH', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Result := ResultCode = 0; // If found, ResultCode is 0
end;

function InitializeSetup(): Boolean;
var
  ResultCode: Integer;
begin
  IsUpgrade := False;
  OldVersion := GetInstalledVersion();
  
  if OldVersion <> '' then
  begin
    IsUpgrade := True;
    if MsgBox('Network Setter ' + OldVersion + ' is currently installed.' + #13#10 + #13#10 + 
              'Do you want to upgrade to version {#MyAppVersion}?' + #13#10 + #13#10 +
              'Your settings and presets will be preserved.', 
              mbConfirmation, MB_YESNO) = IDYES then
      Result := True
    else
      Result := False;
  end
  else
  begin
    Result := True;
  end;
  
  // Warn if app is running
  if Result and IsAppRunning() then
  begin
    if MsgBox('Network Setter is currently running.' + #13#10 + #13#10 +
              'It must be closed before installation can continue.' + #13#10 + #13#10 +
              'Click OK to close it automatically, or Cancel to exit setup.',
              mbConfirmation, MB_OKCANCEL) = IDOK then
    begin
      // Try to close the application
      Exec('taskkill.exe', '/F /IM NetworkSetter.exe', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Sleep(1000);
      Result := True;
    end
    else
      Result := False;
  end;
end;

function InitializeUninstall(): Boolean;
var
  ResultCode: Integer;
begin
  Result := True;
  
  // Check if app is running before uninstall
  if IsAppRunning() then
  begin
    if MsgBox('Network Setter is currently running and must be closed.' + #13#10 + #13#10 +
              'Click OK to close it, or Cancel to abort uninstallation.',
              mbConfirmation, MB_OKCANCEL) = IDOK then
    begin
      Exec('taskkill.exe', '/F /IM NetworkSetter.exe', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Sleep(1000);
      Result := True;
    end
    else
      Result := False;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  StartupKeyExists: Boolean;
begin
  if CurStep = ssPostInstall then
  begin
    if IsUpgrade then
    begin
      MsgBox('Network Setter V2 has been successfully upgraded!' + #13#10 + #13#10 + 
             'Previous version: ' + OldVersion + #13#10 +
             'New version: {#MyAppVersion}' + #13#10 + #13#10 +
             'What''s New in V2:' + #13#10 +
             '• System tray integration' + #13#10 +
             '• Quick access popup' + #13#10 +
             '• Light and Dark themes' + #13#10 +
             '• Run at startup option' + #13#10 +
             '• Background operation' + #13#10 + #13#10 +
             'Your network presets and settings have been preserved!', 
             mbInformation, MB_OK);
    end
    else
    begin
      MsgBox('Network Setter V2 has been installed successfully!' + #13#10 + #13#10 + 
             'Features:' + #13#10 +
             '• System tray integration for quick access' + #13#10 +
             '• Light and Dark themes' + #13#10 +
             '• Background operation' + #13#10 +
             '• Network configuration presets' + #13#10 +
             '• IPv4 and IPv6 support' + #13#10 + #13#10 +
             'Note: Administrator rights required for network changes.' + #13#10 +
             'Right-click and "Run as administrator" when needed.', 
             mbInformation, MB_OK);
    end;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then
  begin
    if MsgBox('Do you want to remove your settings and network presets?' + #13#10 + #13#10 +
              'Click Yes to delete all settings (clean uninstall).' + #13#10 +
              'Click No to keep your settings for future installations.',
              mbConfirmation, MB_YESNO) = IDYES then
    begin
      // Remove settings directory
      DelTree(ExpandConstant('{userappdata}\NetworkSetter'), True, True, True);
      MsgBox('All settings and presets have been removed.', mbInformation, MB_OK);
    end
    else
    begin
      MsgBox('Your settings and presets have been preserved.' + #13#10 +
             'They will be available if you reinstall Network Setter.', 
             mbInformation, MB_OK);
    end;
  end;
end;
