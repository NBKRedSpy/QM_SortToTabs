using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using UnityEngine;

namespace QM_SortToTabs
{
    public class ModConfig
    {
        public TabMappings DestinationTabs = new TabMappings();
        public bool ExportItemRecords { get; set; } = false;

        public static ModConfig LoadConfig(string configPath)
        {
            ModConfig config;

            if(File.Exists(configPath))
            {
                try
                {
                    config = new Deserializer().Deserialize<ModConfig>(File.ReadAllText(configPath));
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing configuration.  Ignoring config file and using defaults");
                    Debug.LogException(ex);

                    config = new ModConfig();
                }
            }
            else
            {
                //Error or new config.
                config = new ModConfig();

                string yaml = new Serializer().Serialize(config);
                File.WriteAllText(configPath, yaml);
            }

            config.DestinationTabs.Init();
            return config;

        }
    }
}
