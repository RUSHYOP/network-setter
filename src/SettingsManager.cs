using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace NetworkSetter
{
    public class AppSettings
    {
        public bool RunAtStartup { get; set; } = false;
        public bool MinimizeToTray { get; set; } = true;
        public bool StartMinimized { get; set; } = false;
        public Theme Theme { get; set; } = Theme.System;
        public bool ShowNotifications { get; set; } = true;
    }

    public static class SettingsManager
    {
        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NetworkSetter",
            "settings.json"
        );

        private const string RegistryKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "NetworkSetter";

        public static AppSettings LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsFilePath))
                    return new AppSettings();

                var json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
            }
            catch
            {
                return new AppSettings();
            }
        }

        public static void SaveSettings(AppSettings settings)
        {
            try
            {
                var directory = Path.GetDirectoryName(SettingsFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);

                // Update startup registry setting
                SetStartupRegistry(settings.RunAtStartup);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save settings: {ex.Message}");
            }
        }

        public static void SetStartupRegistry(bool enable)
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true))
                {
                    if (key == null)
                        return;

                    if (enable)
                    {
                        var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
                        if (!string.IsNullOrEmpty(exePath))
                        {
                            key.SetValue(AppName, $"\"{exePath}\" --minimized");
                        }
                    }
                    else
                    {
                        key.DeleteValue(AppName, false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update startup setting: {ex.Message}");
            }
        }

        public static bool IsStartupEnabled()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, false))
                {
                    var value = key?.GetValue(AppName);
                    return value != null;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
