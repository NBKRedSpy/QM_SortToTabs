using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace QM_SortToTabs
{
    public class ModConfig
    {
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
                ModConfig config;
                config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(configPath));
                config.TabMappings.Init();
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

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };

            if (File.Exists(configPath))
            {
                try
                {
                    config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(configPath), serializerSettings);
                    config.TabMappings.Init();

                    LastConfigFileChange = new FileInfo(configPath).LastWriteTime;

                    return config;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing configuration.  Ignoring config file and using defaults");
                    Debug.LogException(ex);

                    //Not overwriting in case the user just made a typo.
                    config = new ModConfig();
                    config.TabMappings.SetDefaults();
                    config.TabMappings.Init();
                    return config;
                }
            }
            else
            {
                //New config
                config = new ModConfig();
                config.TabMappings.SetDefaults();
                config.TabMappings.Init();
                
                string json = JsonConvert.SerializeObject(config, serializerSettings);
                File.WriteAllText(configPath, json);

                LastConfigFileChange = new FileInfo(configPath).LastWriteTime;

                return config;
            }


        }
    }
}
