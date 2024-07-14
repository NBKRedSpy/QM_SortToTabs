using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SortToTabs
{
    public static class Plugin
    {
        public static string ConfigPath => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name) + ".json";
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name);

        public static ModConfig Config { get; private set; }

        public static void ReloadChangedConfig()
        {
            ModConfig config = ModConfig.ReloadChangedConfig(ConfigPath);

            if(config != null)
            {
                Debug.Log("Config Reloaded");
                Config = config;
            }
        }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Config = ModConfig.LoadConfig(ConfigPath);
            ExportRecords();
        }

        private static void ExportRecords()
        {
            string path = Path.Combine(ModPersistenceFolder, "DataExport.csv");

            //Only write if the file does not already exist.
            if (File.Exists(path)) return;

            Directory.CreateDirectory(ModPersistenceFolder);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Id,Type,SubType");

                foreach (var item in Data.Items.Records)
                {

                    CompositeItemRecord composite = item as CompositeItemRecord;

                    BasePickupItemRecord target;
                    if (composite != null)
                    {
                        target = composite.PrimaryRecord;
                    }
                    else
                    {
                        target = item;
                    }

                    TrashRecord trash = target as TrashRecord;

                    writer.WriteLine(String.Join(",", target.Id, target.GetType().Name, trash?.SubType));
                }
            }
        }

        [Hook(ModHookType.AfterBootstrap)]
        public static void DungeonUpdateAfterGameLoop(IModContext context)
        {
            new Harmony("nbk_redspy.proto").PatchAll();

        }
    }
}
