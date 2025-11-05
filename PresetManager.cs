using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NetworkSetter
{
    public class PresetManager
    {
        private static readonly string PresetsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NetworkSetter",
            "presets.json"
        );

        public static List<NetworkConfig> LoadPresets()
        {
            try
            {
                if (!File.Exists(PresetsFilePath))
                    return new List<NetworkConfig>();

                var json = File.ReadAllText(PresetsFilePath);
                return JsonConvert.DeserializeObject<List<NetworkConfig>>(json) ?? new List<NetworkConfig>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load presets: {ex.Message}");
            }
        }

        public static void SavePresets(List<NetworkConfig> presets)
        {
            try
            {
                var directory = Path.GetDirectoryName(PresetsFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var json = JsonConvert.SerializeObject(presets, Formatting.Indented);
                File.WriteAllText(PresetsFilePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save presets: {ex.Message}");
            }
        }

        public static void AddPreset(NetworkConfig config)
        {
            var presets = LoadPresets();
            
            // Remove existing preset with the same name
            presets.RemoveAll(p => p.Name.Equals(config.Name, StringComparison.OrdinalIgnoreCase));
            
            presets.Add(config);
            SavePresets(presets);
        }

        public static void DeletePreset(string name)
        {
            var presets = LoadPresets();
            presets.RemoveAll(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            SavePresets(presets);
        }

        public static NetworkConfig? GetPreset(string name)
        {
            var presets = LoadPresets();
            return presets.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
