using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QM_SortToTabs.Mcm;
using UnityEngine;

namespace QM_SortToTabs
{
    public class ModConfig : IMcmConfigTarget
    {

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        public const int CurrentConfigVersion = 2;

        public int Version { get; set; } = -1;

        [JsonProperty(Order = 1000)]
        public TabMappings TabMappings = new TabMappings();

        /// <summary>
        /// If true, will log all matching info to assist debugging.
        /// </summary>
        public bool DebugLogMatches { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode SortToTabsKey { get; set; } = KeyCode.F5;

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode TabSortKey { get; set; } = KeyCode.S;


        [JsonIgnore]
        private static DateTime LastConfigFileChange = DateTime.MinValue;


        public void SetDefaults()
        {
            TabMappings.SetDefaults();
            Version = CurrentConfigVersion;
        }

        /// <summary>
        /// Reloads the config file if the date has changed.
        /// Keeps the existing config if there is a load failure.
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public static ModConfig ReloadChangedConfig(string configPath)
        {

            if (LastConfigFileChange == new FileInfo(configPath).LastWriteTime)
            {
                return null;
            }

            //Try to reload, but return null if it fails
            try
            {
                //TODO:  This should just use LoadConfig.  It works, so leaving for now to get the 0.8.5 change out.
                ModConfig config;
                config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(configPath));
                LastConfigFileChange = new FileInfo(configPath).LastWriteTime;

                return config;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing configuration.  Keeping existing config.");
                Debug.LogException(ex);

                return null;
            }
        }

        public static ModConfig LoadConfig(string configPath)
        {
            ModConfig config;


            if (File.Exists(configPath))
            {
                try
                {
                    string configText = File.ReadAllText(configPath);
                    config = JsonConvert.DeserializeObject<ModConfig>(configText);

                    if(config.ConfigUpgrade(configPath))
                    {
                        //Reset the config.
                        config = new ModConfig();
                        config.SetDefaults();

                        SaveConfig(configPath, config);

                        return config;
                    }


                    //Update file if there are json properties missing.
                    string updatedConfig = JsonConvert.SerializeObject(config, JsonSettings);

                    if (updatedConfig != configText)
                    {
                        SaveConfig(configPath, config);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing configuration.  Ignoring config file and using defaults");
                    Debug.LogException(ex);

                    //Just use the default config without overwriting the config file.  In case the user
                    //  made a simple typo such as missing a comma.
                    config = new ModConfig();
                    config.SetDefaults();
                    return config;
                }
            }
            else
            {
                //New config
                config = new ModConfig();
                config.SetDefaults();

                SaveConfig(configPath, config);

            }

            return config;


        }

        public void Save()
        {
            SaveConfig(Plugin.ConfigDirectories.ConfigPath, this);
        }

        private static void SaveConfig(string configPath, ModConfig config)
        {
            try
            {
                string json = JsonConvert.SerializeObject(config, JsonSettings);
                File.WriteAllText(configPath, json);
                LastConfigFileChange = new FileInfo(configPath).LastWriteTime;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SortToTabs]: Error saving the config file: {ex.ToString()}");
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Resets only the rules to the default and writes out the config file.
        /// </summary>
        public void ResetRulesToDefault(string configPath)
        {
            TabMappings = new TabMappings();
            TabMappings.SetDefaults();
            SaveConfig(configPath, this);
        }


        /// <summary>
        /// Executes any config upgrades required for older versions of the config
        /// </summary>
        /// <param name="configPath">The full path to the config file.</param>
        /// <returns>true if an upgraded was required and executed</returns>
        private bool ConfigUpgrade(string configPath)
        {
            //Version 2 was the first version that the config format changed.
            if (Version >= 2) return false;

            string backupPath = configPath + ".upgrade-backup";

            Debug.LogWarning($"[QM_SortToTabs] WARN the config is incompatible with the latest version and has been reset.  A backup of the old config can be found at '{backupPath}'");

            //TODO: This should be a UI prompt, but leaving for now.  Can check with the existence of a backup file for debugging.
            //Make a backup
            File.Copy(configPath, backupPath, true);

            //Removing this, just deleting instead.  Hopefully no longer needed.
            //Simplifies the UI.

            File.Delete(configPath);

            Version = CurrentConfigVersion;
            return true;
        }

    }
}
